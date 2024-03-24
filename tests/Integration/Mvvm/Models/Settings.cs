using System.Text.Json.Serialization;
using Common.Core.Classes;

namespace Sample.Mvvm.Models;

public class Settings : ModelBase, ICloneable
{
	internal const StringComparison cCompare = StringComparison.OrdinalIgnoreCase;

	#region Properties

	private double? _fontSize;
	private bool? _booleanVal;
	private string? _dataFile;
	private string? _dataFolder;
	private decimal? _decimalVal;
	private double? _doubleVal;
	private int? _integerRule;
	private int? _integerVal;
	private DateOnly? _dateOnlyVal;
	private string? _dateTimeVal;
	private string? _stringRule;
	private string? _stringVal;

	public double? FontSize
	{
		get => _fontSize;
		set
		{
			if( value is not null && value.Equals( _fontSize ) ) { return; }
			_fontSize = value;
		}
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public bool? BooleanVal
	{
		get => _booleanVal;
		set => _booleanVal = value;
	}

	public TestTypes ComboBoxVal { get; set; }

	public TestTypes ComboEditRule { get; set; }

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? DataFolder
	{
		get => _dataFolder;
		set
		{
			if( value is not null && value.Equals( _dataFolder ) ) { return; }
			_dataFolder = value;
		}
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? DataFile
	{
		get => _dataFile;
		set
		{
			if( value is not null && value.Equals( _dataFile ) ) { return; }
			_dataFile = value;
		}
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public DateOnly? DateOnlyVal
	{
		get { return _dateOnlyVal; }
		set { _dateOnlyVal = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? DateTimeVal
	{
		get { return _dateTimeVal; }
		set { _dateTimeVal = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public decimal? DecimalVal
	{
		get { return _decimalVal; }
		set { _decimalVal = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public double? DoubleVal
	{
		get { return _doubleVal; }
		set { _doubleVal = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public int? IntegerRule
	{
		get { return _integerRule; }
		set { _integerRule = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public int? IntegerVal
	{
		get { return _integerVal; }
		set { _integerVal = value; }
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? StringRule
	{
		get => _stringRule;
		set
		{
			if( value is not null && value.Equals( _stringRule ) ) { return; }
			_stringRule = value;
		}
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? StringVal
	{
		get => _stringVal;
		set
		{
			if( value is not null && value.Equals( _stringVal ) ) { return; }
			_stringVal = value;
		}
	}

	public List<User> Users { get; set; } = [];

	#endregion

	public object Clone()
	{
		return MemberwiseClone();
	}

	public bool HasChanges( Settings source )
	{
		if( FontSize != source.FontSize ) { return true; }
		if( BooleanVal != source.BooleanVal ) { return true; }
		if( ComboBoxVal != source.ComboBoxVal ) { return true; }
		if( ComboEditRule != source.ComboEditRule ) { return true; }
		if( DataFile != source.DataFile ) { return true; }
		if( DataFolder != source.DataFolder ) { return true; }
		if( DateOnlyVal != source.DateOnlyVal ) { return true; }
		if( DateTimeVal != source.DateTimeVal ) { return true; }
		if( DecimalVal != source.DecimalVal ) { return true; }
		if( DoubleVal != source.DoubleVal ) { return true; }
		if( IntegerRule != source.IntegerRule ) { return true; }
		if( IntegerVal != source.IntegerVal ) { return true; }
		if( StringRule != source.StringRule ) { return true; }
		if( StringVal != source.StringVal ) { return true; }
		return false;
	}
}