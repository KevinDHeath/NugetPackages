using System.Globalization;
using System.Windows.Controls;

namespace Common.Wpf.Rules;

/// <summary>
/// Validation rule for required values, with no leading or trailing blanks, and an optional
/// minimum number of characters.
/// </summary>
public class RequiredRule : RuleBase
{
	#region Properties and Constructor

	/// <summary>Minimum number of characters. The default is 1.</summary>
	public int Min { get; set; }

	/// <summary>Initializes a new instance of the RequiredRule class.</summary>
	public RequiredRule()
	{
		Min = 1;
	}

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