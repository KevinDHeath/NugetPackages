namespace Helper.Tests.Application;

public class ConsoleAppTests
{
	private readonly ConsoleApp _testApp = new( detectDebugMode: false ) { IsUnitTest = true };

	public ConsoleAppTests()
	{
		_testApp.StartApp();
	}

	~ConsoleAppTests()
	{
		_testApp.StopApp();
	}

	[Fact]
	public void ConfigFile_should_be_empty()
	{
		// Act
		string result = _testApp.ConfigFile;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void ConfigFile_should_not_be_empty()
	{
		// Assign
		string configFile = Global.cMachineConfig + ConfigFileHelper.cJsonExtension;
		ConsoleApp app = new( Path.Combine( Global.cTestFolder, configFile ) );

		// Act
		string result = app.ConfigFile;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void DebugMode_should_be_true()
	{
		// Act
		bool result = _testApp.DebugMode;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void ElapsedTime_should_be_positive()
	{
		// Act
		TimeSpan result = _testApp.ElapsedTime;

		// Assert
		_ = result.Should().BePositive();
	}

	[Fact]
	public void FormatTitleLine_length_should_be_80()
	{
		// Act (with branch coverage)
		string result = _testApp.FormatTitleLine( "Unit Testing" ); // Text with even number of chars
		_ = _testApp.FormatTitleLine( null );                       // Null text
		_ = _testApp.FormatTitleLine( new string( 'a', 81 ) );      // Text greater than maxWidth
		_ = _testApp.FormatTitleLine( new string( 'a', 11 ) );      // Text with odd number of chars

		// Assert
		_ = result.Length.Should().Be( 80 );
	}

	[Fact]
	public void HelpRequest_should_be_true()
	{
		// Arrange
		ConsoleApp app = new();
		app.StartApp( ["?"] );

		// Act
		bool result = app.HelpRequest;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StartApp_should_already_be_started()
	{
		// Arrange
		ConsoleApp app = new();
		app.StartApp( null );

		// Act (with branch coverage)
		bool result = app.StartApp();
		app.StartApp( null );  // Already started with args

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StartApp_with_args_should_already_be_started()
	{
		// Arrange
		ConsoleApp app = new();
		app.StartApp( ["1"] ); // Start with single arg

		// Act
		bool result = app.StartApp();

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StopApp_should_be_true()
	{
		// Arrange
		ConsoleApp app = new();
		_ = app.StartApp();

		// Act
		app.StopApp();
		bool result = Environment.ExitCode == 0;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StopApp_with_fail_should_be_true()
	{
		// Arrange
		ConsoleApp app = new();
		_ = app.StartApp();

		_testApp.StopApp( false );  // Stop with failure

		// Act (with branch coverage)
		app.StopApp( false );
		bool result = Environment.ExitCode > 0;
		Environment.ExitCode = 0; // Reset exit code
		app.StopApp();            // Already stopped

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Title_should_contain_Version()
	{
		// Act
		string result = _testApp.Title;

		// Assert
		_ = result.Should().ContainEquivalentOf( "Version" );
	}
}