using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using TestWPF.UI.Rules;

namespace TestWPF.UI.Converters;

[ValueConversion( typeof( ValidationRule ), typeof( bool ) )]
public class UserRuleConverter : IValueConverter
{
	/// <inheritdoc/>
	public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		=> value is UserRule;

	/// <inheritdoc/>
	public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		=> value;
}