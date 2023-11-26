using System;
using System.Diagnostics;

namespace Logging.Helper
{
	#region Log Severity types

	/// <summary>Logging severity types.</summary>
	public enum LogSeverity
	{
		/// <summary>Log as a debugging message.</summary>
		Debug = 0,
		/// <summary>Log as an informational message.</summary>
		Information = 1,
		/// <summary>Log as a warning message.</summary>
		Warning = 2,
		/// <summary>Log as an error message.</summary>
		Error = 3,
		/// <summary>Log as a fatal message.</summary>
		Fatal = 4
	}

	#endregion

	/// <summary>Base class to handle logging using an event handler.</summary>
	public class LoggerEvent
	{
		/// <summary>Logging event handler.</summary>
		public event EventHandler<LoggerEventArgs> RaiseLogHandler;

		#region Public Methods

		/// <summary>Logs a message by using a RaiseLogHandler event.</summary>
		/// <param name="message">Message text.</param>
		/// <param name="severity">Identifies the type of trace event.</param>
		public virtual void RaiseLogEvent( string message, LogSeverity severity = LogSeverity.Information )
		{
			// Check required parameter
			if( null == message )
			{
				message = string.Empty;
			}

			var handler = RaiseLogHandler;
			if( null != handler )
			{
				// Raise the log event if a handler has been set
				handler.Invoke( this, new LoggerEventArgs( message, severity ) );
			}
			else
			{
				// Display the message when a logger has not been set
				Console.WriteLine( severity + @": " + message );
			}
		}

		/// <summary>Logs a status message by using a RaiseLogHandler event.</summary>
		/// <param name="message">Message text.</param>
		/// <param name="args">Formatting values.</param>
		public virtual void LogStatus( string message, params object[] args )
		{
			RaiseLogEvent( string.Format( message, args ) );
		}

		#endregion

		#region Operations Timing

		private readonly Stopwatch _sw = new Stopwatch();

		private static string FormatMessage( string msg )
		{
			return $"Timing.: {msg.Trim()}";
		}

		private string FormatTimeSpan()
		{
			return $"Elapsed: {_sw.Elapsed}";
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		/// <param name="severity">Identifies the type of trace event. The default is debug.</param>
		public virtual void ResetTimer( LogSeverity severity = LogSeverity.Debug )
		{
			// Log the elapse time
			RaiseLogEvent( FormatTimeSpan(), severity );

			_sw.Reset();
		}

		/// <summary>
		/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
		/// </summary>
		/// <param name="message">Message text.</param>
		/// <param name="severity">Identifies the type of trace event. The default is debug.</param>
		public virtual void RestartTimer( string message = null, LogSeverity severity = LogSeverity.Debug )
		{
			// Log the elapse time
			if( _sw.IsRunning )
			{
				RaiseLogEvent( FormatTimeSpan(), severity );
			}

			// Log the start message
			if( !string.IsNullOrEmpty( message ) )
			{
				RaiseLogEvent( FormatMessage( message ), severity );
			}

			_sw.Restart();
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		/// <param name="message">Message text.</param>
		/// <param name="severity">Identifies the type of trace event. The default is debug.</param>
		public virtual void StartTimer( string message = null, LogSeverity severity = LogSeverity.Debug )
		{
			// Log the start message
			if( !string.IsNullOrWhiteSpace( message ) )
			{
				RaiseLogEvent( FormatMessage( message ), severity );
			}

			_sw.Start();
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		/// <param name="severity">Identifies the type of trace event. The default is debug.</param>
		public virtual void StopTimer( LogSeverity severity = LogSeverity.Debug )
		{
			_sw.Stop();

			// Log the elapse time
			RaiseLogEvent( FormatTimeSpan(), severity );
		}

		#endregion
	}
}