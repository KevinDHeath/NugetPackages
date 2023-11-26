#region NLog Information
// NLog Home
// http://nlog-project.org/

// NLog NuGet package
// https://www.nuget.org/packages/NLog/
// Install-Package NLog -Version 4.7.2

// NLog Documentation
// https://github.com/nlog/nlog/wiki

// NLog Tutorials
// https://github.com/NLog/NLog/wiki/Tutorial
// https://www.codeproject.com/Articles/10631/Introduction-to-NLog
#endregion

using System;

namespace Logging.Helper
{
	/// <summary>Class that implements NLog logging.</summary>
	internal class ImplNLog : ILog
	{
		#region Constants

		private const string cDefaultPattern = @"${longdate} ${uppercase:${level:padding=-5}} ${message}";
		private const string cLogFileDirVar = @"basedir";
		private const string cLogFileNameVar = @"logfile";

		#endregion

		#region ILog Implementation

		#region Properties

		/// <inheritdoc/>
		public int MaxLogFiles { get; set; }

		/// <inheritdoc/>
		public bool IsDebugEnabled => _logger?.IsDebugEnabled ?? false;

		/// <inheritdoc/>
		public bool IsErrorEnabled => _logger?.IsErrorEnabled ?? false;

		/// <inheritdoc/>
		public bool IsFatalEnabled => _logger?.IsFatalEnabled ?? false;

		/// <inheritdoc/>
		public bool IsInfoEnabled => _logger?.IsInfoEnabled ?? false;

		/// <inheritdoc/>
		public bool IsWarnEnabled => _logger?.IsWarnEnabled ?? false;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public void Debug( string message )
		{
			_logger.Debug( message );
		}

		/// <inheritdoc/>
		public void Error( string message )
		{
			_logger.Error( message );
		}

		/// <inheritdoc/>
		/// <remarks>
		/// You need to use an exception rendering option such as
		/// "${exception:format=toString,Data:maxInnerExceptionLevel=10}"
		/// in your target layout to actually capture the exception information.
		/// </remarks>
		public void Error( string message, Exception exception )
		{
			_logger.Error( exception, message );
		}

		/// <inheritdoc/>
		/// <remarks>
		/// You need to use an exception rendering option such as
		/// "${exception:format=toString,Data:maxInnerExceptionLevel=10}"
		/// in your target layout to actually capture the exception information.
		/// </remarks>
		public void Error( Exception exception )
		{
			_logger.Error( exception );
		}

		/// <inheritdoc/>
		public void Fatal( string message )
		{
			_logger.Fatal( message );
		}

		/// <inheritdoc/>
		public void Info( string message )
		{
			_logger.Info( message );
		}

		/// <inheritdoc/>
		public void Warn( string message )
		{
			_logger.Warn( message );
		}

		/// <inheritdoc/>
		public void SetLogFile( string logDirectory, string logFileName = "" )
		{
			if( !string.IsNullOrWhiteSpace( logDirectory ) )
			{
				// Change the NLog variable used for the log file folder
				NLog.LogManager.Configuration.Variables[cLogFileDirVar] = logDirectory;
			}

			if( !string.IsNullOrWhiteSpace( logFileName ) )
			{
				// Change the NLog variable used for the log file name
				NLog.LogManager.Configuration.Variables[cLogFileNameVar] = logFileName;
			}
		}

		#endregion

		#endregion

		#region Constructor / Destructor

		private readonly NLog.Logger _logger;

		/// <summary>Creates a new instance of the ImplNLog class.</summary>
		/// <param name="loggerType">Type to be used as the name of the logger.</param>
		/// <param name="configFile">NLog configuration file to use.</param>
		internal ImplNLog( Type loggerType, string configFile )
		{
			if( !string.IsNullOrWhiteSpace( configFile ) )
			{
				// Set the configuration from a file
				NLog.LogManager.ThrowConfigExceptions = true;
				NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration( configFile );
			}

			if( null == NLog.LogManager.Configuration || // No configuration file
				NLog.LogManager.Configuration.ConfiguredNamedTargets.Count == 0 ) // No targets loaded
			{
				// Set a default configuration
				NLog.LogManager.Configuration = GetDefaultConfig();
			}

			// Set the logger
			_logger = null != loggerType ?
				NLog.LogManager.GetLogger( loggerType.FullName ) :
				NLog.LogManager.GetCurrentClassLogger();
		}

		/// <summary>Called once the object has been disposed.</summary>
		~ImplNLog()
		{
			try
			{
				NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
			}
			catch( Exception )
			{
				// Do nothing
			}
		}

		#endregion

		#region Private Methods

		/// <summary>Gets a default console configuration.</summary>
		private static NLog.Config.LoggingConfiguration GetDefaultConfig()
		{
			// Create configuration object 
			var retValue = new NLog.Config.LoggingConfiguration();

			// Create target and add it to the configuration 
			var consoleTarget = new NLog.Targets.ConsoleTarget();
			retValue.AddTarget( "console", consoleTarget );

			// Set target properties 
			consoleTarget.Layout = cDefaultPattern;

			// Define rules
			var rule = new NLog.Config.LoggingRule( "*", NLog.LogLevel.Debug, consoleTarget );
			retValue.LoggingRules.Add( rule );

			return retValue;
		}

		#endregion
	}
}