using Common.Wpf.Commands;
using Sample.Mvvm.Models;

namespace Sample.Mvvm.ViewModels;

public class CommandTestViewModel : ViewModelBase
{
	#region Properties

	public string Name
	{
		get => ( _testItem.User is not null ) ? _testItem.User : string.Empty;
		set
		{
			if( value.Equals( _testItem.User ) ) return;
			_testItem.User = value;
		}
	}

	public Account? Item
	{
		get { return _item; }
		set
		{
			_item = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Delegate Commands

	public DelegateCommand AddCommand { get; }

	public DelegateCommand RemoveCommand { get; }

	public DelegateCommand TestCommand { get; }

	#endregion

	public CommandTestViewModel( AccountStore accountStore )
	{
		_testItem = accountStore.CurrentAccount is not null ?
			accountStore.CurrentAccount : new Account { User = "Not logged in!" };

		AddCommand = new DelegateCommand( _ => Item = new Account() );
		RemoveCommand = new DelegateCommand( _ => Item = null );
		//TestCommand = new DelegateCommand( delegate { } );
		TestCommand = new DelegateCommand( _ => Item = _testItem );
	}

	#region Constructor and Variables

	private readonly Account _testItem;
	private Account? _item;


	#endregion
}