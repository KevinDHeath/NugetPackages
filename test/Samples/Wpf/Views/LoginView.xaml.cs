using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sample.Wpf.Views;

public partial class LoginView : UserControl
{
	public LoginView()
	{
		InitializeComponent();
	}

	private void FirstFocus( object sender, RoutedEventArgs e )
	{
		if( sender is TextBox tb ) { _ = Keyboard.Focus( tb ); }
	}

	private void PasswordChanged( object sender, RoutedEventArgs e )
	{
		if( sender is PasswordBox pb && DataContext is LoginViewModel vm )
		{
			//vm.Password = pb.SecurePassword;
			vm.Password = new System.Net.NetworkCredential( string.Empty, pb.SecurePassword ).Password;
		}
	}
}