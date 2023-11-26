using System.Globalization;
using System.Windows.Controls;

namespace Common.Wpf.Rules;

/// <summary>Validation rule for editable combo boxes.</summary>
public class ComboEditRule : RuleBase
{
	/// <inheritdoc/>
	public override ValidationResult Validate( object value, CultureInfo cultureInfo )
	{
		// The value is null when an invalid string is entered
		if( value is null ) { return PleaseEnter( Property, cDefinedValue ); }

		return ValidationResult.ValidResult;
	}
}