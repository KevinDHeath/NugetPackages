global using Sample.Mvvm.ViewModels;

using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Sample.Mvvm.Models;
using Sample.Mvvm.Services;
using Sample.Mvvm.Stores;

namespace Sample.Wpf;

public partial class App : Application
{
	private readonly IServiceProvider _sp;

	public App()
	{
		IServiceCollection services = RegisterServices.Build();
		services.AddSingleton( s => new MainWindow()
		{
			DataContext = s.GetRequiredService<MainViewModel>()
		} );

		_sp = services.BuildServiceProvider();
	}

	protected override void OnStartup( StartupEventArgs e )
	{
		var fontSize = _sp.GetRequiredService<SettingsStore>().Settings.FontSize;
		ChangeFontSize( fontSize );

		// Set the initial View
		LayoutNavigationService<HomeViewModel> service =
			_sp.GetRequiredService<LayoutNavigationService<HomeViewModel>>();
		service.Navigate();

		MainWindow = _sp.GetRequiredService<MainWindow>();
		MainWindow.Show();

		base.OnStartup( e );
	}

	#region Change Font Size

	public static double AppFontSize
	{
		get => (double)Current.Resources["Common.FontSize"];
		private set => Current.Resources["Common.FontSize"] = value;
	}

	private const double cMinFontSize = 10;
	private const double cMaxFontSize = 18;

	public static void ChangeFontSize( double? value )
	{
		// Font size can be between 10 and 18
		if( value is not null && value != AppFontSize && value >= cMinFontSize && value <= cMaxFontSize )
		{
			AppFontSize = value.Value;
		}
	}

	#endregion
}