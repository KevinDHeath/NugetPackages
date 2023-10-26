using System;

namespace KevinDHeath.Logging.Helper
{
	/// <summary>Common event arguments to use when logging a message.</summary>
	public class LoggerEventArgs : EventArgs
	{
		#region Constructor

		/// <summary>Initializes a new instance of the EventArgs class.</summary>
		/// <param name="message">Message to log.</param>
		/// <param name="severity">Event severity, if not provided the severity will be set as Information.</param>
		public LoggerEventArgs( string message, LogSeverity severity = LogSeverity.Information )
		{
			Message = string.IsNullOrEmpty( message ) ? string.Empty : message;
			Severity = severity;
		}

		#endregion

		#region Properties

		/// <summary>Gets the event message.</summary>
		public string Message { get; }

		/// <summary>Gets the event severity.</summary>
		public LogSeverity Severity { get; }

		#endregion
	}
}