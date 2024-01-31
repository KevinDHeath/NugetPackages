using Application.Helper;

namespace Sample.Console;

/// <inheritdoc/>
public class OverrideConsoleApp : ConsoleApp
{
	/// <inheritdoc/>
	public override void ShowProgramInfo()
	{
		base.ShowProgramInfo();
		System.Console.WriteLine( "Specific information requested from derived class." );
		System.Console.WriteLine( "Argument values:" );
		System.Console.WriteLine( "  0 = Run Application Helper test" );
		System.Console.WriteLine( "  1 = Run Configuration Helper test" );
		System.Console.WriteLine( "  2 = Run Logging Helper test" );
		System.Console.WriteLine( "  /? or ? = Show this information" );

	}
}