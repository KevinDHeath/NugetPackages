namespace Helper.Tests;

public class ConsoleLog
{
	#region Override Console to StringWriter
	private readonly StringWriter _sw;

	public ConsoleLog()
	{
		_sw = new();
		Console.SetOut( _sw );
	}

	~ConsoleLog()
	{
		_sw.Flush();
		_sw.Close();
		_sw.Dispose();

		// Recover the standard output stream
		StreamWriter so = new( Console.OpenStandardOutput() );
		Console.SetOut( so );
	}
	#endregion

	private readonly object _lock = new();

	internal string CaptureLog( Logger logger, LogSeverity logType )
	{
		string msg = "Capture console log";
		lock( _lock )
		{
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
			return Capture();
		}
	}

	internal string Capture()
	{
		_sw.Flush();
		string result = _sw.GetStringBuilder().ToString();
		_sw.GetStringBuilder().Clear();
		return result;
	}
}