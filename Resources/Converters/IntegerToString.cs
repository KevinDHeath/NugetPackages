using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using Common.Core.Converters;

namespace Common.Wpf.Converters;

/// <summary>Converts a signed 32-bit integer to a string.</summary>
[ValueConversion( typeof( int ), typeof( string ) )]
public partial class IntegerToString : ConverterBase
{
	private static readonly Regex s_intRegex = IntRegEx();

	[GeneratedRegex( "[^0-9-]+", RegexOptions.Compiled )]
	private static partial Regex IntRegEx();

	/// <inheritdoc/>
	public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the int to a string
		if( value is not null && value is int val )
		{
			return val.ToString( "N0", culture );
		}

		return string.Empty;
	}

	/// <inheritdoc/>
	public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the string back to an int
		if( value is not null && value is string input )
		{
			input = s_intRegex.Replace( input, string.Empty );
			if( StringConverter.TryParse( ref input, out int rtn, culture ) ) { return rtn; }
		}

		return Activator.CreateInstance( targetType );
	}
}