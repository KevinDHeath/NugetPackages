using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Core.Converters;

#region Nullable Integer

/// <summary>Converts a nullable integer (System.Int32) string to or from JSON.</summary>
public class JsonIntegerString : JsonConverter<int?>
{
	/// <summary>
	/// Gets a value that indicates whether null should be passed to the converter on serialization.
	/// </summary>
	public override bool HandleNull => true;

	/// <summary>Reads and converts the JSON to a nullable integer.</summary>
	/// <param name="reader">The reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value or <see langword="null"/> if the value could not be converted.</returns>
	public override int? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		switch( reader.TokenType )
		{
			case JsonTokenType.Number:
				return reader.GetInt32();

			case JsonTokenType.String:
				string? value = reader.GetString();
				value ??= string.Empty;
				if( StringConverter.TryParse( ref value, out int result ) ) { return result; }
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
	public override void Write( Utf8JsonWriter writer, int? value, JsonSerializerOptions options )
	{
		if( value.HasValue )
		{
			writer.WriteNumberValue( value.Value );
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}

#endregion