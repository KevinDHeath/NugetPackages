using Common.Core.Classes;
using Common.Wpf.Commands;
using TestWPF.Core.Models;

namespace TestWPF.Core.ViewModels;

public class SharedViewModel : ModelBase
{
	#region Used by views: CustomTest, HamburgerTest, and Introduction

	private bool _isMenuOpen;
	private bool _isCustomTest;

	public bool IsMenuOpen
	{
		get { return _isMenuOpen; }
		set
		{
			_isMenuOpen = value;
			OnPropertyChanged();
		}
	}

	public bool AllowCustomTest
	{
		get { return _isCustomTest; }
		set
		{
			_isCustomTest = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Used by view: CommandTest

	private readonly TestItem _testItem;
	private TestItem? _item;

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

	public DelegateCommand AddCommand { get; }

	public DelegateCommand RemoveCommand { get; }

	public DelegateCommand TestCommand { get; }

	#endregion

	public SharedViewModel()
	{
		_testItem = new TestItem { Name = "Hello world!" };
		AddCommand = new DelegateCommand( _ => Item = new TestItem() );
		RemoveCommand = new DelegateCommand( _ => Item = null );
		TestCommand = new DelegateCommand( delegate { } );
	}
}