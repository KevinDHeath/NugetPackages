using System.Windows;
using TestWPF.Core.ViewModels;

namespace TestWPF.Views;

public partial class HamburgerTest : Window
{
    public HamburgerTest()
    {
        InitializeComponent();
		Introduction_Click( this, new RoutedEventArgs() );
	}

	#region Event Handlers

	private void Introduction_Click( object sender, RoutedEventArgs e )
	{
		if( DataContext is TestViewModel viewModel )
		{
			cbToggleMenu.IsChecked = false;
			ContentMain.Content = new Introduction( viewModel );
		}
	}

	private void ShowCanExecute_Click( object sender, RoutedEventArgs e )
	{
		if( DataContext is TestViewModel viewModel )
		{
			cbToggleMenu.IsChecked = false;
			ContentMain.Content = new CanExecuteTest( viewModel );
		}
	}

	private void ShowCustomControl1_Click( object sender, RoutedEventArgs e )
	{
		if( DataContext is TestViewModel viewModel )
		{
			cbToggleMenu.IsChecked = false;
			ContentMain.Content = new Custom1Test( viewModel );
		}
	}

	private void ShowTestWpf_Click( object sender, RoutedEventArgs e )
	{
		cbToggleMenu.IsChecked = false;
		ContentMain.Content = new TestWpfView();
	}

	#endregion
}