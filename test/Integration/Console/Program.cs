using Logging.Helper;

namespace Sample.Console;

class Program
{
	// Important: Set the application settings file Property in project to:
	// Copy to Output Directory = Copy if newer

	internal static readonly OverrideConsoleApp sApp = new();
	internal static readonly Logger sLogger = new( typeof( Program ) );

	#region Program Entry Point

	static void Main( string[] args )
	{
		// To automatically close the console when debugging stops, in Visual Studio
		// enable Tools->Options->Debugging->Automatically close the console when debugging stops.

		int runType = TestNuGet.sTest;
		if( args.Length > 0 && int.TryParse( args[0], out int outInt ) ) { runType = outInt; }

		if( 0 == runType && args.Length == 0 ) { args = ["/?"]; }
		var result = false;
		try
		{
			sApp.StartApp( args );
			if( 0 == runType ) System.Console.WriteLine();
			if( sApp.HelpRequest ) { result = true; }
			else
			{
				sLogger.Info( sApp.FormatTitleLine( " Starting " + sApp.Title + " " ) );
				result = RunTest( runType );
			}
		}
		catch( Exception ex )
		{
			sLogger.Fatal( ex );
		}
		finally
		{
			sApp.StopApp( result );
		}

		Environment.Exit( Environment.ExitCode );
	}

	#endregion

	private static bool RunTest( int runType )
	{
		if( TestNuGet.RunTest( runType ) ) { return true; }

		return false;
	}
}