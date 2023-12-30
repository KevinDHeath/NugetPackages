using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Common.Wpf.Converters;

/// <summary>Converts a decimal to a string.</summary>
[ValueConversion( typeof( decimal ), typeof( string ) )]
public partial class DecimalToString : ConverterBase
{
	internal static readonly Regex s_doubleRegex = DecRegEx();

	[GeneratedRegex( "[^0-9.-]+", RegexOptions.Compiled )]
	private static partial Regex DecRegEx();

	/// <inheritdoc/>
	/// <param name="value">The value produced by the binding source.</param>
	/// <param name="targetType">The type of the binding target property.</param>
	/// <param name="parameter">An integer containing the number of decimal places (0-10)
	/// can be provided in the converter parameter.<br/>
	/// The default number of decimal places is 2.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the decimal to a string
		if( value is not null && value is decimal val )
		{
			return val.ToString( Format( parameter, culture ), culture );
		}

		return string.Empty;
	}

	/// <inheritdoc/>
	public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the string back to a decimal
		if( value is not null and string input )
		{
			input = s_doubleRegex.Replace( input, string.Empty );
			if( StringConverter.TryParse( ref input, out decimal rtn, culture ) ) { return rtn; }
		}

		return Activator.CreateInstance( targetType );
	}

	private static string Format( object? value, CultureInfo culture )
	{
		if( value is null ) { return "N2"; }

		int dec = 2;
		if( value is string val )
		{
			if( StringConverter.TryParse( ref val, out int rtn, culture ) && rtn is >= 0 and <= 10 )
			{
				dec = rtn;
			}
		}

		return "N" + dec;
	}
}