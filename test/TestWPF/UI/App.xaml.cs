using System.Windows;
using TestWPF.Core.Models;
using TestWPF.Core.ViewModels;

namespace TestWPF;

/// <summary>Interaction logic for App.xaml</summary>
public partial class App : Application
{
	#region Properties

	// 1. Add a double resource into the Application resources
	// 2. Or define the default font size in am appsettings.json file
	// Use this app resource as a DynamicResource anywhere it's needed
	//   FontSize="{DynamicResource Common.FontSize}"

	public static double AppFontSize
	{
		get => (double)Current.Resources["Common.FontSize"];
		private set => Current.Resources["Common.FontSize"] = value;
	}

	private static ValidationViewModel? _controlsvm;
	internal static ValidationViewModel ValidationVM => _controlsvm ??= new ValidationViewModel();

	public static Settings? Settings { get; private set; } = new Settings();

	#endregion

	protected override void OnStartup( StartupEventArgs e )
	{
		Settings? settings = Settings.Load( "appsettings.json" );
		if( settings is not null && settings.FontSize is not null )
		{
			Settings = settings;
			if( Settings.FontSize is not null ) { ChangeFontSize( Settings.FontSize.Value ); }
		}

		base.OnStartup( e );
	}

	private const double cMinFontSize = 10;
	private const double cMaxFontSize = 18;

	public static void ChangeFontSize( double value )
	{
		// Font size can be between 10 and 18
		if( value != AppFontSize && value >= cMinFontSize && value <= cMaxFontSize )
		{
			AppFontSize = value;
		}
	}
}