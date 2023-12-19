using System.Windows.Controls;
using TestWPFPackages.Core.ViewModels;

namespace TestWPFPackages.Views;

/// <summary>Interaction logic for Introduction.xaml</summary>
public partial class Introduction : UserControl
{
	public Introduction()
	{
		InitializeComponent();
		DataContext = new TestViewModel();
	}

	public Introduction( TestViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
	}
}