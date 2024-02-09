using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using Application.Helper;

namespace Logging.Helper
{
	#region Instructions
	/// <summary>Class that provides common logging properties and methods.</summary>

	/// <remarks>
	/// <p>These are the different types of log message that can be used:</p>
	/// <table>
	/// <tr><th>Type</th><th>Description</th>
	/// </tr><tr>
	/// <td>Fatal</td><td>Use to log non-recoverable exceptions that
	/// unexpectedly end the process.</td>
	/// </tr><tr>
	/// <td>Error</td><td>Use to log exceptions that have been handled but caused
	/// the processing to stop.</td>
	/// </tr><tr>
	/// <td>Warning</td><td>Use to indicate that the process faced a potential problem
	/// but can continue.</td>
	/// </tr><tr>
	/// <td>Information</td><td>These are purely informational messages; they <i>should not</i>
	/// be used to indicate a fault or error state.</td>
	/// </tr><tr>
	/// <td>Debug</td><td>Used to indicate that the logged message can be used for
	/// debugging purposes to help find the solution to tricky bugs.</td>
	/// </tr>
	/// </table>
	/// </remarks>

	/// <example>
	/// In the program that requires logging functionality:
	/// <code lang="C#">
	/// using Logging.Helper;
	/// public class Program
	/// {
	///   /// <summary>Public static Logger.</summary>
	///   // Logging configuration in the application settings (App.config)...
	///   public static readonly Logger Logging = new Logger( typeof( Program ) );
	///     
	///   // ... OR shared logging configuration filename specified in the application settings...
	///   public static readonly Logger Logging = new Logger( Properties.Settings.Default.LogConfig );
	///     
	///   // ... OR shared logging configuration filename hard-coded in each program...
	///   public static readonly Logger Logging = new Logger( MethodBase.GetCurrentMethod().DeclaringType, "Sample.log4net" );
	///
	///   static void Main( string[] args )
	///   {
	///     try
	///     {
	///       Logging.Log( "Test specified message type.", LogSeverity.Warning );
	///     }
	///     catch( Exception ex )
	///     {
	///       // Handle any unexpected exceptions
	///       Logging.Fatal( ex.ToString() );
	///     }
	///     finally
	///     {
	///       // Set the program exit code based on whether any errors have been logged
	///       Environment.ExitCode = Logging.HasErrors ? 1 : 0;
	///     }
	///     Environment.Exit( Environment.ExitCode );
	///   }
	/// }
	/// </code>
	/// <br/>To allow a processing class to log using the logger created in the program it must inherit
	/// from the Logging.Helper.LoggerEvent class and have the event handler set after it has been instantiated:
	/// <code lang="C#">
	/// using Logging.Helper;
	/// public class ProcessingClass : LoggerEvent
	/// {
	///   internal int DoProcessing()
	///   {
	///     var retValue = 0; // Assume normal completion
	///     try
	///     {
	///       // This method is inherited from the LoggerEvent class
	///       RaiseLogEvent( "Information message about the processing..." );
	///     }
	///     catch( Exception ex )
	///     {
	///       RaiseLogEvent( ex.ToString(), LogSeverity.Fatal );
	///       retValue = 1; // Abnormal completion
	///     }
	///     return retValue;
	///   }
	/// }
	/// 
	/// using Logging.Helper;
	/// public class Program
	/// {
	///   /// <summary>Public static Logger.</summary>
	///   public static readonly Logger Logging = new Logger( typeof( Program ) );
	///     
	///   static void Main( string[] args )
	///   {
	///     try
	///     {
	///       // Create an object from a class that inherits LoggerEvent
	///       var logicClass = new ProcessingClass();
	///       logicClass.RaiseLogHandler += Logging.OnRaiseLog;
	///
	///       // Do Processing and set the Exit code
	///       logicClass.DoProcessing();
	///     }
	///     catch( Exception ex )
	///     {
	///       // Handle any unexpected exceptions
	///       Logging.Fatal( ex.ToString() );
	///     }
	///     finally
	///     {
	///       // Set the program exit code based on whether any errors have been logged
	///       Environment.ExitCode = Logging.HasErrors ? 1 : 0;
	///     }
	///     Environment.Exit( Environment.ExitCode );
	///   }
	/// }
	/// </code>
	/// </example>
	#endregion
	public sealed class Logger
	{
		#region Properties and Constants

		/// <summary>Gets or sets the maximum number of log files to keep.</summary>
		public int MaxLogFiles
		{
			get => LogImpl?.MaxLogFiles ?? 0;
			set
			{
				if( null != LogImpl )
				{
					LogImpl.MaxLogFiles = value;
				}
			}
		}

		/// <summary>Gets the logging configuration file name.</summary>
		public string ConfigFile { get; private set; } = string.Empty;

		/// <summary>Gets the count of warning messages logged.</summary>
		public int WarnCount { get; private set; }

		/// <summary>Gets the count of error messages logged.</summary>
		public int ErrorCount { get; private set; }

		/// <summary>Gets the count of fatal messages logged.</summary>
		public int FatalCount { get; private set; }

		/// <summary>Indicates whether errors exists.</summary>
		public bool HasErrors => ( ErrorCount + FatalCount ) > 0;

		/// <summary>Gets or sets the logger interface.</summary>
		private ILog LogImpl { get; set; }

		/// <summary>Log file extension (suffix) including the period.</summary>
		public const string cExtension = ".log";

		#endregion

		#region Constructors and Initialization

		/// <summary>
		/// Initializes a new instance of the Logger class using an optional configuration file name.
		/// </summary>
		/// <param name="configFile">Logging configuration file name to use.</param>
		/// <exception cref="NLog.NLogConfigurationException">Thrown if the NLog configuration is invalid.</exception>
		/// <exception cref="TargetException">Thrown when an attempt is made to invoke an invalid target.</exception>
		public Logger( string configFile = "" )
		{
			Initialize( Assembly.GetCallingAssembly(), MethodBase.GetCurrentMethod().DeclaringType, configFile );
		}

		/// <summary>
		/// Initializes a new instance of the Logger class using a logger type and configuration file name.
		/// </summary>
		/// <param name="loggerType">Type to be used as the name of the logger to retrieve.</param>
		/// <param name="configFile">Logging configuration file name to use.</param>
		/// <exception cref="NLog.NLogConfigurationException">Thrown if the NLog configuration is invalid.</exception>
		public Logger( Type loggerType, string configFile = "" )
		{
			Initialize( Assembly.GetCallingAssembly(), loggerType, configFile );
		}

		private void Initialize( Assembly assembly, Type loggerType, string configFile )
		{
			try
			{
				if( configFile.Length > 0 )
				{
					// Set the configuration file name
					ConfigFile = Path.GetFullPath( configFile );

					// Check if the configuration file exists
					if( !File.Exists( ConfigFile ) ) { ConfigFile = string.Empty; }
				}
				else
				{
					// If no configuration file supplied check if an app.config file exists
					configFile = assembly.Location + ".config";
					if( File.Exists( configFile ) ) { ConfigFile = configFile; }
				}
			}
			catch { ConfigFile = string.Empty; }

			// Initialize the logging implementation
			//LogImpl = new ImplBasicLog();

			// Requires an assembly reference for NLog.dll
			LogImpl = new ImplNLog( loggerType, ConfigFile );

			// Requires an assembly reference for log4net.dll
			//LogImpl = new ImplLog4Net( assembly, loggerType, ConfigFile );
		}

		/// <summary>Converts the value of this instance to a string.</summary>
		/// <returns>Instance value.</returns>
		public override string ToString()
		{
			return null == LogImpl ? GetType().ToString() : LogImpl.ToString();
		}

		#endregion

		#region Private Methods

		/// <summary>Cleans a string value.</summary>
		/// <param name="strToClean">String to clean.</param>
		/// <returns>Trimmed string.</returns>
		internal static string CleanString( string strToClean )
		{
			return string.IsNullOrWhiteSpace( strToClean ) ? string.Empty : strToClean.Trim();
		}

		private bool CanLog( LogSeverity severity )
		{
			if( null == LogImpl ) return false;

			switch( severity )
			{
				case LogSeverity.Error:
					return LogImpl.IsErrorEnabled;
				case LogSeverity.Fatal:
					return LogImpl.IsFatalEnabled;
				case LogSeverity.Information:
					return LogImpl.IsInfoEnabled;
				case LogSeverity.Warning:
					return LogImpl.IsWarnEnabled;
				default:
					return LogImpl.IsDebugEnabled;
			}
		}

		#endregion

		#region Log Event Handler Methods

		/// <summary>Event handler for the logging process.</summary>
		/// <param name="sender">Sender object triggering the event.</param>
		/// <param name="e">Instance of LogEventArgs class with addition details about the logging event.</param>
		public void OnRaiseLog( object sender, LoggerEventArgs e )
		{
			Log( e.Message, e.Severity );
		}

		/// <summary>Log a message.</summary>
		/// <param name="msg">Message text.</param>
		/// <param name="severity">Identifies the type of trace event.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool Log( string msg, LogSeverity severity = LogSeverity.Information )
		{
			bool retValue;
			switch( severity )
			{
				case LogSeverity.Fatal:
					retValue = Fatal( msg );
					break;
				case LogSeverity.Error:
					retValue = Error( msg );
					break;
				case LogSeverity.Warning:
					retValue = Warn( msg );
					break;
				case LogSeverity.Information:
					retValue = Info( msg );
					break;
				default:
					retValue = Debug( msg );
					break;
			}

			return retValue;
		}

		#endregion

		#region Message Logging

		#region Logging Information

		/// <summary>Logs an informational message.</summary>
		/// <param name="message">Message text to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Info( string message )
		{
			if( !CanLog( LogSeverity.Information ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 ) return false;

			LogImpl.Info( message );
			return true;
		}

		/// <summary>Logs an informational message with arguments.</summary>
		/// <param name="message">Composite format message string to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Info( string message, params object[] args )
		{
			if( !CanLog( LogSeverity.Information ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 || null == args ) return false;

			LogImpl.Info( string.Format( message, args ) );
			return true;
		}

		#endregion

		#region Logging Warning

		/// <summary>Logs a warning message.</summary>
		/// <param name="message">Message text to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Warn( string message )
		{
			WarnCount++;
			if( !CanLog( LogSeverity.Warning ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 ) return false;

			LogImpl.Warn( message );
			return true;
		}

		/// <summary>Logs a warning message with arguments.</summary>
		/// <param name="message">Composite format message string to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Warn( string message, params object[] args )
		{
			WarnCount++;
			if( !CanLog( LogSeverity.Warning ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 || null == args ) return false;

			LogImpl.Warn( string.Format( message, args ) );
			return true;
		}

		#endregion

		#region Logging Error

		/// <summary>Logs an error message.</summary>
		/// <param name="message">Message text to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Error( string message )
		{
			ErrorCount++;
			if( !CanLog( LogSeverity.Error ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 ) return false;

			LogImpl.Error( message );
			return true;
		}

		/// <summary>Logs an error message with arguments.</summary>
		/// <param name="message">Composite format message string to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Error( string message, params object[] args )
		{
			ErrorCount++;
			if( !CanLog( LogSeverity.Error ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 || null == args ) return false;

			LogImpl.Error( string.Format( message, args ) );
			return true;
		}

		/// <summary>Logs an error message with an exception.</summary>
		/// <param name="message">Message text to log.</param>
		/// <param name="exception">Exception to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Error( string message, Exception exception )
		{
			ErrorCount++;
			if( !CanLog( LogSeverity.Error ) ) return false;
			message = CleanString( message );

			LogImpl.Error( message, exception );
			return true;
		}

		/// <summary>Logs an exception.</summary>
		/// <param name="exception">Exception to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Error( Exception exception )
		{
			ErrorCount++;
			if( !CanLog( LogSeverity.Error ) ) return false;

			LogImpl.Error( exception );
			return true;
		}

		#endregion

		#region Logging Fatal

		/// <summary>Logs a fatal error message.</summary>
		/// <param name="message">Message text to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Fatal( string message )
		{
			FatalCount++;
			if( !CanLog( LogSeverity.Fatal ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 ) return false;

			LogImpl.Fatal( message );
			return true;
		}

		/// <summary>Logs a fatal error message with arguments.</summary>
		/// <param name="message">Composite format message string to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Fatal( string message, params object[] args )
		{
			FatalCount++;
			if( !CanLog( LogSeverity.Fatal ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 || null == args ) return false;

			LogImpl.Fatal( string.Format( message, args ) );
			return true;
		}

		/// <summary>Logs a fatal exception.</summary>
		/// <param name="exception">Exception to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Fatal( Exception exception )
		{
			return Log( GenericException.FormatException( exception ), LogSeverity.Fatal );
		}

		#endregion

		#region Logging Debug

		/// <summary>Logs a debugging message.</summary>
		/// <param name="message">Message text to log.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Debug( string message )
		{
			if( !CanLog( LogSeverity.Debug ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 ) return false;

			LogImpl.Debug( message );
			return true;
		}

		/// <summary>Logs a debugging message with arguments.</summary>
		/// <param name="message">Composite format message string to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns><see langword="true"/> if the message was logged, otherwise <see langword="false"/> is returned.</returns>
		public bool Debug( string message, params object[] args )
		{
			if( !CanLog( LogSeverity.Debug ) ) return false;

			message = CleanString( message );
			if( message.Length <= 0 || null == args ) return false;

			LogImpl.Debug( string.Format( message.Trim(), args ) );
			return true;
		}

		#endregion

		#endregion

		#region Log File Methods

		/// <summary>Sets the location of the log file and optionally, the log file name.</summary>
		/// <param name="logDirectory">Location of the log file.</param>
		/// <param name="logFileName">Name of the log file.</param>
		/// <exception cref="ArgumentException">Thrown when one of the arguments provided to a method is not valid.</exception>
		/// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
		/// <exception cref="NotSupportedException">Thrown when an invoked method is not supported, or when there
		/// is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
		/// <exception cref="SecurityException">Thrown when a security error is detected.</exception>
		public void SetLogFile( string logDirectory, string logFileName = "" )
		{
			// Check the parameter values
			logDirectory = CleanString( logDirectory );
			logFileName = CleanString( logFileName );

			// Process the directory first as it could contain the file name as well
			if( logDirectory.Length > 0 )
			{
				logDirectory = Path.GetFullPath( logDirectory );

				// If a file extension is found then a file name is also present
				string ext = Path.GetExtension( logDirectory );
				if( ext.Length > 0 & logFileName.Length == 0 )
				{
					logFileName = Path.GetFileName( logDirectory );
					logDirectory = Path.GetDirectoryName( logDirectory );
				}

				// Check that the directory path is valid
				DirectoryInfo dir = new DirectoryInfo( logDirectory );
				if( null == dir || !dir.Root.Exists ) { throw new DirectoryNotFoundException( logDirectory ); }
			}

			if( logFileName.Length > 0 )
			{
				// Check that the log file name is valid
				logFileName = Path.GetFileName( logFileName );
			}

			// Set the implementation log file location and name
			LogImpl.SetLogFile( logDirectory, logFileName );
		}

		/// <summary>Removes the oldest non-read-only log files in a directory.</summary>
		/// <param name="directory">Directory containing the log files.</param>
		/// <param name="logNameMask">Search pattern in the form [LogFile]*.[ext] of the log file names.</param>
		/// <param name="maxFiles">Maximum number of log files to keep.</param>
		/// <returns><see langword="true"/> if any log files have been removed, otherwise <see langword="false"/> is returned.</returns>
		/// <example>
		/// In a method that needs to remove old log files:
		/// <code lang="C#">
		/// var directory = @"C:\Temp\Logs" );
		/// const string mask = "LogfileName*.log";
		/// const int maxLogFiles = 50;
		/// logging.RemoveLogs( directory, mask, maxLogFiles );
		/// </code>
		/// </example>
		public bool RemoveLogs( string directory, string logNameMask, int maxFiles = 0 )
		{
			DirectoryInfo dir = new DirectoryInfo( directory );

			// Check the required parameters have been passed
			if( null == dir || string.IsNullOrEmpty( logNameMask ) || maxFiles <= 0 )
			{
				return false;
			}

			// Check if the directory contains any log files
			FileInfo[] logFiles = null;
			try
			{
				logFiles = dir.GetFiles( logNameMask );
			}
			catch( Exception ) { } // ignored

			if( null == logFiles )
			{
				return false;
			}

			// Create list of log files that are not read-only
			List<FileInfo> list = new List<FileInfo>();
			foreach( var fi in logFiles )
			{
				// Exclude read-only files from deletion
				if( !fi.Attributes.HasFlag( FileAttributes.ReadOnly ) )
				{
					list.Add( fi );
				}
			}

			// Check that file count is greater than maximum number
			if( list.Count <= maxFiles )
			{
				return false;
			}

			// Sort in ascending sequence by last written date/time
			list.Sort( ( x, y ) => x.LastWriteTime.CompareTo( y.LastWriteTime ) );

			// Delete old log files
			maxFiles = list.Count - maxFiles;
			int count = 0;
			for( var i = 0; i < maxFiles; i++ )
			{
				FileInfo fi = list[i];
				try
				{
					fi.Delete();
					count++;
				}
				catch( Exception ) { Error( @"Could not delete log file " + fi.FullName ); }
			}

			return count > 0;
		}

		#endregion
	}
}