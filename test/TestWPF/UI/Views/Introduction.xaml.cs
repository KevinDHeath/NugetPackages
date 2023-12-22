using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

/// <summary>Interaction logic for Introduction.xaml</summary>
public partial class Introduction : UserControl
{
	public Introduction()
	{
		InitializeComponent();
		DataContext = new SharedViewModel();
	}

	public Introduction( SharedViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
	}
}