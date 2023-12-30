using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sample.Wpf.Rules;

internal class UserRule : ValidationRule
{
	public override ValidationResult Validate( object value, CultureInfo cultureInfo )
	{
		if( value == null || value is not BindingGroup bg )
			return ValidationResult.ValidResult;

		UserViewModel? vm = bg.Items[0] as UserViewModel;

		// Get the converted proposed values
		bool ageResult = bg.TryGetValue( vm, "Age", out object ageValue );
		bool nameResult = bg.TryGetValue( vm, "Name", out object nameValue );

		if( ageResult && ageValue is not null && nameResult && nameValue is not null )
		{
			int age = (int)ageValue;
			string name = (string)nameValue;

			if( ( age < 69 ) && ( name.Equals( "Kevin", StringComparison.OrdinalIgnoreCase ) ) )
				return new ValidationResult( false, "Kevin cannot be less that sixty nine!" );
		}

		return ValidationResult.ValidResult;
	}
}
