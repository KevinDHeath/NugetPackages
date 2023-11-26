using System.Windows;
using System.Windows.Controls;

namespace Common.Wpf.Controls;

/// <summary>Control to implement the Hamburger menu (or Navigation Drawer) style item class.</summary>
public class HamburgerMenuItem : RadioButton
{
	static HamburgerMenuItem()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( HamburgerMenuItem ),
			new FrameworkPropertyMetadata( typeof( HamburgerMenuItem ) ) );
	}
}