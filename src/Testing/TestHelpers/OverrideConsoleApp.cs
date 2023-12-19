using Application.Helper;

namespace TestHelpers;

/// <inheritdoc/>
public class OverrideConsoleApp : ConsoleApp
{
	/// <inheritdoc/>
	public override void ShowProgramInfo()
	{
		base.ShowProgramInfo();
		Console.WriteLine( "Specific information requested for derived class." );
	}
}