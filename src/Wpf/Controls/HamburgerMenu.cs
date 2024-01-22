using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Common.Wpf.Controls;

/// <summary>Control to implement the Hamburger style menu (or Navigation Drawer) control.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control with menu items:<br/>
/// <code language="XAML">
/// &lt;cc:HamburgerMenu IsOpen="{Binding IsChecked, ElementName=cbToggleMenu}" ...&gt;
///   &lt;custom:HamburgerMenu.Content&gt;
///     &lt;StackPanel Margin="0 10"&gt;
///       &lt;custom:HamburgerMenuItem&gt;
///         &lt;TextBlock Text="Introduction"/&gt;
///       &lt;/custom:HamburgerMenuItem&gt;
///     &lt;/StackPanel&gt;
///   &lt;/custom:HamburgerMenu.Content&gt;
/// &lt;/cc:HamburgerMenu&gt;
/// </code>
/// 3. In the <b>ViewModel</b> create the properties for binding: 
/// <code>
/// public bool IsMenuOpen => _navigationStore.IsMenuOpen;</code>
/// </remarks>
public class HamburgerMenu : Control
{
	#region Properties

	/// <summary>Identifies the IsOpen dependency property.</summary>
	public static readonly DependencyProperty IsOpenProperty =
		DependencyProperty.Register( name: nameof( IsOpen ), propertyType: typeof( bool ),
			ownerType: typeof( HamburgerMenu ),
			typeMetadata: new PropertyMetadata( defaultValue: false,
				propertyChangedCallback: OnIsOpenPropertyChanged ) );

	/// <summary>Gets or sets the menu open indicator. The default is <see langword="false"/>.</summary>
	public bool IsOpen
	{
		get { return (bool)GetValue( IsOpenProperty ); }
		set { SetValue( IsOpenProperty, value ); }
	}

	/// <summary>Identifies the OpenCloseDuration dependency property.</summary>
	public static readonly DependencyProperty OpenCloseDurationProperty =
		DependencyProperty.Register( nameof( OpenCloseDuration ), typeof( Duration ),
			typeof( HamburgerMenu ), new PropertyMetadata( defaultValue: Duration.Automatic ) );

	/// <summary>Gets or sets the menu open duration. The default is <c>Automatic</c>.</summary>
	public Duration OpenCloseDuration
	{
		get { return (Duration)GetValue( OpenCloseDurationProperty ); }
		set { SetValue( OpenCloseDurationProperty, value ); }
	}

	/// <summary>Identifies the FallBackOpenWidth dependency property.</summary>
	public static readonly DependencyProperty FallBackOpenWidthProperty =
		DependencyProperty.Register( nameof( FallBackOpenWidth ), typeof( double ),
			typeof( HamburgerMenu ), new PropertyMetadata( 100.0 ) );

	/// <summary>Gets or sets the fall-back menu open width. The default is <c>100</c>.</summary>
	public double FallBackOpenWidth
	{
		get { return (double)GetValue( FallBackOpenWidthProperty ); }
		set { SetValue( FallBackOpenWidthProperty, value ); }
	}

	/// <summary>Identifies the Content dependency property.</summary>
	public static readonly DependencyProperty ContentProperty =
		DependencyProperty.Register( nameof( Content ), typeof( FrameworkElement ),
			typeof( HamburgerMenu ), new PropertyMetadata( null ) );

	/// <summary>Gets or sets the menu content.</summary>
	public FrameworkElement Content
	{
		get { return (FrameworkElement)GetValue( ContentProperty ); }
		set { SetValue( ContentProperty, value ); }
	}

	#endregion

	#region Constructors

	static HamburgerMenu()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( HamburgerMenu ), new FrameworkPropertyMetadata( typeof( HamburgerMenu ) ) );
	}

	/// <summary>Initializes a new instance of the HamburgerMenu class.</summary>
	public HamburgerMenu()
	{
		Width = 0;
	}

	#endregion

	#region Private Methods

	private static void OnIsOpenPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
	{
		if( d is HamburgerMenu hamburgerMenu )
		{
			hamburgerMenu.OnIsOpenPropertyChanged();
		}
	}

	private void OnIsOpenPropertyChanged()
	{
		if( IsOpen )
		{
			OpenMenuAnimated();
		}
		else
		{
			CloseMenuAnimated();
		}
	}

	private void OpenMenuAnimated()
	{
		double contentWidth = GetDesiredContentWidth();
		DoubleAnimation openingAnimation = new( contentWidth, OpenCloseDuration );
		BeginAnimation( WidthProperty, openingAnimation );
	}

	private double GetDesiredContentWidth()
	{
		if( Content == null )
		{
			return FallBackOpenWidth;
		}

		Content.Measure( new Size( MaxWidth, MaxHeight ) );

		return Content.DesiredSize.Width;
	}

	private void CloseMenuAnimated()
	{
		DoubleAnimation closingAnimation = new( 0, OpenCloseDuration );
		BeginAnimation( WidthProperty, closingAnimation );
	}

	#endregion
}