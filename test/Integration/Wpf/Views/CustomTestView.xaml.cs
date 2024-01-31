using System.Windows.Controls;

namespace Sample.Wpf.Views;

public partial class CustomTestView : UserControl
{
	#region Constructors

	public CustomTestView()
	{
		InitializeComponent();
	}

	#endregion

	private void TextChanged( object sender, TextChangedEventArgs e )
	{
		if( sender is TextBox tb )
		{
			var test = tb.Text;
			if( test != tb.Text ) { tb.Text = test; }
		}
	}
}