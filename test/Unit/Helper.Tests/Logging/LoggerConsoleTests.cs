namespace Helper.Tests.Logging;

[Collection( "LoggerTests" )]
public class LoggerConsoleTests
{
	private const string cNLogConfig = @"NLog.config";

	[Fact]
	public void Log_Debug_should_start_with_DEBUG()
	{
		// Arrange
		Logger logger = new( cNLogConfig );

		// Act
		_ = logger.Debug( "Test {0}", null );
		_ = logger.Debug( "Test {0}", "debug" );
		string result = Global.CaptureLog( logger, LogSeverity.Debug );

		// Assert
		_ = result.Should().StartWith( "DEBUG" );
	}

	[Fact]
	public void Log_Error_exception_should_start_with_ERROR()
	{
		// Arrange
		Logger logger = new( cNLogConfig );
		object[]? args = null;

		// Act
		_ = logger.Error( "Test {0}", args );
		_ = logger.Error( "Test {0}", "error" );
		string result = Global.CaptureLog( logger, LogSeverity.Error );

		// Assert
		_ = result.Should().StartWith( "ERROR" );
	}

	[Fact]
	public void Log_Fatal_exception_should_start_with_FATAL()
	{
		// Arrange
		Logger logger = new( cNLogConfig );
		Exception exception = new( "Fatal" );

		// Act
		_ = logger.Fatal( "Test {0}", null );
		_ = logger.Fatal( "Test {0}", "fatal" );
		_ = logger.Fatal( exception );
		string result = Global.CaptureLog( logger, LogSeverity.Fatal );

		// Assert
		_ = result.Should().StartWith( "FATAL" );
	}

	[Fact]
	public void Log_Info_should_start_with_INFO()
	{
		// Arrange
		Logger logger = new( cNLogConfig );

		// Act
		_ = logger.Info( "Test {0}", null );
		_ = logger.Info( "Test {0}", "info" );
		string result = Global.CaptureLog( logger, LogSeverity.Information );

		// Assert
		_ = result.Should().StartWith( "INFO" );
	}

	[Fact]
	public void Log_Warn_should_start_with_WARN()
	{
		// Arrange
		Logger logger = new( cNLogConfig );

		// Act
		_ = logger.Warn( "Test {0}", null );
		_ = logger.Warn( "Test {0}", "warn" );
		string result = Global.CaptureLog( logger, LogSeverity.Warning );

		// Assert
		_ = result.Should().StartWith( "WARN" );
	}
}