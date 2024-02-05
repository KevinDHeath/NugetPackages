using System.Text.Json.Nodes;

namespace Core.Tests.Converters;

public class JsonConverterTests
{
	internal static JsonSerializerOptions ConfigureConverters()
	{
		JsonSerializerOptions rtn = JsonHelper.DefaultSerializerOptions();
		rtn.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
		rtn.Converters.Add( new JsonBooleanString() );
		rtn.Converters.Add( new JsonDateOnlyString() );
		rtn.Converters.Add( new JsonDecimalString() );
		rtn.Converters.Add( new JsonIntegerString() );

		return rtn;
	}

	[Fact]
	public void Read_should_be_JsonData()
	{
		// Arrange
		string fileName = Global.cDataFolder + Global.cGlobalData;

		// Act
		Global? result = JsonHelper.DeserializeFile<Global>( fileName, ConfigureConverters() );

		// Assert
		_ = result.Should().BeAssignableTo<Global>();
	}

	[Theory]
	[InlineData( null, null, null, null )]
	[InlineData( true, null, null, null )]
	[InlineData( false, null, null, null )]
	[InlineData( "Y", "2000-01-01", "0", "0" )]
	[InlineData( "N", "X", null, null )]
	[InlineData( "1", null, 1.45, 1 )]
	[InlineData( "0", 1, "A", "A" )]
	[InlineData( "A", null, null, 1.45 )]
	public void ReadWrite( object? bVal, object? doVal, object? dVal, object? iVal )
	{
		// Arrange
		string json = new JsonObject()
		{
			{ "Boolean", bVal is not null and bool b ? b : bVal?.ToString() },
			{ "DateOnly", doVal is not null and int di ? di : doVal?.ToString() },
			{ "Decimal", dVal is not null and double d ? d : dVal?.ToString() },
			{ "Integer", iVal is not null and int i ? i : iVal?.ToString() }
		}.ToString();

		// Act
		JsonSerializerOptions options = ConfigureConverters();
		Global? obj = JsonHelper.DeserializeJson<Global>( ref json, options );
		string? str = JsonHelper.Serialize( obj, options );
		bool result = json is not null && str is not null;

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void Write_nulls_should_have_values()
	{
		// Arrange
		var obj = new Global();

		// Act
		string? result = JsonHelper.Serialize( obj, ConfigureConverters() );

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public void Write_should_not_be_null_or_empty()
	{
		// Arrange
		Global obj = new()
		{
			Boolean = true,
			DateOnly = new DateOnly( 2000, 1, 1 ),
			Decimal = (decimal)123.45,
			Integer = 123
		};

		// Act
		string? result = JsonHelper.Serialize( obj, ConfigureConverters() );

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
	}
}