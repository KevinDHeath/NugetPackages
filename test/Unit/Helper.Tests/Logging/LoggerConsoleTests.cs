namespace Helper.Tests.Logging;

[Collection( "LoggerTests" )]
public class LoggerConsoleTests
{
	private readonly Logger _logger = new( Path.Combine( Global.cTestFolder, "NLog.config" ) );
	private readonly ConsoleLog _console;

	private const string cMsg = "Log to console target";
	private const string cArg = "Test {0}";

	public LoggerConsoleTests()
	{
		// For branch coverage
		_ = _logger.Debug( "" );         // Debug with empty string
		_ = _logger.Debug( "", cMsg );   // Debug with empty string and single argument
		_ = _logger.Debug( cArg, null ); // Debug with null argument
		_ = _logger.Debug( cArg, cMsg ); // Debug with single argument
		_ = _logger.Info( "" );          // Info with empty string
		_ = _logger.Info( cArg, null );  // Info with null argument
		_ = _logger.Info( cArg, cMsg );  // Info with single argument
		_ = _logger.Info( "", cMsg );    // Info with empty string and single argument

		_console = new();
	}

	[Fact]
	public void Log_Debug_should_start_with_DEBUG()
	{
		// Act
		string result = _console.CaptureLog( _logger, LogSeverity.Debug );

		// Assert
		_ = result.Should().StartWith( "DEBUG" );
	}

	[Fact]
	public void Log_Error_exception_should_start_with_ERROR()
	{
		// Act
		string result = _console.CaptureLog( _logger, LogSeverity.Error );

		// Assert
		_ = result.Should().StartWith( "ERROR" );
		//_ = result.Length.Should().BeGreaterThanOrEqualTo( 0 ); // if issues with override
	}

	[Fact]
	public void Log_Fatal_exception_should_start_with_FATAL()
	{
		// Act
		string result = _console.CaptureLog( _logger, LogSeverity.Fatal );

		// Assert
		_ = result.Should().StartWith( "FATAL" );
		//_ = result.Length.Should().BeGreaterThanOrEqualTo( 0 ); // if issues with override
	}

	[Fact]
	public void Log_Info_should_start_with_INFO()
	{
		// Act
		string result = _console.CaptureLog( _logger, LogSeverity.Information );

		// Assert
		_ = result.Should().StartWith( "INFO" );
		//_ = result.Length.Should().BeGreaterThanOrEqualTo( 0 ); // if issues with override
	}

	[Fact]
	public void Log_Warn_should_start_with_WARN()
	{
		// Act
		string result = _console.CaptureLog( _logger, LogSeverity.Warning );

		// Assert
		_ = result.Should().StartWith( "WARN" );
		//_ = result.Length.Should().BeGreaterThanOrEqualTo( 0 ); // if issues with override
	}
}