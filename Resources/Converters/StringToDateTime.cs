using System.Globalization;
using System.Windows.Data;
using Common.Core.Converters;
using Common.Wpf.Rules;

namespace Common.Wpf.Converters;

/// <summary>Converts a date string to a DataTime object.</summary>
[ValueConversion( typeof( string ), typeof( DateTime ) )]
public class StringToDateTime : ConverterBase
{
	/// <inheritdoc/>
	public override object? Convert( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the string to a DateTime
		if( value is not null && value is string input )
		{
			if( StringConverter.TryParse( ref input, out DateTime rtn, culture ) ) { return rtn; }
		}

		return Activator.CreateInstance( targetType );
	}

	/// <inheritdoc/>
	/// <param name="value">The value that is produced by the binding target.</param>
	/// <param name="targetType">The type to convert to.</param>
	/// <param name="parameter">A custom date format string can be provided in the converter parameter.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
	{
		if( value is not null && value is DateTime )
		{
			return RuleBase.CustomDateFormat( value, parameter, culture );
		}

		return string.Empty;
	}
}