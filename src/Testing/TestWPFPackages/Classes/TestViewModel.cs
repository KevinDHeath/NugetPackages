using Common.Core.Classes;
using TestWPFPackages.Core.Commands;
using TestWPFPackages.Core.Models;

namespace TestWPFPackages.Core.ViewModels;

public class TestViewModel : ModelBase
{
	#region Properties

	public string Name
	{
		get => ( _testItem.Name is not null ) ? _testItem.Name : string.Empty;
		set
		{
			if( value.Equals( _testItem.Name ) ) return;
			_testItem.Name = value;
		}
	}

	public TestItem? Item
	{
		get { return _item; }
		set
		{
			_item = value;
			OnPropertyChanged();
		}
	}

	public bool IsMenuOpen
	{
		get { return _isMenuOpen; }
		set
		{
			_isMenuOpen = value;
			OnPropertyChanged();
		}
	}

	public bool CanExecuteTest
	{
		get { return _isLoggedIn; }
		set
		{
			_isLoggedIn = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Commands

	public RelayCommand AddCommand { get; }

	public RelayCommand RemoveCommand { get; }

	public RelayCommand TestCommand { get; }

	#endregion

	#region Constructor and Variables

	private readonly TestItem _testItem;
	private TestItem? _item;
	private bool _isMenuOpen;
	private bool _isLoggedIn;

	public TestViewModel()
	{
		// For Custom1 Test
		_testItem = new TestItem { Name = "Hello world!" };

		// For CanExecute Test
		AddCommand = new RelayCommand( _ => Item = new TestItem() );
		RemoveCommand = new RelayCommand( _ => Item = null );
		TestCommand = new RelayCommand( delegate { } );
	}

	#endregion
}