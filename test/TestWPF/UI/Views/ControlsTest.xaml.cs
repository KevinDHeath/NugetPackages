using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

public partial class ControlsTest : UserControl
{
	public ControlsTest()
	{
		InitializeComponent();
		DataContext = new ControlsViewModel( App.Settings );
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
}