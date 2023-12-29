global using Sample.Mvvm.ViewModels;
global using Sample.Mvvm.Services;
global using Sample.Mvvm.Stores;

using Microsoft.Extensions.DependencyInjection;

namespace Sample.Mvvm.Services;

public class RegisterServices
{
	public const string cAppSettings = "appsettings.json";

	public static IServiceCollection Build( string appSettings = cAppSettings )
	{
		IServiceCollection rtn = new ServiceCollection();

		// Register Stores
		_ = rtn.AddSingleton<AccountStore>();
		_ = rtn.AddSingleton( new SettingsStore( appSettings ) );
		_ = rtn.AddSingleton( s => new UserStore( s.GetRequiredService<SettingsStore>() ) );
		_ = rtn.AddSingleton<NavigationStore>();
		_ = rtn.AddSingleton<ModalNavigationStore>();

		// Register ViewModels
		_ = rtn.AddSingleton<MainViewModel>();
		_ = rtn.AddTransient( s => new CommandTestViewModel( s.GetRequiredService<AccountStore>() ) );
		_ = rtn.AddTransient( s => new CustomTestViewModel( s.GetRequiredService<AccountStore>() ) );
		_ = rtn.AddTransient( s => new HomeViewModel( CreateLoginNavigationService( s ) ) );
		_ = rtn.AddTransient( CreateLoginViewModel );
		_ = rtn.AddTransient( s => new UnitTestViewModel( s.GetRequiredService<SettingsStore>() ) );
		_ = rtn.AddTransient( s => new UserListViewModel( s.GetRequiredService<UserStore>(),
				CreateAddUserNavigationService( s ) ) );
		_ = rtn.AddTransient( s => new UserViewModel( s.GetRequiredService<UserStore>(),
				s.GetRequiredService<CloseModalNavigationService>() ) );
		_ = rtn.AddTransient( CreateNavigationBarViewModel );

		// Register Services
		_ = rtn.AddSingleton<CloseModalNavigationService>();
		_ = rtn.AddSingleton( CreateHomeNavigationService );

		return rtn;
	}

	private static LoginViewModel CreateLoginViewModel( IServiceProvider sp )
	{
		CompositeNavigationService navigationService =
			new( sp.GetRequiredService<CloseModalNavigationService>(),
			CreateUnitTestNavigationService( sp ) );

		return new LoginViewModel( sp.GetRequiredService<AccountStore>(),
			sp.GetRequiredService<UserStore>(),
			navigationService );
	}

	private static NavigationBarViewModel CreateNavigationBarViewModel( IServiceProvider sp )
	{
		return new NavigationBarViewModel( sp.GetRequiredService<AccountStore>(),
			CreateHomeNavigationService( sp ),
			CreateCommandTestNavigationService( sp ),
			CreateLoginNavigationService( sp ),
			CreateUserListNavigationService( sp ),
			CreateUnitTestNavigationService( sp ),
			CreateCustomTestNavigationService( sp ) );
	}

	private static ModalNavigationService<UserViewModel> CreateAddUserNavigationService( IServiceProvider sp )
	{
		return new ModalNavigationService<UserViewModel>(
			sp.GetRequiredService<ModalNavigationStore>(),
			() => sp.GetRequiredService<UserViewModel>() );
	}

	private static LayoutNavigationService<HomeViewModel> CreateHomeNavigationService( IServiceProvider sp )
	{
		return new LayoutNavigationService<HomeViewModel>( sp.GetRequiredService<NavigationStore>(),
			() => sp.GetRequiredService<HomeViewModel>(),
			() => sp.GetRequiredService<NavigationBarViewModel>() );
	}

	private static LayoutNavigationService<CommandTestViewModel> CreateCommandTestNavigationService( IServiceProvider sp )
	{
		return new LayoutNavigationService<CommandTestViewModel>( sp.GetRequiredService<NavigationStore>(),
			() => sp.GetRequiredService<CommandTestViewModel>(),
			() => sp.GetRequiredService<NavigationBarViewModel>() );
	}

	private static ModalNavigationService<LoginViewModel> CreateLoginNavigationService( IServiceProvider sp )
	{
		return new ModalNavigationService<LoginViewModel>( sp.GetRequiredService<ModalNavigationStore>(),
			() => sp.GetRequiredService<LoginViewModel>() );
	}

	private static LayoutNavigationService<UserListViewModel> CreateUserListNavigationService( IServiceProvider sp )
	{
		return new LayoutNavigationService<UserListViewModel>(
			sp.GetRequiredService<NavigationStore>(),
			() => sp.GetRequiredService<UserListViewModel>(),
			() => sp.GetRequiredService<NavigationBarViewModel>() );
	}

	private static LayoutNavigationService<UnitTestViewModel> CreateUnitTestNavigationService( IServiceProvider sp )
	{
		return new LayoutNavigationService<UnitTestViewModel>( sp.GetRequiredService<NavigationStore>(),
			() => sp.GetRequiredService<UnitTestViewModel>(),
			() => sp.GetRequiredService<NavigationBarViewModel>() );
	}

	private static LayoutNavigationService<CustomTestViewModel> CreateCustomTestNavigationService(
		IServiceProvider sp )
	{
		return new LayoutNavigationService<CustomTestViewModel>( sp.GetRequiredService<NavigationStore>(),
			() => sp.GetRequiredService<CustomTestViewModel>(),
			() => sp.GetRequiredService<NavigationBarViewModel>() );
	}
}