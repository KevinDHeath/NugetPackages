using System.Windows;
using System.Windows.Controls;
using TestWPF.Core.ViewModels;

namespace TestWPF.UI.Views;

/// <summary>Interaction logic for TestWpfView.xaml</summary>
public partial class Validation : UserControl
{
	public Validation()
	{
		InitializeComponent();
		DataContext = new ValidationViewModel();
	}

	public Validation( ValidationViewModel vm )
	{
		InitializeComponent();
		DataContext = vm;
	}

	#region Data Entry Event Handlers

	private int _errorCount = 0;
	private void CheckForErrors()
	{
		tbError.Text = !MainGrid.BindingGroup.ValidateWithoutUpdate() ?
			MainGrid.BindingGroup.ValidationErrors[0].ErrorContent.ToString() : string.Empty;
	}

	// This event occurs when a ValidationRule in a BindingGroup or a Binding fails.
	private void ItemError( object sender, ValidationErrorEventArgs e )
	{
		if( e.Action == ValidationErrorEventAction.Added ) _errorCount++;
		else if( e.Action == ValidationErrorEventAction.Removed ) _errorCount--;
		tbErrorCount.Text = _errorCount.ToString();
	}

	private void MainGrid_Loaded( object sender, RoutedEventArgs e ) => CheckForErrors();

	private void DataChanged( object sender, TextChangedEventArgs e ) => CheckForErrors();

	// Fix bug with event being fired twice
	// Maybe due to using UpdateSourceTrigger=PropertyChanged
	private DateTime? _lastPickerDate;

	private void SelectedDateChanged( object sender, SelectionChangedEventArgs e )
	{
		if( e.OriginalSource is DatePicker dp )
		{
			var pickerDate = dp.SelectedDate is null ? DateTime.MaxValue : dp.SelectedDate;
			if( _lastPickerDate != pickerDate )
			{
				_lastPickerDate = pickerDate;
				CheckForErrors();
			}
		}
	}

	private void SelectedGenderChanged( object sender, SelectionChangedEventArgs e ) => CheckForErrors();

	#endregion

	#region List View Event Handlers

	private bool _listViewInit;

	private void UsersList_GotFocus( object sender, RoutedEventArgs e )
	{
		// Only do this once
		if( !_listViewInit )
		{
			_listViewInit = true;
			UsersList.Sort( UsersList.DefaultColumn );
		}
	}

	private void UsersList_ColumnHeaderClicked( object sender, RoutedEventArgs e )
	{
		if( e.OriginalSource is not null &&
			e.OriginalSource is GridViewColumnHeader header &&
			header.Role != GridViewColumnHeaderRole.Padding )
		{
			UsersList.Sort( header );
		}
	}

	private void OnFilterChanged( object sender, TextChangedEventArgs e )
	{
		UsersList.ApplyFilter();
	}

	#endregion
}