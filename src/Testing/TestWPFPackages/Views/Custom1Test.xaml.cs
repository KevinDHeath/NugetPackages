using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.Views;

/// <summary>Interaction logic for Custom1Test.xaml</summary>
public partial class Custom1Test : UserControl
{
	#region Constructors

	public Custom1Test()
	{
		InitializeComponent();
		DataContext = new TestViewModel();
	}

	public Custom1Test( TestViewModel vm )
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