using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using TestWPF.Core.ViewModels;

namespace TestWPF.Rules;

internal class UserRule : ValidationRule
{
	public override ValidationResult Validate( object value, CultureInfo cultureInfo )
	{
		if( value == null || value is not BindingGroup bg )
			return ValidationResult.ValidResult;

		WpfTestViewModel? vm = bg.Items[0] as WpfTestViewModel;

		// Get the converted proposed values
		bool ageResult = bg.TryGetValue( vm?.CurrentUser, "Age", out object ageValue );
		bool nameResult = bg.TryGetValue( vm?.CurrentUser, "Name", out object nameValue );

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
