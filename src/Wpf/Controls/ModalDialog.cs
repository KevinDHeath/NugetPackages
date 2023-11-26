using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Wpf.Controls;

// https://github.com/SingletonSean/wpf-ui-workshops/tree/master/ModalControl

/// <summary>Control to implement a modal dialog for displaying User Controls.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control setting the ZIndex:
/// <code language="XAML">
/// &lt;cc:ModalDialog Panel.ZIndex="1" IsOpen="{Binding IsOpen}"&gt;
///   &lt;ContentControl Content="{Binding CurrentModalViewModel}"/&gt;
/// &lt;/cc:ModalDialog&gt;</code>
/// 3. In the <b>ViewModel</b> create the properties for binding: 
/// <code>
/// public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
/// public bool IsOpen => _modalNavigationStore.IsOpen;</code>
/// </remarks>
public class ModalDialog : ContentControl
{
	#region Properties

	/// <summary>Identifies the IsOpen dependency property.</summary>
	public static readonly DependencyProperty IsOpenProperty =
		DependencyProperty.Register( nameof( IsOpen ), typeof( bool ), typeof( ModalDialog ),
			new PropertyMetadata( false ) );

	/// <summary>Gets or sets the is open indicator.</summary>
	public bool IsOpen
	{
		get { return (bool)GetValue( IsOpenProperty ); }
		set { SetValue( IsOpenProperty, value ); }
	}

	#endregion

	#region Constructors

	static ModalDialog()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( ModalDialog ),
			new FrameworkPropertyMetadata( typeof( ModalDialog ) ) );

		BackgroundProperty.OverrideMetadata( typeof( ModalDialog ),
			new FrameworkPropertyMetadata( CreateDefaultBackground() ) );
	}

	#endregion

	#region Private Methods

	private static object CreateDefaultBackground()
	{
		return new SolidColorBrush( Colors.Black )
		{
			Opacity = 0.3
		};
	}

	#endregion
}