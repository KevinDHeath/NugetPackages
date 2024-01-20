using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Core.Converters;

#region Nullable Boolean

/// <summary>Converts a nullable boolean (System.Boolean) string to or from JSON.</summary>
public class JsonBooleanString : JsonConverter<bool?>
{
	/// <summary>Reads and converts the JSON to a nullable boolean.</summary>
	/// <param name="reader">The reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value or <see langword="null"/> if the value could not be converted.</returns>
	public override bool? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		switch( reader.TokenType )
		{
			case JsonTokenType.False:
				return false;

			case JsonTokenType.True:
				return true;

			case JsonTokenType.String:
				string? value = reader.GetString();
				if( value is not null && StringConverter.TryParse( ref value, out bool result ) )
				{ return result; }
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
	public override void Write( Utf8JsonWriter writer, bool? value, JsonSerializerOptions options )
	{
		if( value.HasValue )
		{
			writer.WriteBooleanValue( value.Value );
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}

#endregion