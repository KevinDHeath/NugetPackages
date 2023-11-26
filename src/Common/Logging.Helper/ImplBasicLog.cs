using System;

namespace Logging.Helper
{
	/// <summary>Class that implements a basic Console logger.</summary>
	internal class ImplBasicLog : ILog
	{
		#region ILog Implementation

		#region Properties

		/// <inheritdoc/>
		public int MaxLogFiles { get; set; }

		/// <inheritdoc/>
		public bool IsDebugEnabled => true;

		/// <inheritdoc/>
		public bool IsErrorEnabled => true;

		/// <inheritdoc/>
		public bool IsFatalEnabled => true;

		/// <inheritdoc/>
		public bool IsInfoEnabled => true;

		/// <inheritdoc/>
		public bool IsWarnEnabled => true;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public void Debug( string message )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Debug ) + message );
		}

		/// <inheritdoc/>
		public void Error( string message )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Error ) + message );
		}

		/// <inheritdoc/>
		public void Error( string message, Exception exception )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Error ) + message );
		}

		/// <inheritdoc/>
		public void Error( Exception exception )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Error ) + exception );
		}

		/// <inheritdoc/>
		public void Fatal( string message )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Fatal ) + message );
		}

		/// <inheritdoc/>
		public void Info( string message )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Information ) + message );
		}

		/// <inheritdoc/>
		public void Warn( string message )
		{
			Console.WriteLine( GetLayoutPrefix( LogSeverity.Warning ) + message );
		}

		/// <inheritdoc/>
		public void SetLogFile( string logDirectory, string logFileName = "" )
		{
			// Not implemented
		}

		#endregion

		#endregion

		#region Private Methods

		private static string GetLayoutPrefix( LogSeverity severity )
		{
			var time = DateTime.Now.ToString( @"HH:mm:ss" );

			switch( severity )
			{
				case LogSeverity.Debug:
					return time + " DEBUG ";
				case LogSeverity.Error:
					return time + " ERROR ";
				case LogSeverity.Fatal:
					return time + " FATAL ";
				case LogSeverity.Information:
					return time + " INFO  ";
				case LogSeverity.Warning:
					return time + " WARN  ";
				default:
					return time + " TRACE ";
			}
		}

		#endregion
	}
}