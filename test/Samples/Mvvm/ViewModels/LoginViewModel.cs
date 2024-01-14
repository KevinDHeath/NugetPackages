using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Common.Wpf.Commands;
using Sample.Mvvm.Models;

namespace Sample.Mvvm.ViewModels;

public class LoginViewModel : ViewModelBase
{
	#region Properties

	private string _name = string.Empty;
	private string _password = string.Empty;

	[Required]
	public string Name
	{
		get => _name;
		set
		{
			ValidateProperty( value );
			_name = value;
			OnPropertyChanged();
		}
	}

	[Required]
	public string Password
	{
		get => _password;
		set
		{
			ValidateProperty( value );
			_password = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Commands

	public ICommand LoginCommand { get; }

	public ICommand CancelCommand { get; }

	private bool CanLogin( object? _ ) => !HasErrors;

	private void DoLogin( object? _ )
	{
		string email = $"{Name}@test.com".Replace( " ", string.Empty ).ToLower();
		User? user = _userStore.Users.FirstOrDefault( n => n.Name.StartsWith( Name, Settings.cCompare ) );
		if( user is not null )
		{
			Name = user.Name;
			email = user.Email;
		}
		else { user = new User(); }

		Account account = new() { User = Name, Email = email, Login = user };
		_accountStore.CurrentAccount = account;
		_navigation.Navigate();
	}

	private void DoCancel( object? _ ) => _navigation.Navigate();

	#endregion

	private readonly AccountStore _accountStore;
	private readonly UserStore _userStore;
	private readonly INavigationService _navigation;

	public LoginViewModel( AccountStore accountStore, UserStore userStore,
		INavigationService loginNavigationService )
	{
		_accountStore = accountStore;
		_userStore = userStore;
		_navigation = loginNavigationService;

		LoginCommand = new RelayCommand( CanLogin, DoLogin );
		CancelCommand = new RelayCommand( _ => true, DoCancel );

		ValidateAllProperties();
	}
}