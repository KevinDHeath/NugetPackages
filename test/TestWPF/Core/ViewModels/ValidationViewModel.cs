using System.Collections.ObjectModel;
using System.Windows.Input;
using Common.Core.Classes;
using Common.Wpf.Commands;
using TestWPF.Core.Models;

namespace TestWPF.Core.ViewModels;

public sealed class ValidationViewModel : ModelBase
{
	#region Properties

	public User CurrentUser
	{
		get => _curUser;
		set
		{
			if( value is not null )
			{
				_orgUser = value;
				_curUser = (User)_orgUser.Clone();
				OnPropertyChanged();
			}
		}
	}

	public static IEnumerable<Common.Core.Models.Genders> GenderValues => _genders;

	public int ErrorCount
	{
		get => _errorCount;
		set
		{
			if( value != _errorCount )
			{
				_errorCount = value;
				OnPropertyChanged();
			}
		}
	}

	public ObservableCollection<User> UsersList
	{
		get => _usersList;
		set
		{
			if( value is not null )
			{
				_usersList = value;
				OnPropertyChanged();
			}
		}
	}

	#endregion

	#region Commands

	public ICommand AddUserCommand
	{
		get
		{
			_addUser ??= new RelayCommand( p => CanAddUser(), p => DoAddUser() );
			return _addUser;
		}
	}

	public ICommand CancelEditCommand
	{
		get
		{
			_cancelEdit ??= new RelayCommand( p => IsDirty(), p => DoCancelEdit() );
			return _cancelEdit;
		}
	}

	public ICommand CommitEditCommand
	{
		get
		{
			_commitEdit ??= new RelayCommand( p => CanCommitEdit(), p => DoCommitEdit() );
			return _commitEdit;
		}
	}

	#endregion

	#region Constructor and Variables

	private static readonly IList<User> _users = User.GetUsers();
	private static readonly IEnumerable<Common.Core.Models.Genders> _genders = User.GetGenders();
	private ObservableCollection<User> _usersList;
	private User _curUser;
	private User _orgUser;
	private int _errorCount;
	private ICommand? _addUser;
	private ICommand? _cancelEdit;
	private ICommand? _commitEdit;

	public ValidationViewModel()
	{
		_orgUser = _users[2];
		_curUser = (User)_orgUser.Clone();
		_usersList = new ObservableCollection<User>( _users );
		_filterText = string.Empty;
	}

	#endregion

	#region Private Methods

	private bool IsDirty() => _curUser.Name != _orgUser.Name || _curUser.Email != _orgUser.Email ||
		_curUser.Age != _orgUser.Age || _curUser.BirthDate != _orgUser.BirthDate ||
		_curUser.Gender != _orgUser.Gender;

	private bool CanAddUser() => ErrorCount == 0 && !IsDirty();

	private void DoAddUser()
	{
		_orgUser = new User();
		CurrentUser = new User();
	}

	private void DoCancelEdit() => CurrentUser.ApplyChanges( _orgUser );

	private bool CanCommitEdit() => ErrorCount == 0 & IsDirty();

	private void DoCommitEdit()
	{
		_orgUser.ApplyChanges( CurrentUser );
		if( !UsersList.Contains( _orgUser ) ) UsersList.Add( _orgUser );
	}

	#endregion

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
			return ( item ) =>
			{
				if( FilterText.Length == 0 ) { return true; }

				if( item is User user )
				{
					string age = user.Age.HasValue ? user.Age.Value.ToString() : string.Empty;
					string dob = user.BirthDate.HasValue ? user.BirthDate.Value.ToString() : string.Empty;
					string name = user.Name is not null ? user.Name : string.Empty;
					string email = user.Email is not null ? user.Email : string.Empty;

					if( name.Contains( FilterText, StringComparison.OrdinalIgnoreCase ) ||
						( age.Length > 0 && age.Contains( FilterText ) ) ||
						( dob.Length > 0 && dob.Contains( FilterText ) ) ||
						( email.Length > 0 && email.Contains( FilterText ) ) )
						return true;
				}

				return false;
			};
		}
	}

	#endregion
}