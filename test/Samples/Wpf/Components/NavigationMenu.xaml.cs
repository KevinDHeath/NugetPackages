using System.Windows;
using System.Windows.Controls;

namespace Sample.Wpf.Components;

public partial class NavigationMenu : UserControl
{
	public NavigationMenu()
	{
		InitializeComponent();
	}

	/// <summary>Identifies the ContentView dependency property.</summary>
	public readonly static DependencyProperty ContentViewProperty = DependencyProperty.Register(
		nameof( ContentView ), typeof( ViewModelBase ), typeof( NavigationMenu ),
		new PropertyMetadata( defaultValue: null, propertyChangedCallback: ViewChangedCallback ) );

	/// <summary>Gets or sets the navigation bar view model.</summary>
	public ViewModelBase? ContentView
	{
		get => (ViewModelBase)GetValue( ContentViewProperty );
		set => SetValue( ContentViewProperty, value );
	}

	private static void ViewChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
	{
		var input = (NavigationMenu)d;
		input.ContentView = e.NewValue as ViewModelBase;
	}
}