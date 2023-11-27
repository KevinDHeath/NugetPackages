using System.Globalization;
using System.Windows.Controls;
using Common.Wpf.Converters;

namespace Common.Wpf.Rules;

/// <summary>Base class for validation rules.</summary>
public abstract class RuleBase : ValidationRule
{
	/// <summary>Property name being validated.</summary>
	public string? Property { get; set; }

	#region Error Messages

	internal const string cEnterDate = "Please enter a valid date.";
	internal const string cEnterNumber = "Please enter a number.";
	internal const string cEnterValue = "Please enter a value.";
	internal const string cMinCharacters = "At least {0:N0} characters are required.";
	internal const string cMinMaxRange = "Value must be between {0:N0} and {1:N0}";
	internal const string cNoBlanksAtEnd = "Blanks are not allowed at the end.";
	internal const string cNoBlanksAtStart = "Blanks are not allowed at the beginning.";
	internal const string cUnknownValType = "Unknown value type.";

	internal const string cDefinedValue = "a defined value.";
	internal const string cNumeric = "a number.";

	#endregion

	#region ValidationRule Implementation

	/// <summary>Performs validation checks on a value.</summary>
	/// <param name="value">The value from the binding target to check.</param>
	/// <param name="cultureInfo">The culture to use in this rule.</param>
	/// <returns>A ValidationResult object.</returns>
	public abstract override ValidationResult Validate( object value, CultureInfo cultureInfo );

	#endregion

	#region Helper Methods

	internal static ValidationResult PleaseEnter( string? prop, string text )
	{
		string msg = !string.IsNullOrWhiteSpace( prop ) ? $"{prop} must be {text}" : $"Please enter {text}";
		return new ValidationResult( false, msg );
	}

	// Converts a DateOnly or DateTime to a string using a custom format.
	internal static string CustomDateFormat( object value, object parameter, CultureInfo cultureInfo )
	{
		if( parameter is not null && parameter is string format )
		{
			// Try using the format string provided in the parameter
			// Unknown single char specifier can throw a run-time FormatException
			string formatted; 
			try
			{
				switch( value )
				{
					case DateOnly dateOnly:
						formatted = dateOnly.ToString( format );
						break;
					case DateTime dateTime:
						formatted = dateTime.ToString( format );
						break;
					default:
						return string.Empty;
				}

				bool chValidity = StringConverter.TryParse( ref formatted, out DateTime _, cultureInfo );
				if( chValidity ) return formatted;
			}
			catch { }
		}

		return value switch
		{
			DateOnly dateOnly => dateOnly.ToString(),
			DateTime dateTime => dateTime.ToString( "g" ),
			_ => string.Empty,
		};
	}

	#endregion
}