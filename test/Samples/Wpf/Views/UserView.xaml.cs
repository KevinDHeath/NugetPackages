using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sample.Wpf.Views
{
	public partial class UserView : UserControl
	{
		public UserView()
		{
			InitializeComponent();
		}

		private void FirstFocus( object sender, RoutedEventArgs e )
		{
			if( sender is TextBox tb ) { _ = Keyboard.Focus( tb ); }
		}
	}
}