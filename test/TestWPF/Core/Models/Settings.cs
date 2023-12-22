using Common.Core.Classes;
using System.Text.Json.Serialization;

namespace TestWPF.Core.Models;

public class Settings : ModelBase, ICloneable
{
	#region Properties

	private double? _fontSize;
	private string? _dataFile;
	private string? _dataFolder;
	private decimal? _decimalVal;
	private int? _integerRule;
	private int? _integerVal;
	private DateOnly? _dateOnlyVal;
	private string? _dateTimeVal;
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
	public decimal? DecimalVal
	{
		get { return _decimalVal; }
		set { _decimalVal = value; }
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
	public string? StringVal
	{
		get => _stringVal;
		set
		{
			if( value is not null && value.Equals( _stringVal ) ) { return; }
			_stringVal = value;
		}
	}

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

	public TestTypes TestType { get; set; }

	#endregion

	public object Clone()
	{
		return MemberwiseClone();
	}

	public bool HasChanges( Settings source )
	{
		if( FontSize != source.FontSize ) { return true; }
		if( DataFile != source.DataFile ) { return true; }
		if( DataFolder != source.DataFolder ) { return true; }
		if( DateOnlyVal != source.DateOnlyVal ) { return true; }
		if( DateTimeVal != source.DateTimeVal ) { return true; }
		if( DecimalVal != source.DecimalVal ) { return true; }
		if( IntegerRule != source.IntegerRule ) { return true; }
		if( IntegerVal != source.IntegerVal ) { return true; }
		if( StringVal != source.StringVal ) { return true; }
		if( TestType != source.TestType ) { return true; }
		return false;
	}

	private static string _settingsFile = string.Empty;

	public static Settings? Load( string settingsFile )
	{
		if( string.IsNullOrWhiteSpace( settingsFile ) ) {  return null; }
		_settingsFile = ReflectionHelper.AddCurrentPath( settingsFile );
		return JsonHelper.DeserializeFile<Settings>( _settingsFile );
	}

	public static bool Save( Settings settings )
	{
		if( string.IsNullOrWhiteSpace( _settingsFile ) ) { return false; }
		System.Text.Json.JsonSerializerOptions options = JsonHelper.DefaultSerializerOptions();
		options.WriteIndented = true;
		return JsonHelper.Serialize( settings, _settingsFile, options );
	}
}