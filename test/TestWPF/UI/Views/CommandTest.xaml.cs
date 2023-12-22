using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

public partial class CommandTest : UserControl
{
	public CommandTest()
	{
		InitializeComponent();
		DataContext = new SharedViewModel();
	}

	public CommandTest( SharedViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
	}
}