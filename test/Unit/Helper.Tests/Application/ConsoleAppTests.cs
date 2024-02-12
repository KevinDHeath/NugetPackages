namespace Helper.Tests.Application;

public class ConsoleAppTests
{
	[Fact]
	public void ConfigFile_should_be_empty()
	{
		// Arrange
		ConsoleApp app = new();

		// Act
		string result = app.ConfigFile;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void DebugMode_should_be_true()
	{
		// Arrange
		ConsoleApp app = new( detectDebugMode: false ) { IsUnitTest = true };

		// Act (with branch coverage)
		_ = app.StartApp();
		app.StopApp();
		bool result = app.DebugMode;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void FormatTitleLine_should_not_be_empty()
	{
		// Arrange
		ConsoleApp app = new();

		// Act
		string result = app.FormatTitleLine( "Unit Testing" );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void HelpRequest_should_be_true()
	{
		// Arrange
		ConsoleApp app = new();
		string[] args = ["?"];
		app.StartApp( args ); // THIS SHOULD RETURN BOOL

		// Act
		bool result = app.HelpRequest;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StopApp_should_be_true()
	{
		// Arrange
		ConsoleApp app = new();

		// Act (with branch coverage)
		app.StopApp();
		_ = app.StartApp();
		app.StopApp();
		bool result = Environment.ExitCode == 0;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Title_should_not_be_empty()
	{
		// Arrange
		ConsoleApp app = new();

		// Act
		string result = app.Title;

		// Assert
		_ = result.Should().NotBeEmpty();
	}
}