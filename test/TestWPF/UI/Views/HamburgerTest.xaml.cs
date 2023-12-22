using System.Windows;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

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
		if( DataContext is SharedViewModel viewModel )
		{
			//cbToggleMenu.IsChecked = false;
			ContentMain.Content = new Introduction( viewModel );
		}
	}

	private void CommandTest_Click( object sender, RoutedEventArgs e )
	{
		if( DataContext is SharedViewModel viewModel )
		{
			cbToggleMenu.IsChecked = false;
			ContentMain.Content = new CommandTest( viewModel );
		}
	}

	private void ControlTest_Click( object sender, RoutedEventArgs e )
	{
		cbToggleMenu.IsChecked = false;
		ContentMain.Content = new ControlsTest();
	}

	private void CustomTest_Click( object sender, RoutedEventArgs e )
	{
		if( DataContext is SharedViewModel viewModel )
		{
			cbToggleMenu.IsChecked = false;
			ContentMain.Content = new CustomTest( viewModel );
		}
	}

	private void Validation_Click( object sender, RoutedEventArgs e )
	{
		cbToggleMenu.IsChecked = false;
		ContentMain.Content = new Validation( App.ValidationVM );
	}

	#endregion
}