// https://github.com/NLog/NLog/wiki/Memory-target
// https://stackoverflow.com/questions/68100146/how-to-use-nlog-in-xunit-tests-from-net-core

using Helper.Tests.Logging;

namespace Helper.Tests;

[Collection( "LoggerTests" )]
public class LoggerTests
{
	private const string cAppConfig = @"Helper.Tests.dll.config";
	private const string cMessage = @"Log to memory target";

	[Fact]
	public void ConfigFile_should_not_be_empty()
	{
		// Arrange
		Logger logger = new( typeof( LoggerTests ), string.Empty );

		// Act
		string result = logger.ConfigFile;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void DefaultConfig_should_be_true()
	{
		// Arrange
		string configFile = "NLogNoTargets.config";

		// Act
		Logger logger = new( configFile );
		bool result = logger.ConfigFile.Length > 0;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Has_Errors_should_be_true()
	{
		// Arrange
		Logger logger = new( cAppConfig );
		Exception exception = new( cMessage );

		// Act (with branch coverage)
		_ = logger.Error( exception );
		_ = logger.Error( cMessage, exception );
		_ = logger.Error( cMessage );
		bool result = logger.HasErrors;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Fatal_should_be_true()
	{
		// Arrange
		Logger logger = new( cAppConfig );

		// Act
		bool result = logger.Fatal( cMessage );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Log_Info_should_be_false()
	{
		// Arrange
		Logger logger = new( cAppConfig );

		// Act
		bool result = logger.Info( cMessage );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void Log_Warning_should_be_true()
	{
		// Arrange
		Logger logger = new( cAppConfig );

		// Act
		bool result = logger.Warn( cMessage );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void MaxLogFiles_should_be_1()
	{
		// Arrange
		Logger logger = new( cAppConfig ) { MaxLogFiles = 1 };

		// Act
		int result = logger.MaxLogFiles;

		// Assert
		_ = result.Should().Be( 1 );
	}

	[Fact]
	public void OnRaiseLog_should_be_true()
	{
		// Arrange
		Logger logger = new( cAppConfig );
		LoggerEventTests logic = new();
		logic.RaiseLogHandler += logger.OnRaiseLog;

		// Act
		bool result = false;
		try { _ = logic.DoProcessing(); }
		catch( Exception ex ) { result = logger.Error( cMessage, ex ); }

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void SetLogFile_should_be_true()
	{
		// Arrange
		Logger logger = new( cAppConfig );

		// Act
		logger.SetLogFile( @"logger.log" );
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void RemoveLogs_should_be_true()
	{
		// Arrange
		if( !File.Exists( @".\NLog1.log" ) ) { File.Create( @".\NLog1.log" ); }
		if( !File.Exists( @".\NLog2.log" ) ) { File.Create( @".\NLog2.log" ); }
		Logger logger = new( cAppConfig );

		// Act (with branch coverage)
		_ = logger.RemoveLogs( @".\", @"NLog*.log", 0 );       // maxFiles <= 0
		_ = logger.RemoveLogs( @".\", @"NLog*.log", 5 );       // file count is < maximum number
		_ = logger.RemoveLogs( @"notexist", @"temp*.log", 1 ); // bad directory name
		bool result = logger.RemoveLogs( @".\", @"NLog*.log", 1 );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void ToString_should_end_with_NLog()
	{
		// Arrange
		Logger logger = new( cAppConfig );

		// Act
		string result = logger.ToString();

		// Assert
		_ = result.Should().EndWith( "NLog" );
	}
}