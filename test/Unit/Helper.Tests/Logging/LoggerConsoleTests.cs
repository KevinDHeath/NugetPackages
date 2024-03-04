namespace Helper.Tests.Logging;

[Collection( "LoggerTests" )]
public class LoggerConsoleTests
{
	private readonly Logger _logger = new( Path.Combine( Global.cTestFolder, "NLog.config" ) );

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
	}

	[Fact]
	public void Log_Debug_should_be_true()
	{
		// Act
		bool result = _logger.Log( cMsg, LogSeverity.Debug );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Error_exception_should_be_true()
	{
		// Act
		bool result = _logger.Error( new Exception( cMsg ) );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Fatal_should_be_true()
	{
		// Act
		bool result = _logger.Log( cMsg, LogSeverity.Fatal );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Info_should_be_true()
	{
		// Act
		bool result = _logger.Log( cMsg, LogSeverity.Information );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Warn_should_should_be_true()
	{
		// Act
		bool result = _logger.Log( cMsg, LogSeverity.Warning );

		// Assert
		_ = result.Should().BeTrue();
	}
}