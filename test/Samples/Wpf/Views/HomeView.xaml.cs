using System.Windows;
using System.Windows.Controls;

namespace Sample.Wpf.Views;

public partial class HomeView : UserControl
{
	public HomeView()
	{
		InitializeComponent();
	}

	private void Container_Loaded( object sender, RoutedEventArgs e )
	{
		ComponentList.Sort( ComponentList.DefaultColumn );
	}

	private void ComponentList_ColumnHeaderClicked( object sender, RoutedEventArgs e )
	{
		if( e.OriginalSource is not null &&
			e.OriginalSource is GridViewColumnHeader header &&
			header.Role != GridViewColumnHeaderRole.Padding )
		{
			ComponentList.Sort( header );
		}
	}
}