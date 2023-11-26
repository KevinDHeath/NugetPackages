using System.Windows;

namespace Common.Wpf.Controls;

/// <summary>Extension to the System.Windows.Controls.ComboBox class.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control specifying whether error messages should be shown below the control:<br/>
/// <code language="XAML">&lt;cc:ComboBox IsErrorShown="True"... /&gt;</code>
/// </remarks>
public class ComboBox : System.Windows.Controls.ComboBox
{
	#region Properties

	/// <summary>Identifies the IsErrorShown dependency property.</summary>
	public readonly static DependencyProperty IsErrorShownProperty = DependencyProperty.Register(
		name: nameof( IsErrorShown ), propertyType: typeof( bool ), ownerType: typeof( ComboBox ),
		typeMetadata: new PropertyMetadata( defaultValue: false ) );

	/// <summary>Gets or sets whether error messages are shown to the user below the control.
	/// The default is false.</summary>
	public bool IsErrorShown
	{
		get { return (bool)GetValue( IsErrorShownProperty ); }
		set
		{
			SetValue( IsErrorShownProperty, value );
		}
	}

	#endregion

	#region Constructors

	static ComboBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( ComboBox ),
			new FrameworkPropertyMetadata( typeof( ComboBox ) ) );
	}

	/// <summary>Initializes a new instance of the ComboBox class.</summary>
	public ComboBox()
	{ }

	#endregion
}