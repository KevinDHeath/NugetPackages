using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Common.Wpf.Commands;
using Sample.Mvvm.Models;

namespace Sample.Mvvm.ViewModels;

public class UnitTestViewModel : ViewModelBase
{
	#region Properties

	public List<TestTypes> TestTypes { get; } = Enumerations.GetTestTypes().ToList();

	[Required( ErrorMessage = "{0} is required." )]
	[Range( 10, 18, ErrorMessage = "{0} must be between {1} and {2}." )]
	public double? FontSize
	{
		get => _settings.FontSize;
		set
		{
			if( value is not null && value.Equals( _settings.FontSize ) ) { return; }
			ValidateProperty( value );
			_settings.FontSize = value;
			OnPropertyChanged();
		}
	}

	public TestTypes ComboBoxVal
	{
		get => _settings.ComboBoxVal;
		set
		{
			if( value.Equals( _settings.ComboBoxVal ) ) { return; }
			_settings.ComboBoxVal = value;
			OnPropertyChanged();
		}
	}

	public TestTypes ComboEditRule
	{
		get => _settings.ComboEditRule;
		set
		{
			if( value.Equals( _settings.ComboEditRule ) ) { return; }
			_settings.ComboEditRule = value;
			OnPropertyChanged();
		}
	}

	public string? DataFile
	{
		get => _settings.DataFile;
		set
		{
			if( value is not null && value.Equals( _settings.DataFile ) ) { return; }
			_settings.DataFile = value;
			OnPropertyChanged();
		}
	}

	[Required( ErrorMessage = "Location is required." )]
	public string? DataFolder
	{
		get => _settings.DataFolder;
		set
		{
			if( value is not null && value.Equals( _settings.DataFolder ) ) { return; }
			ValidateProperty( value );
			_settings.DataFolder = value;
			OnPropertyChanged();
		}
	}

	public DateOnly? DateOnlyVal
	{
		get => _settings.DateOnlyVal;
		set
		{
			if( value is not null && value.Equals( _settings.DateOnlyVal ) ) { return; }
			_settings.DateOnlyVal = value;
			OnPropertyChanged();
		}
	}

	public string? DateTimeVal
	{
		get => _settings.DateTimeVal;
		set
		{
			if( value is not null && value.Equals( _settings.DateTimeVal ) ) { return; }
			_settings.DateTimeVal = value;
			OnPropertyChanged();
		}
	}

	public decimal? DecimalVal
	{
		get => _settings.DecimalVal;
		set
		{
			if( value is not null && value.Equals( _settings.DecimalVal ) ) { return; }
			_settings.DecimalVal = value;
			OnPropertyChanged();
		}
	}

	public double? DoubleVal
	{
		get => _settings.DoubleVal;
		set
		{
			if( value is not null && value.Equals( _settings.DoubleVal ) ) { return; }
			ValidateProperty( value );
			_settings.DoubleVal = value;
			OnPropertyChanged();
		}
	}

	public int? IntegerRule
	{
		get => _settings.IntegerRule;
		set
		{
			if( value is not null && value.Equals( _settings.IntegerRule ) ) { return; }
			_settings.IntegerRule = value;
			OnPropertyChanged();
		}
	}

	public int? IntegerVal
	{
		get => _settings.IntegerVal;
		set
		{
			if( value is not null && value.Equals( _settings.IntegerVal ) ) { return; }
			_settings.IntegerVal = value;
			OnPropertyChanged();
		}
	}

	public string? StringRule
	{
		get => _settings.StringRule;
		set
		{
			if( value is not null && value.Equals( _settings.StringRule ) ) { return; }
			_settings.StringRule = value;
			OnPropertyChanged();
		}
	}

	public string? StringVal
	{
		get => _settings.StringVal;
		set
		{
			if( value is not null && value.Equals( _settings.StringVal ) ) { return; }
			_settings.StringVal = value;
			OnPropertyChanged();
		}
	}

	#endregion

	private Settings _orgValue;
	private readonly Settings _settings;

	public UnitTestViewModel( SettingsStore settingsStore )
	{
		_settings = settingsStore.Settings;
		_orgValue = (Settings)_settings.Clone();
	}

	#region Commands

	ICommand? _cancelEdit;
	ICommand? _commitEdit;

	public ICommand CancelEditCommand
	{
		get
		{
			_cancelEdit ??= new RelayCommand( p => IsDirty(), p => DoCancelEdit() );
			return _cancelEdit;
		}
	}

	public ICommand CommitEditCommand
	{
		get
		{
			_commitEdit ??= new RelayCommand( p => CanCommitEdit(), p => DoCommitEdit() );
			return _commitEdit;
		}
	}

	private bool IsDirty() => _settings.HasChanges( _orgValue );

	public void DoCancelEdit()
	{
		FontSize = _orgValue.FontSize;
		ComboBoxVal = _orgValue.ComboBoxVal;
		ComboEditRule = _orgValue.ComboEditRule;
		DataFile = _orgValue.DataFile;
		DataFolder = _orgValue.DataFolder;
		DateOnlyVal = _orgValue.DateOnlyVal;
		DateTimeVal = _orgValue.DateTimeVal;
		DecimalVal = _orgValue.DecimalVal;
		DoubleVal = _orgValue.DoubleVal;
		IntegerRule = _orgValue.IntegerRule;
		IntegerVal = _orgValue.IntegerVal;
		StringRule = _orgValue.StringRule;
		StringVal = _orgValue.StringVal;
	}

	private bool CanCommitEdit() => !HasErrors && IsDirty();

	private void DoCommitEdit()
	{
		if( SettingsStore.Save( _settings ) )
		{
			_orgValue = (Settings)_settings.Clone();
		}
	}

	#endregion
}