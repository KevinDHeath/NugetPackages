using System.Windows.Controls;

namespace Sample.Wpf.Views;

public partial class UserListView : UserControl
{
	public UserListView()
	{
		InitializeComponent();
	}

	private void MainGrid_Loaded( object sender, System.Windows.RoutedEventArgs e )
	{
		UserList.Sort( UserList.DefaultColumn );
	}

	private void UserList_ColumnHeaderClicked( object sender, System.Windows.RoutedEventArgs e )
	{
		if( e.OriginalSource is not null &&
			e.OriginalSource is GridViewColumnHeader header &&
			header.Role != GridViewColumnHeaderRole.Padding )
		{
			UserList.Sort( header );
		}
	}

	private void OnFilterChanged( object sender, TextChangedEventArgs e )
	{
		UserList.ApplyFilter();
	}
}