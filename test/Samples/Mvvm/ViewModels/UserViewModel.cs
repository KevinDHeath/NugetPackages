using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Common.Wpf.Commands;
using Sample.Mvvm.Models;
using Sample.Mvvm.Validations;

namespace Sample.Mvvm.ViewModels;

public class UserViewModel : ViewModelBase
{
	#region Properties

	[Required( ErrorMessage = "{0} cannot be empty." )]
	[StringLength( 50, ErrorMessage = "{0} cannot be longer than {1}." )]
	[UserRule()]
	public string Name
	{
		get => _mod.Name;
		set
		{
			ValidateProperty( value );
			_mod.Name = value;
			OnPropertyChanged();
		}
	}

	[Required( ErrorMessage = "{0} cannot be empty." )]
	[RegularExpression( cEmailRegex, ErrorMessage = "Format not valid." )]
	[StringLength( 50, ErrorMessage = "{0} cannot be longer than {1}." )]
	[UserEmail]
	public string Email
	{
		get => _mod.Email;
		set
		{
			value = value.Trim(); // Whitespace not allowed
			ValidateProperty( value );
			_mod.Email = value;
			OnPropertyChanged();
		}
	}

	[Required( ErrorMessage = "{0} cannot be empty." )]
	public DateOnly? BirthDate
	{
		get => _mod.BirthDate;
		set
		{
			ValidateProperty( value );
			_mod.BirthDate = value;
			OnPropertyChanged();

			// Force UserRule validation
			OnPropertyChanged( nameof( Age ) );
			Name = _mod.Name;
		}
	}
	public int? Age => CalculateAge( BirthDate );

	public TestTypes Tester
	{
		get { return _mod.Tester; }
		set
		{
			_mod.Tester = value;
			OnPropertyChanged();
		}
	}

	public static IEnumerable<TestTypes> TesterTypes => _testTypes;

	#endregion

	#region Commands

	public ICommand CommitCommand { get; }

	public ICommand RollbackCommand { get; }

	public ICommand CancelCommand { get; }

	private void DoCancel( object? _ ) => _closeNavigationService.Navigate();

	private bool IsDirty() => _org.HasChanges( _mod );

	private bool CanRollback( object? _ ) => IsDirty();

	private void DoRollback( object? _ )
	{
		Name = _org.Name;
		Email = _org.Email;
		BirthDate = _org.BirthDate;
		Tester = _org.Tester;
	}

	private bool CanCommit( object? _ ) => !HasErrors & IsDirty();

	private void DoCommit( object? _ )
	{
		if( _isNew ) { _userStore.AddUser( _mod ); }
		else { _org.ApplyChanges( _mod ); }
		_closeNavigationService.Navigate();
	}

	#endregion

	private static IEnumerable<TestTypes> _testTypes = Enumerations.GetTestTypes();
	internal readonly UserStore _userStore;
	private readonly INavigationService _closeNavigationService;
	private readonly User _org;
	private readonly User _mod;
	internal readonly bool _isNew;

	public UserViewModel( UserStore userStore, INavigationService closeNavigationService )
	{
		_userStore = userStore;
		_closeNavigationService = closeNavigationService;
		_org = _userStore.CurrentUser is not null ? _userStore.CurrentUser : new User();
		_mod = (User)_org.Clone();
		_isNew = string.IsNullOrEmpty( _mod.Name );
		_testTypes = Enumerations.GetTestTypes();

		CommitCommand = new RelayCommand( CanCommit, DoCommit );
		RollbackCommand = new RelayCommand( CanRollback, DoRollback );
		CancelCommand = new RelayCommand( _ => true, DoCancel );

		ValidateAllProperties();
	}
}