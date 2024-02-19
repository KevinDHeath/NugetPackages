global using FluentAssertions;
global using Xunit;
global using Application.Helper;
global using Configuration.Helper;
global using Logging.Helper;

namespace Helper.Tests;

internal class Global : LoggerEvent
{
	internal const string cTestFolder = @".\Testdata";
	internal const string cConfigFileHelper = "ConfigFileHelper";
	internal const string cInvalidFile = "bad.config";
	internal const string cInvalidPath = @".\baddir";

	internal static void CreateLogFile( string fileName )
	{
		if( !File.Exists( fileName ) ) { using FileStream fs = File.Create( fileName ); }
	}

	internal static string GetLongPath( string fileName = "" )
	{
		return new string( 'a', 32768 ) + fileName;
	}

	internal static Exception GetException()
	{
		int div = 0;
		try { div = 10 / div; }
		catch( Exception ex ) { return ex; }
		return new Exception();
	}
}