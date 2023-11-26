using System;
using System.Windows;
using System.Windows.Controls;

namespace Common.Wpf.Controls;

// https://www.codeproject.com/Articles/1268558/A-WPF-ListView-Custom-Control-with-Search-Filter-T

/// <summary>Control to implement the entry of search criteria in a TextBox.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control:<br/>
/// <code language="XAML">&lt;cc:SearchTextBox... /&gt;</code>
/// </remarks>
public class SearchTextBox : TextBox
{
	#region Constructor

	static SearchTextBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( SearchTextBox ),
			new FrameworkPropertyMetadata( typeof( SearchTextBox ) ) );
	}

	#endregion

	#region Properties and Fields

	/// <summary>
	/// Dependency property for IsEmpty.
	/// </summary>
	public static readonly DependencyProperty IsEmptyProperty =
		DependencyProperty.Register( nameof( IsEmpty ), typeof( bool ), typeof( SearchTextBox ),
			new PropertyMetadata( false ) );

	/// <summary>Gets or sets the is empty indicator.</summary>
	public bool IsEmpty
	{
		get { return (bool)GetValue( IsEmptyProperty ); }
		private set { SetValue( IsEmptyProperty, value ); }
	}

	#endregion

	#region Overridden Methods

	/// <summary>
	/// Raises the Initialized event. This method is invoked whenever IsInitialized is set
	/// to true internally.
	/// </summary>
	/// <param name="e">The RoutedEventArgs that contains the event data.</param>
	protected override void OnInitialized( EventArgs e )
	{
		UpdateIsEmpty();
		base.OnInitialized( e );
	}

	/// <summary>
	/// Is called when content in this editing control changes.
	/// </summary>
	/// <param name="e">The arguments that are associated with the TextChanged event.</param>
	protected override void OnTextChanged( TextChangedEventArgs e )
	{
		UpdateIsEmpty();
		base.OnTextChanged( e );
	}

	#endregion

	#region Private Methods

	private void UpdateIsEmpty()
	{
		IsEmpty = string.IsNullOrEmpty( Text );
	}

	#endregion
}
