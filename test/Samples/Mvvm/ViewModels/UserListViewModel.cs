using System.Collections.ObjectModel;
using System.Windows.Input;
using Common.Wpf.Commands;
using Sample.Mvvm.Models;

namespace Sample.Mvvm.ViewModels;

public class UserListViewModel : ViewModelBase
{
	public ObservableCollection<User> UserList{  get; private set; }

	public User? CurrentUser
	{
		get => _userStore.CurrentUser;
		set
		{
			_userStore.CurrentUser = value;
			OnPropertyChanged();
		}
	}

	public ICommand AddUserCommand { get; }

	public ICommand EditUserCommand { get; }

	private readonly UserStore _userStore;
	private readonly INavigationService _navigationService;

	public UserListViewModel( UserStore userStore, INavigationService addUserNavigationService )
	{
		_userStore = userStore;
		_navigationService = addUserNavigationService;

		UserList = new ObservableCollection<User>( _userStore.Users );
		_filterText = string.Empty;

		AddUserCommand = new RelayCommand( _ => true, DoAddUser );
		EditUserCommand = new RelayCommand( CanEditUser, DoEditUser );
		_userStore.UserAdded += OnUserAdded;
	}

	private bool CanEditUser( object? _ ) => CurrentUser is not null;

	private void DoEditUser( object? _ ) => _navigationService.Navigate();

	private void DoAddUser( object? _ )
	{
		CurrentUser = new User();
		_navigationService.Navigate();
	}

	private void OnUserAdded( User user )
	{
		UserList.Add( user );
		CurrentUser = user;
	}

	#region List Filtering

	private string _filterText;

	public string FilterText
	{
		get => _filterText;
		set
		{
			if( value is not null )
			{
				_filterText = value;
				OnPropertyChanged();
			}
		}
	}

	public Func<object, bool> ListFilter
	{
		get
		{
			 const StringComparison cComp = StringComparison.OrdinalIgnoreCase;
			return ( item ) =>
			{
				if( FilterText.Length == 0 ) { return true; }

				if( item is User user )
				{
					string name = user.Name is not null ? user.Name : string.Empty;
					string email = user.Email is not null ? user.Email : string.Empty;
					string dob = user.BirthDate.HasValue ? user.BirthDate.Value.ToString() : string.Empty;
					string age = user.Age.HasValue ? user.Age.Value.ToString() : string.Empty;
					string test = user.Tester.ToString();

					if( name.Contains( FilterText, cComp ) ||
						( email.Length > 0 && email.Contains( FilterText, cComp ) ) ||
						( dob.Length > 0 && dob.Contains( FilterText ) ) ||
						( age.Length > 0 && age.Contains( FilterText ) ) ||
						( test.Length > 0 && test.Contains( FilterText, cComp ) ) )
						return true;
				}

				return false;
			};
		}
	}

	#endregion
}