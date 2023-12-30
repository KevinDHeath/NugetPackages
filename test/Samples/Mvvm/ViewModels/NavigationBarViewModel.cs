using System.Windows.Input;
using Sample.Mvvm.Commands;

namespace Sample.Mvvm.ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
	private readonly AccountStore _accountStore;
	private bool _isMenuOpen;

	public ICommand NavigateHomeCommand { get; }

	public ICommand NavigateCommandTestCommand { get; }

	public ICommand NavigateLoginCommand { get; }

	public ICommand NavigateUserListCommand { get; }

	public ICommand NavigateUnitTestCommand { get; }

	public ICommand NavigateCustomTestCommand { get; }

	public ICommand LogoutCommand { get; }

	public bool IsLoggedIn => _accountStore.IsLoggedIn;

	public bool IsLoggedOut => !_accountStore.IsLoggedIn;

	public bool IsMenuOpen
	{
		get => _isMenuOpen;
		set
		{
			if( value.Equals( _isMenuOpen ) ) return;
			_isMenuOpen = value;
			OnPropertyChanged();
		}
	}

	public NavigationBarViewModel( AccountStore accountStore,
		INavigationService homeNavigationService,
		INavigationService accountNavigationService,
		INavigationService loginNavigationService,
		INavigationService userListNavigationService,
		INavigationService userUnitTestNavigationService,
		INavigationService customNavigationService )
	{
		_accountStore = accountStore;
		NavigateHomeCommand = new NavigateCommand( homeNavigationService );
		NavigateCommandTestCommand = new NavigateCommand( accountNavigationService );
		NavigateLoginCommand = new NavigateCommand( loginNavigationService );
		NavigateUserListCommand = new NavigateCommand( userListNavigationService );
		NavigateUnitTestCommand = new NavigateCommand( userUnitTestNavigationService );
		NavigateCustomTestCommand = new NavigateCommand( customNavigationService );
		LogoutCommand = new LogoutCommand( _accountStore );

		_accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
	}

	private void OnCurrentAccountChanged()
	{
		OnPropertyChanged( nameof( IsLoggedIn ) );
		OnPropertyChanged( nameof( IsLoggedOut ) );
	}

	public override void Dispose()
	{
		_accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

		base.Dispose();
	}
}