using Configuration.Helper;

namespace Sample.Console;

internal class TestNuGet
{
	internal static readonly int sTest = 1; // 0=Application, 1=Configuration, 2=Logging

	internal static bool RunTest( int runType )
	{
		return runType switch
		{
			0 => TestApplication(),
			1 => TestConfiguration(),
			2 => TestLogging(),
			_ => false,
		};
	}

	#region Test Application Helper

	private static bool TestApplication()
	{
		System.Console.WriteLine();
		Program.sLogger.Log( "Test of Application.Helper" );

		// Show the properties
		Program.sLogger.Log( $"Config file is {Program.sApp.ConfigFile}" );
		Program.sLogger.Log( $"Debug mode is {Program.sApp.DebugMode}" );
		Program.sLogger.Log( $"Elapsed time is {Program.sApp.ElapsedTime}" );
		Program.sLogger.Log( $"Help request is {Program.sApp.HelpRequest}" );
		Program.sLogger.Log( $"Program title is {Program.sApp.Title}" );

		return true;
	}

	#endregion

	#region Test Configuration Helper

	private static bool TestConfiguration()
	{
		System.Console.WriteLine();
		Program.sLogger.Log( "Test of Configuration.Helper" );

		// Load the configuration settings
		var configFile = GetConfigFile();
		var config = ConfigFileHelper.GetConfiguration( configFile );

		// Get a setting value from the AppSettings section
		var director = config.GetSetting( @"favoritedirector" );

		// Get a setting value from the ConnectionStrings section
		var connect = config.ConnectionStrings.GetSetting( "movies" );

		// Get a setting value from the Custom section
		var password = config.GetSection( "custom" ).GetSetting( "password" );

		// Add or update a setting value in the AppSettings section
		var ok = config.AddSetting( "FavouriteActress", "Julia Roberts" );
		if( ok ) ok = config.AddSetting( @"favouriteactress", "Angelina Jolie" );
		var actress = config.GetSetting( @"favouriteactress" );

		// Show the setting values
		Program.sLogger.Log( $"Database is {connect}" );
		Program.sLogger.Log( $"My favorite director is {director}" );
		Program.sLogger.Log( $"My favorite actress is {actress}" );
		Program.sLogger.Log( $"My secure password is {password}" );

		return ok;
	}

	internal static string GetConfigFile( string extension = ConfigFileHelper.cJsonExtension )
	{
		// Check for specific configuration file
		string configFile = Path.Combine( "Configuration", Environment.MachineName + extension );
		if( File.Exists( configFile ) ) return configFile;

		return string.Empty;
	}

	#endregion

	#region Test Logging Helper

	private static bool TestLogging()
	{
		System.Console.WriteLine();
		Program.sLogger.Info( "Test of Logging.Helper" );

		// Show the setting values
		Program.sLogger.Warn( "Logging a warning message" );

		// Create an object from a class that inherits LoggerEvent
		var logicClass = new ProcessingClass();
		logicClass.RaiseLogHandler += Program.sLogger.OnRaiseLog;

		// Do Processing
		var res = logicClass.DoProcessing();

		return -1 == res;
	}

	#endregion
}