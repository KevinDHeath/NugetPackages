using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Core.Converters;

#region Nullable DateOnly

/// <summary>Converts a nullable DateOnly string to or from JSON.</summary>
public class JsonDateOnlyString : JsonConverter<DateOnly?>
{
	/// <summary>
	/// Gets a value that indicates whether null should be passed to the converter on serialization.
	/// </summary>
	public override bool HandleNull => true;

	/// <summary>Reads and converts the JSON to a nullable DateOnly.</summary>
	/// <param name="reader">The reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value or <see langword="null"/> if the value could not be converted.</returns>
	public override DateOnly? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		switch( reader.TokenType )
		{
			case JsonTokenType.String:
				string? value = reader.GetString();
				value ??= string.Empty;
				if( StringConverter.TryParse( ref value, out DateOnly result ) ) { return result; }
				break;

			default:
				break;
		}

		return null;
	}

	/// <summary>Writes a specified value as JSON.</summary>
	/// <param name="writer">The writer to write to.</param>
	/// <param name="value">The value to convert to JSON.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	public override void Write( Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options )
	{
		if( value.HasValue )
		{
			writer.WriteStringValue( value.Value.ToString() );
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}

#endregion