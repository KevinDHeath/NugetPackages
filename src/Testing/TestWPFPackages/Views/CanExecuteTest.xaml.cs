using System.Windows.Controls;
using TestWPFPackages.Core.ViewModels;

namespace TestWPFPackages.Views;

/// <summary>Interaction logic for CanExecuteTest.xaml</summary>
public partial class CanExecuteTest : UserControl
{
	public CanExecuteTest()
	{
		InitializeComponent();
		DataContext = new TestViewModel();
	}

	public CanExecuteTest( TestViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
	}
}