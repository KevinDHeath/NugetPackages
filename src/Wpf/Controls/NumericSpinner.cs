using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Common.Wpf.Controls;

// https://stopbyte.com/t/free-wpf-numeric-spinner-numericupdown/499
// https://github.com/Stopbyte/WPF-Numeric-Spinner-NumericUpDown

/// <summary>Control to implement the ability to increase and decrease numeric values using
/// a 'NumericUpDown' style control.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control specifying the minimum and maximum values:<br/>
/// <code language="XAML">&lt;cc:NumericSpinner MinValue="0" MaxValue="140"... /&gt;</code>
/// </remarks>
public class NumericSpinner : TextBox
{
	#region Public Event

	/// <summary>Occurs when a property has changed.</summary>
	public event EventHandler PropertyChanged;

	#endregion

	#region Properties

	/// <summary>Identifies the Step dependency property.</summary>
	public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
		name: nameof( Step ), propertyType: typeof( decimal ), ownerType: typeof( NumericSpinner ),
		typeMetadata: new PropertyMetadata( defaultValue: new decimal( 1 ) ) );

	/// <summary>Gets or sets the increase/decrease value whenever a button is used.
	/// The default is one.</summary>
	public decimal Step
	{
		get { return (decimal)GetValue( StepProperty ); }
		set
		{
			SetValue( StepProperty, value );
		}
	}

	/// <summary>Identifies the Decimals dependency property.</summary>
	public static readonly DependencyProperty DecimalsProperty = DependencyProperty.Register(
		name: nameof( Decimals ), propertyType: typeof( int ), ownerType: typeof( NumericSpinner ),
		typeMetadata: new PropertyMetadata( defaultValue: 0 ) );

	/// <summary>Gets or sets the number of decimals to display. The default is zero.</summary>
	public int Decimals
	{
		get { return (int)GetValue( DecimalsProperty ); }
		set
		{
			SetValue( DecimalsProperty, value );
		}
	}

	/// <summary>Identifies the MinValue dependency property.</summary>
	public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
		name: nameof( MinValue ), propertyType: typeof( decimal ), ownerType: typeof( NumericSpinner ),
		typeMetadata: new PropertyMetadata( defaultValue: decimal.MinValue ) );

	/// <summary>Gets or sets the minimum value allowed. The default is decimal minimum.</summary>
	public decimal MinValue
	{
		get { return (decimal)GetValue( MinValueProperty ); }
		set
		{
			if( value > MaxValue ) MaxValue = value;
			SetValue( MinValueProperty, value );
		}
	}

	/// <summary>Identifies the MaxValue dependency property.</summary>
	public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
		name: nameof( MaxValue ), propertyType: typeof( decimal ), ownerType: typeof( NumericSpinner ),
		typeMetadata: new PropertyMetadata( defaultValue: decimal.MaxValue ) );

	/// <summary>Gets or sets the maximum value allowed. The default is decimal maximum.</summary>
	public decimal MaxValue
	{
		get { return (decimal)GetValue( MaxValueProperty ); }
		set
		{
			if( value < MinValue ) value = MinValue;
			SetValue( MaxValueProperty, value );
		}
	}

	/// <summary>Identifies the IsErrorShown dependency property.</summary>
	public static readonly DependencyProperty IsErrorShownProperty = DependencyProperty.Register(
		name: nameof( IsErrorShown ), propertyType: typeof( bool ), ownerType: typeof( NumericSpinner ),
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

	private decimal? Value
	{
		get { return _value; }
		set
		{
			if( value < MinValue ) value = MinValue;
			if( value > MaxValue ) value = MaxValue;
			_value = value;
		}
	}

	#endregion

	#region Constructors and Variables

	private decimal? _value;
	private Button? _btnUp = null;
	private Button? _btnDown = null;

	static NumericSpinner()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( NumericSpinner ),
			new FrameworkPropertyMetadata( typeof( NumericSpinner ) ) );
	}

	/// <summary>Initializes a new instance of the NumericSpinner class.</summary>
	public NumericSpinner()
	{
		SetBinding( TextProperty, new Binding( nameof( Value ) )
		{
			ElementName = "PART_ContentHost",
			Mode = BindingMode.TwoWay,
			UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
		} );

		DependencyPropertyDescriptor.FromProperty( DecimalsProperty, typeof( NumericSpinner ) )
			.AddValueChanged( this, PropertyChanged );
		DependencyPropertyDescriptor.FromProperty( MinValueProperty, typeof( NumericSpinner ) )
			.AddValueChanged( this, PropertyChanged );
		DependencyPropertyDescriptor.FromProperty( MaxValueProperty, typeof( NumericSpinner ) )
			.AddValueChanged( this, PropertyChanged );
		DependencyPropertyDescriptor.FromProperty( IsErrorShownProperty, typeof( NumericSpinner ) )
			.AddValueChanged( this, PropertyChanged );

		PropertyChanged += ( x, y ) => Validate();
	}

	#endregion

	#region Overridden Methods

	/// <summary>Is called when content in this editing control changes.</summary>
	/// <param name="e">The arguments that are associated with the TextChanged event.</param>
	protected override void OnTextChanged( TextChangedEventArgs e )
	{
		if( !string.IsNullOrWhiteSpace( Text ) && decimal.TryParse( Text,
			NumberStyles.Number | NumberStyles.AllowLeadingSign, CultureInfo.CurrentUICulture,
			out decimal value ) )
		{
			Value = value;
			var txtVal = Value.ToString();
			if( !Text.Equals( txtVal ) ) Text = txtVal;
		}
		else
		{
			// Text could be empty - this should be validated
			_value = null;
		}

		base.OnTextChanged( e );
	}

	/// <summary>When overridden in a derived class, this is invoked whenever application code
	/// or internal processes call ApplyTemplate().</summary>
	public override void OnApplyTemplate()
	{
		_btnUp = GetTemplateChild( "btnUp" ) as Button;
		if( _btnUp is not null ) { _btnUp.Click += Increase; }

		_btnDown = GetTemplateChild( "btnDown" ) as Button;
		if( _btnDown is not null ) { _btnDown.Click += Decrease; }

		base.OnApplyTemplate();
	}

	#endregion

	#region Private Methods

	/// <summary>Re-validate the object, whenever a value is changed.</summary>
	/// <remarks>Logically, This is not needed at all, it's handled within other properties.</remarks>
	private void Validate()
	{
		if( MinValue > MaxValue ) MinValue = MaxValue;
		if( MaxValue < MinValue ) MaxValue = MinValue;
		if( Value < MinValue ) Value = MinValue;
		if( Value > MaxValue ) Value = MaxValue;

        if( Value is not null )
        {
			Value = decimal.Round( Value.Value, Decimals );
		}
	}

	private void Increase( object sender, RoutedEventArgs e )
	{
		Value ??= 0;
		Value += Step;
		Text = Value.ToString();
	}

	private void Decrease( object sender, RoutedEventArgs e )
	{
		Value ??= 0;
		Value -= Step;
		Text = Value.ToString();
	}

	#endregion
}