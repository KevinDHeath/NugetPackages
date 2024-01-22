using System.Globalization;
using System.Windows.Controls;

namespace Common.Wpf.Rules;

/// <summary>
/// Validation rule for required values, with no leading or trailing blanks, and an optional
/// minimum number of characters.
/// </summary>
public class RequiredRule : RuleBase
{
	#region Properties

	/// <summary>Minimum number of characters. The default is <c>1</c>.</summary>
	public int Min { get; set; } = 1;

	#endregion

	/// <inheritdoc/>
	public override ValidationResult Validate( object value, CultureInfo cultureInfo )
	{
		if( value is not string str || string.IsNullOrWhiteSpace( str ) )
		{
			return new ValidationResult( false, cEnterValue );
		}
		else if( str.StartsWith( " " ) )
		{
			return new ValidationResult( false, cNoBlanksAtStart );
		}
		else if( str.EndsWith( " " ) )
		{
			return new ValidationResult( false, cNoBlanksAtEnd );
		}
		else if( str.Length < Min )
		{
			return new ValidationResult( false, string.Format( cMinCharacters, Min ) );
		}

		return ValidationResult.ValidResult;
	}
}