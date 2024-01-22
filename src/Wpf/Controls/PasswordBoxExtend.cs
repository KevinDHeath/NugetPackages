using System.Windows.Controls;
using System.Windows;

// WPF PasswordBox and Data binding
// http://blog.functionalfun.net/2008/06/wpf-passwordbox-and-data-binding.html

namespace Common.Wpf.Controls;

/// <summary>Extends PasswordBox with 2 attached properties that allows binding of the password.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the PasswordBox control specifying to bind the password:<br/>
/// <code language="XAML">
/// &lt;PasswordBox cc:PasswordBoxExtend.BindPassword="True"... &gt;
///   &lt;cc:PasswordBoxExtend.BoundPassword&gt;
///     &lt;Binding Path="Password" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged"/&gt; 
///   &lt;/cc:PasswordBoxExtend.BoundPassword&gt;
/// &lt;/PasswordBox&gt;
/// </code>
/// </remarks>
public static class PasswordBoxExtend
{
	#region Properties

	/// <summary>Identifies the BindPassword dependency property.</summary>
	public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
		"BindPassword", typeof( bool ), typeof( PasswordBoxExtend ),
		new PropertyMetadata( false, OnBindPasswordChanged ) );

	/// <summary>Identifies the BoundPassword dependency property.</summary>
	public static readonly DependencyProperty BoundPassword = DependencyProperty.RegisterAttached(
		"BoundPassword", typeof( string ), typeof( PasswordBoxExtend ),
		new PropertyMetadata( string.Empty, OnBoundPasswordChanged ) );

	private static readonly DependencyProperty UpdatingPassword = DependencyProperty.RegisterAttached(
		"UpdatingPassword", typeof( bool ), typeof( PasswordBoxExtend ),
		new PropertyMetadata( false ) );

	#endregion

	#region Private Methods

	private static void OnBindPasswordChanged( DependencyObject dp, DependencyPropertyChangedEventArgs e )
	{
		// When the BindPassword attached property is set on a PasswordBox,
		// start listening to its PasswordChanged event
		if( dp is not PasswordBox box )
		{
			return;
		}

		bool wasBound = (bool)e.OldValue;
		bool needToBind = (bool)e.NewValue;

		if( wasBound )
		{
			box.PasswordChanged -= HandlePasswordChanged;
		}

		if( needToBind )
		{
			box.PasswordChanged += HandlePasswordChanged;
		}
	}

	private static bool GetUpdatingPassword( DependencyObject dp ) => (bool)dp.GetValue( UpdatingPassword );

	private static void SetUpdatingPassword( DependencyObject dp, bool value ) => dp.SetValue( UpdatingPassword, value );

	private static void OnBoundPasswordChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
	{
		// Only handle this event when the property is attached to a PasswordBox
		// and when the BindPassword attached property has been set to true
		if( d == null || d is not PasswordBox box || !GetBindPassword( d ) )
		{
			return;
		}

		// Avoid recursive updating by ignoring the box's changed event
		box.PasswordChanged -= HandlePasswordChanged;

		string newPassword = (string)e.NewValue;
		if( !GetUpdatingPassword( box ) )
		{
			box.Password = newPassword;
		}

		box.PasswordChanged += HandlePasswordChanged;
	}

	private static void HandlePasswordChanged( object sender, RoutedEventArgs e )
	{
		if( sender is not PasswordBox box ) { return; }

		// Set a flag to indicate that we're updating the password
		SetUpdatingPassword( box, true );

		// Push the new password into the BoundPassword property
		SetBoundPassword( box, box.Password );
		SetUpdatingPassword( box, false );
	}

	#endregion

	#region Public Methods

	/// <summary>Sets the Bind Password property.</summary>
	/// <param name="dp">Dependency object.</param>
	/// <param name="value">Value to set.</param>
	public static void SetBindPassword( DependencyObject dp, bool value ) => dp.SetValue( BindPassword, value );

	/// <summary>Gets the Bind Password property.</summary>
	/// <param name="dp">Dependency object.</param>
	/// <returns><see langword="true"/> if the password is bound, otherwise <see langword="false"/> is returned.</returns>
	public static bool GetBindPassword( DependencyObject dp ) => (bool)dp.GetValue( BindPassword );

	/// <summary>Sets the Bound Password property.</summary>
	/// <param name="dp">Dependency object.</param>
	/// <param name="value">Value to set.</param>
	public static void SetBoundPassword( DependencyObject dp, string value ) => dp.SetValue( BoundPassword, value );

	/// <summary>Gets the Bound Password property.</summary>
	/// <param name="dp">Dependency object.</param>
	/// <returns>The bound password property value.</returns>
	public static string GetBoundPassword( DependencyObject dp ) => (string)dp.GetValue( BoundPassword );

	#endregion
}