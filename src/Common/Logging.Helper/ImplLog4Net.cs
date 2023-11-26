#region log4net Information
// log4net Home
// http://logging.apache.org/log4net/

// log4net NuGet package
// https://www.nuget.org/packages/log4net/
// Install-Package log4net -Version 2.0.13

// log4net Documentation
// https://logging.apache.org/log4net/release/manual/introduction.html

// log4net Tutorials
// http://www.codeproject.com/Articles/140911/log4net-Tutorial

// Configuring log4net in AssemblyInfo.cs (requires log4net.dll reference in project)
//[assembly: log4net.Config.XmlConfigurator( Watch = true )]

// Configure log4net using a specific configuration file
//[assembly: log4net.Config.XmlConfigurator( ConfigFile = "Sample.log4net", Watch = true )]

// The following alias attribute can be used to capture the logging repository for the
// 'SimpleModule' assembly. Without specifying this attribute the logging configuration
// for the 'SimpleModule' assembly will be read from the 'SimpleModule.dll.log4net' file.
// When this attribute is specified the configuration will be shared with this assembly's configuration.
//[assembly: log4net.Config.AliasRepository("SimpleModule")]
#endregion

using System;
using System.IO;
using System.Reflection;

namespace KevinDHeath.Logging.Helper
{
	/// <summary>
	/// Class that implements log4net logging.
	/// </summary>
	internal class ImplLog4Net : ILog
	{
		#region Constants

		private const string cDefaultPattern = @"%date{HH:mm:ss} %-5level %message%newline";

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

		#endregion

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
		/// You need to use the '%exception' pattern variable in your
		/// appender to actually capture this exception information.
		/// </remarks>
		public void Error( string message, Exception exception )
		{
			_logger.Error( message, exception );
		}

		/// <inheritdoc/>
		/// <remarks>
		/// You need to use the '%exception' pattern variable in your
		/// appender to actually capture this exception information.
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
			// Process all the file appenders
			foreach( var repository in log4net.LogManager.GetAllRepositories() )
			{
				foreach( var appender in repository.GetAppenders() )
				{
					if( appender is log4net.Appender.FileAppender )
					{
						// Set the file directory and filename 
						var fileAppender = appender as log4net.Appender.FileAppender;
						var file = logFileName.Length > 0 ? logFileName : Path.GetFileName( fileAppender.File );
						var dir = logDirectory.Length > 0 ? logDirectory : Path.GetDirectoryName( fileAppender.File );
						if( dir != null && file != null )
						{
							fileAppender.File = Path.Combine( dir, file );
						}

						// Notify the sub-system that the configuration has changed
						fileAppender.ActivateOptions();
					}
				}
			}
		}

		#endregion

		#region Constructor and Initialization

		private log4net.ILog _logger;

		/// <summary>
		/// Initializes the Logging component using log4net configuration.
		/// </summary>
		/// <param name="assembly">Calling Assembly.</param>
		/// <param name="loggerType">Type to be used as the name of the logger to retrieve.</param>
		/// <param name="configFile">log4net configuration file to use.</param>
		/// <returns>Logging configuration file name.</returns>
		internal ImplLog4Net( Assembly assembly, Type loggerType, string configFile )
		{
			Initialize( assembly, loggerType, configFile );
		}

		private string Initialize( Assembly assembly, Type loggerType, string configFile )
		{
			var retValue = configFile;

			// Try setting the configuration file from the assembly
			if( string.IsNullOrWhiteSpace( retValue ) )
			{
				retValue = GetConfigFileName( assembly );
			}

			// Set the log4net logger
			var configSet = SetConfigFile( ref retValue );

			_logger = log4net.LogManager.GetLogger( loggerType );

			var loggerSet = true;
			if( !_logger.Logger.Repository.Configured )
			{
				log4net.Config.BasicConfigurator.Configure( GetDefaultConsoleAppender() );
				loggerSet = false;
			}

			if( !configSet )
			{
				if( retValue.Length > 0 )
					_logger.Error( @"Logging configuration file could not be found." + Environment.NewLine + retValue );
				else
					_logger.Error( @"Logging configuration file not specified." );
			}

			if( !loggerSet )
			{
				_logger.Info( @"Using default Console logging." );
			}
			else
			{
				// Set the maximum number of log files from rolling file appender
				foreach( var repository in log4net.LogManager.GetAllRepositories() )
				{
					foreach( var appender in repository.GetAppenders() )
					{
						var rollingFile = appender as log4net.Appender.RollingFileAppender;
						if( rollingFile?.MaxSizeRollBackups > MaxLogFiles )
							MaxLogFiles = rollingFile.MaxSizeRollBackups;
					}
				}
			}

			return retValue;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the logging configuration file.
		/// </summary>
		/// <param name="configFile">Full path and name of the configuration file.</param>
		/// <returns>True if the configuration file was loaded, otherwise false is returned.</returns>
		private static bool SetConfigFile( ref string configFile )
		{
			var retValue = false;

			configFile = configFile.Trim();

			// Check that the configuration file exists
			if( !string.IsNullOrWhiteSpace( configFile ) )
			{
				try
				{
					var fi = new FileInfo( configFile );
					if( fi.Exists )
					{
						log4net.Config.XmlConfigurator.Configure( fi );
						retValue = true;
						configFile = fi.FullName;

					}
					configFile = fi.FullName;
				}
				catch( Exception )
				{
					// ignored
				}
			}

			return retValue;
		}

		/// <summary>
		/// Gets a default console appender settings.
		/// </summary>
		/// <returns>Default console appender.</returns>
		private log4net.Appender.IAppender GetDefaultConsoleAppender()
		{
			var retValue = new log4net.Appender.ConsoleAppender { Layout = GetDefaultPatternLayout() };
			retValue.ActivateOptions();

			return retValue;
		}

		/// <summary>
		/// Gets a default log pattern layout.
		/// </summary>
		/// <returns>Default log pattern.</returns>
		private static log4net.Layout.ILayout GetDefaultPatternLayout()
		{
			var retValue = new log4net.Layout.PatternLayout { ConversionPattern = cDefaultPattern };
			retValue.ActivateOptions();

			return retValue;
		}

		/// <summary>
		/// Gets the configuration file name from the assembly custom attributes.
		/// </summary>
		/// <param name="assembly">Assembly to use.</param>
		/// <returns>Name of the configuration file if it is found, otherwise an empty string is returned.</returns>
		private static string GetConfigFileName( Assembly assembly )
		{
			var retValue = string.Empty;
			if( null == assembly )
			{
				return retValue;
			}

			// Try getting the configuration file name from the assembly custom attribute
			var attrs = assembly.GetCustomAttributes( typeof( log4net.Config.XmlConfiguratorAttribute ), false );
			if( attrs.Length > 0 )
			{
				var attr = (log4net.Config.XmlConfiguratorAttribute)attrs[0];
				retValue = attr.ConfigFile;
			}

			return retValue;
		}

		#endregion
	}
}
