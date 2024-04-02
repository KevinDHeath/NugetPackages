using System.Windows.Controls;

namespace Sample.Wpf.Views;

public partial class UnitTestView : UserControl
{
	public UnitTestView()
	{
		InitializeComponent();
	}

	// Fix bug with event being fired twice. Maybe due to using UpdateSourceTrigger=PropertyChanged
	private DateTime? _lastPickerDate;
	private void SelectedDateChanged( object sender, SelectionChangedEventArgs e )
	{
		if( e.OriginalSource is Common.Wpf.Controls.DatePicker dp )
		{
			DateTime? pickerDate = dp.SelectedDate is null ? DateTime.MaxValue : dp.SelectedDate;
			if( _lastPickerDate != pickerDate ) { _lastPickerDate = pickerDate; }
		}
	}

	private void FontSize_Changed( object sender, TextChangedEventArgs e )
	{
		if( e.OriginalSource is Common.Wpf.Controls.NumericSpinner ns )
		{
			if( double.TryParse( ns.Text, out double value ) )
			{
				App.ChangeFontSize( value );
			}
		}
	}

	#region Error handling for Validation Rules Tab

	private UnitTestViewModel? _vm;

	// This event occurs when a ValidationRule in a BindingGroup or a Binding fails.
	private void ItemError( object sender, ValidationErrorEventArgs e )
	{
		if( _vm is null )
		{
			if( e.OriginalSource is TextBox tb ) { _vm = tb.DataContext as UnitTestViewModel; }
			else if( e.OriginalSource is ComboBox cb ) { _vm = cb.DataContext as UnitTestViewModel; }
		}

		if( _vm is not null )
		{
			if( e.Action == ValidationErrorEventAction.Added ) _vm.UIErrors++;
			else if( e.Action == ValidationErrorEventAction.Removed ) _vm.UIErrors--;
		}
	}

	#endregion
}