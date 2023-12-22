using System.Globalization;
using System.Windows.Data;

namespace Common.Wpf.Converters;

/// <summary>Converts a DateOnly to a string.</summary>
[ValueConversion( typeof( DateOnly ), typeof( string ) )]
public partial class DateOnlyToString : ConverterBase
{
	/// <inheritdoc/>
	public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the DateOnly to a string
		if( value is not null && value is DateOnly val )
		{
			if( val != DateOnly.MinValue ) return val.ToString();
		}

		return string.Empty;
	}

	/// <inheritdoc/>
	public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the string back to a DateOnly
		if( value is not null and string input )
		{
			if( StringConverter.TryParse( ref input, out DateOnly rtn, culture ) ) { return rtn; }
		}

		return Activator.CreateInstance( targetType );
	}
}