using System.Windows;

namespace TestWPF;

/// <summary>Interaction logic for App.xaml</summary>
public partial class App : Application
{
	/// <summary>Gets or sets the global font size.</summary>
	public static double GlobalFontSize
	{
		get => (double)Current.Resources["Common.FontSize"];
		set => Current.Resources["Common.FontSize"] = value;
	}

	/// <summary>Raises the Startup event.</summary>
	/// <param name="e">A StartupEventArgs that contains the event data.</param>
	protected override void OnStartup( StartupEventArgs e )
	{
		GlobalFontSize = 14;
		base.OnStartup( e );
	}
}