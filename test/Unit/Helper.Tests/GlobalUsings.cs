global using FluentAssertions;
global using Xunit;
global using Application.Helper;
global using Logging.Helper;

namespace Helper.Tests;

internal class Global : LoggerEvent
{
	internal static string CaptureLog( Logger logger, LogSeverity logType )
	{
		string msg = "Capture console log";
		using StringWriter stringWriter = new();
		Console.SetOut( stringWriter );

		switch( logType )
		{
			case LogSeverity.Error:
				Exception ex = new( msg );
				logger.Error( ex );
				break;
			default:
				logger.Log( msg, logType );
				break;
		}
		string result = stringWriter.ToString();

		// Recover the standard output stream
		StreamWriter standardOutput = new( Console.OpenStandardOutput() ) { AutoFlush = true };
		Console.SetOut( standardOutput );

		return result;
	}
}