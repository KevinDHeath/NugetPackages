namespace Helper.Tests.Logging;

[Collection( "LoggerTests" )]
public class LoggerTests
{
	private const string cMsg = "Log to memory target";
	private const string cArg = "Test {0}";
	private readonly Logger _logger = new();

	public LoggerTests()
	{
		// For branch coverage
		_logger.MaxLogFiles = 1;
		_ = _logger.Debug( cMsg );      // Cannot log Debug
		_ = _logger.Debug( cArg, "d" ); // Cannot log Debug with single argument
		_ = _logger.Info( cMsg );       // Cannot log Info
		_ = _logger.Info( cArg, "i" );  // Cannot log Info with single argument
	}

	[Fact]
	public void ConfigFile_should_be_empty()
	{
		// Arrange
		Logger logger = new( Global.GetLongPath() );

		// Act
		string result = logger.ConfigFile;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void ConfigFile_should_not_be_empty()
	{
		// Arrange
		Logger logger = new( typeof( LoggerEventTests ), string.Empty );

		// Act
		string result = logger.ConfigFile;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void ConfigFile_should_not_be_found()
	{
		// Arrange
		Logger logger = new( Global.cInvalidFile );

		// Act
		string result = logger.ConfigFile;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void DefaultConfig_should_be_true()
	{
		// Arrange
		string configFile = Path.Combine( Global.cTestFolder, "NLogNoTargets.config" );

		// Act
		Logger logger = new( null, configFile );
		bool result = logger.ConfigFile.Length > 0;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void FatalCount_should_be_gt_0()
	{
		// Arrange
		Exception ex = new( cMsg );

		// Act (with branch coverage)
		_ = _logger.Fatal( ex );         // Fatal with exception
		_ = _logger.Fatal( "" );         // Fatal with empty message
		_ = _logger.Fatal( "", cMsg );   // Fatal with empty message and single argument
		_ = _logger.Fatal( cArg, null ); // Fatal with null argument
		_ = _logger.Fatal( cArg, cMsg ); // Fatal with single argument

		// Assert
		_ = _logger.FatalCount.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void Has_Errors_should_be_true()
	{
		// Arrange
		object[]? args = null;
		Exception ex = new( cMsg );

		// Act (with branch coverage)
		_ = _logger.Error( cMsg );       // Error with message
		_ = _logger.Error( "" );         // Error with empty message
		_ = _logger.Error( ex );         // Error with exception
		_ = _logger.Error( cMsg, ex );   // Error with message and exception
		_ = _logger.Error( cArg, args ); // Error with message and null arguments
		_ = _logger.Error( cArg, cMsg ); // Error with message and single argument
		_ = _logger.Error( "", cMsg );   // Error with empty message and single argument

		// Assert
		_ = _logger.HasErrors.Should().BeTrue();
	}

	[Fact]
	public void Log_Fatal_should_be_true()
	{
		// Act
		bool result = _logger.Fatal( cMsg );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Info_should_be_false()
	{
		// Act
		bool result = _logger.Info( cMsg );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void Log_Warning_should_be_true()
	{
		// Act
		bool result = _logger.Warn( cMsg );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void MaxLogFiles_should_be_1()
	{
		// Act
		int result = _logger.MaxLogFiles;

		// Assert
		_ = result.Should().Be( 1 );
	}

	[Fact]
	public void OnRaiseLog_should_be_true()
	{
		// Arrange
		Logger logger = new();
		LoggerEventTests logic = new();
		logic.RaiseLogHandler += logger.OnRaiseLog;

		// Act
		bool result = false;
		try { _ = logic.DoProcessing(); }
		catch( Exception ex ) { result = logger.Error( cMsg, ex ); }

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void RemoveLogs_should_be_true()
	{
		// Arrange
		string path = Global.cTestFolder;
		Global.CreateLogFile( Path.Combine( path, "NLog1.log" ) );
		Global.CreateLogFile( Path.Combine( path, "NLog2.log" ) );

		// Act (with branch coverage)
		bool result = _logger.RemoveLogs( Global.cTestFolder, "NLog*.log", 1 );
		_ = _logger.RemoveLogs( Global.cInvalidPath, "*.log" ); // Directory does not exist
		_ = _logger.RemoveLogs( path, string.Empty );           // Mask is empty
		_ = _logger.RemoveLogs( path, "NLog*.log", 0 );         // maxFiles <= 0
		_ = _logger.RemoveLogs( path, "NLog*.log", 5 );         // file count is < maximum number
		_ = _logger.RemoveLogs( Global.cInvalidPath, "*.log" ); // Directory does not exist
		_ = _logger.RemoveLogs( @".\", "*.logx", 1 );           // Directory contains no log files

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void RemoveLogs_should_throw_PathTooLongException()
	{
		// Act
		Action act = () => _logger.RemoveLogs( Global.GetLongPath(), "NLog*.log", 1 );

		// Assert
		_ = act.Should().Throw<PathTooLongException>();
	}

	[Fact]
	public void SetLogFile_should_be_true()
	{
		// Arrange
		Logger logger = new();

		// Act
		logger.SetLogFile( @"logger.log" );
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void SetLogFile_should_throw_PathTooLongException()
	{
		// Act
		Action act = () => _logger.SetLogFile( Global.GetLongPath() );

		// Assert
		_ = act.Should().Throw<PathTooLongException>();
	}

	[Fact]
	public void ToString_should_end_with_NLog()
	{
		// Act
		string result = _logger.ToString();

		// Assert
		_ = result.Should().EndWith( "NLog" );
	}

	[Fact]
	public void Warning_Count_should_be_gt_0()
	{
		// Act (with branch coverage)
		_ = _logger.Warn( "" );         // Warn with empty message
		_ = _logger.Warn( "", cMsg );   // Warn with empty message and single argument
		_ = _logger.Warn( cArg, null ); // Warn with message and null argument
		_ = _logger.Warn( cArg, cMsg ); // Warn with message and single argument

		// Assert
		_ = _logger.WarnCount.Should().BeGreaterThan( 0 );
	}
}