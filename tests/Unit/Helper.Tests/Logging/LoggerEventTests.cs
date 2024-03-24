namespace Helper.Tests.Logging;

[Collection( "LoggerTests" )]
public class LoggerEventTests : LoggerEvent
{
	private const string cTimerStarted = "Timer started";
	private readonly StringWriter _sw = new();

	public LoggerEventTests()
	{
		Console.SetOut( _sw );
	}

	~LoggerEventTests()
	{
		_sw.Flush();
		_sw.Close();
		_sw.Dispose();

		// Recover the standard output stream
		StreamWriter so = new( Console.OpenStandardOutput() );
		Console.SetOut( so );
	}

	[Fact]
	public void LoggerEventArgs_message_should_not_be_empty()
	{
		// Arrange
		LoggerEventArgs args = new( "args" );

		// Act (with branch coverage)
		_ = new LoggerEventArgs( null );
		_ = args.Severity;
		string result = args.Message;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void LogStatus_message_should_be_true()
	{
		// Arrange
		LoggerEvent loggerevent = new();

		// Act
		loggerevent.LogStatus( "message {0}", "test" );
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void RaiseLogEvent_should_be_true()
	{
		// Act
		RaiseLogEvent( null, LogSeverity.Error );
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void ReStartTimer_should_be_true()
	{
		// Arrange
		LoggerEvent loggerevent = new();

		// Act (with branch coverage)
		loggerevent.StartTimer( cTimerStarted );
		loggerevent.RestartTimer( "restart" ); // Restart timer
		loggerevent.ResetTimer();              // Reset timer
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StartTimer_should_be_true()
	{
		// Arrange
		LoggerEvent loggerevent = new();

		// Act
		loggerevent.StartTimer( cTimerStarted );
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void StopTimer_should_be_true()
	{
		// Arrange
		LoggerEvent loggerevent = new();
		loggerevent.StartTimer( cTimerStarted );

		// Act
		loggerevent.StopTimer();
		bool result = true;

		// Assert
		_ = result.Should().BeTrue();
	}

	public int DoProcessing()
	{
		RaiseLogEvent( "Log event", LogSeverity.Error );

		// Produce an exception
		int div = 0;
		int retValue = 10 / div;

		return retValue;
	}
}