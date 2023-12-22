using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Common.Core.Classes;
using Common.Wpf.Commands;
using TestWPF.Core.Models;

namespace TestWPF.Core.ViewModels;

public class ControlsViewModel : ModelDataError
{
	#region Properties

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

	public DateOnly? DateOnlyVal
	{
		get { return _settings.DateOnlyVal; }
		set { _settings.DateOnlyVal = value; OnPropertyChanged(); }
	}

	public string? DateTimeVal
	{
		get { return _settings.DateTimeVal; }
		set { _settings.DateTimeVal = value; OnPropertyChanged(); }
	}

	public decimal? DecimalVal
	{
		get { return _settings.DecimalVal; }
		set
		{
			if( value is not null && value.Equals( _settings.DecimalVal ) ) { return; }
			_settings.DecimalVal = value;
			OnPropertyChanged();
		}
	}

	public int? IntegerRule
	{
		get { return _settings.IntegerRule; }
		set
		{
			if( value is not null && value.Equals( _settings.IntegerRule ) ) { return; }
			_settings.IntegerRule = value;
			OnPropertyChanged();
		}
	}

	public int? IntegerVal
	{
		get { return _settings.IntegerVal; }
		set
		{
			if( value is not null && value.Equals( _settings.IntegerVal ) ) { return; }
			_settings.IntegerVal = value;
			OnPropertyChanged();
		}
	}

	public string? StringVal
	{
		get { return _settings.StringVal; }
		set { _settings.StringVal = value; OnPropertyChanged(); }
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

	public TestTypes TestType
	{
		get { return _settings.TestType; }
		set
		{
			if( value.Equals( _settings.TestType ) ) { return; }
			_settings.TestType = value;
			OnPropertyChanged();
		}
	}

	public List<TestTypes> TestTypes { get; } = Enumerations.GetTestTypes().ToList();

	#endregion

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

	private void DoCancelEdit()
	{
		FontSize = _orgValue.FontSize;
		DataFile = _orgValue.DataFile;
		DataFolder = _orgValue.DataFolder;
		DateOnlyVal = _orgValue.DateOnlyVal;
		DateTimeVal = _orgValue.DateTimeVal;
		DecimalVal = _orgValue.DecimalVal;
		IntegerRule = _orgValue.IntegerRule;
		IntegerVal = _orgValue.IntegerVal;
		StringVal = _orgValue.StringVal;
		TestType = _orgValue.TestType;
	}

	private bool CanCommitEdit() => !HasErrors && IsDirty();

	private void DoCommitEdit()
	{
		if( Settings.Save( _settings ) )
		{
			_orgValue = (Settings)_settings.Clone();
		}
	}

	#endregion

	private Settings _orgValue;
	private readonly Settings _settings;

	public ControlsViewModel( Settings? settings )
	{
		_settings = settings is not null ? settings : new Settings(); 
		_orgValue = (Settings)_settings.Clone();
	}
}