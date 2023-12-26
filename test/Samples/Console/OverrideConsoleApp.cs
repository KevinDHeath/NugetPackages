using Application.Helper;

namespace Sample.Console;

/// <inheritdoc/>
public class OverrideConsoleApp : ConsoleApp
{
	/// <inheritdoc/>
	public override void ShowProgramInfo()
	{
		base.ShowProgramInfo();
		System.Console.WriteLine( "Specific information requested for derived class." );
	}
}