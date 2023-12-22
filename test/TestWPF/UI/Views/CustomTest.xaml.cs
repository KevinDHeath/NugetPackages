using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

public partial class CustomTest : UserControl
{
	#region Constructors

	public CustomTest()
	{
		InitializeComponent();
		DataContext = new SharedViewModel();
	}

	public CustomTest( SharedViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
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