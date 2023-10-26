using System;

namespace KevinDHeath.Logging.Helper
{
	/// <summary>Interface for a Logging implementation.</summary>
	internal interface ILog
	{
		#region Properties

		/// <summary>Gets or sets the maximum number of log files to keep.</summary>
		int MaxLogFiles { get; set; }

		/// <summary>
		/// Indicates if logging is enabled for the <see cref="Logging.Helper.LogSeverity.Debug"/> level.
		/// </summary>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Indicates if logging is enabled for the <see cref="Logging.Helper.LogSeverity.Error"/> level.
		/// </summary>
		bool IsErrorEnabled { get; }

		/// <summary>
		/// Indicates if logging is enabled for the <see cref="Logging.Helper.LogSeverity.Fatal"/> level.
		/// </summary>
		bool IsFatalEnabled { get; }

		/// <summary>
		/// Indicates if logging is enabled for the <see cref="Logging.Helper.LogSeverity.Information"/> level.
		/// </summary>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Indicates if logging is enabled for the <see cref="Logging.Helper.LogSeverity.Warning"/> level.
		/// </summary>
		bool IsWarnEnabled { get; }

		#endregion

		#region Methods

		/// <summary>Logs a debug message.</summary>
		/// <param name="message">Message text to log.</param>
		void Debug( string message );

		/// <summary>Logs an error message.</summary>
		/// <param name="message">Message text to log.</param>
		void Error( string message );

		/// <summary>Logs an error message with an exception.</summary>
		/// <param name="message">Message text to log.</param>
		/// <param name="exception">Exception to log.</param>
		void Error( string message, Exception exception );

		/// <summary>Logs an exception.</summary>
		/// <param name="exception">Exception to log.</param>
		void Error( Exception exception );

		/// <summary>Logs a fatal error message.</summary>
		/// <param name="message">Message text to log.</param>
		void Fatal( string message );

		/// <summary>Logs an informational message.</summary>
		/// <param name="message">Message text to log.</param>
		void Info( string message );

		/// <summary>Logs a warning message.</summary>
		/// <param name="message">Message text to log.</param>
		void Warn( string message );

		/// <summary>Sets the location of the log file and optionally, the log file name.</summary>
		/// <param name="logDirectory">Location of the log file.</param>
		/// <param name="logFileName">Name of the log file.</param>
		void SetLogFile( string logDirectory, string logFileName = "" );

		#endregion
	}
}