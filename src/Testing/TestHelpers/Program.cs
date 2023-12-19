using System;
using Logging.Helper;

namespace TestHarness;

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

		if( 0 == TestNuGet.sTest && args.Length == 0 ) { args = ["/?"]; }
		var result = false;
		try
		{
			sApp.StartApp( args );
			if( 0 == TestNuGet.sTest ) Console.WriteLine();
			sLogger.Info( sApp.FormatTitleLine( " Starting " + sApp.Title + " " ) );
			result = RunTest();
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

	private static bool RunTest()
	{
		if( TestNuGet.RunTest() ) { return true; }

		return false;
	}
}