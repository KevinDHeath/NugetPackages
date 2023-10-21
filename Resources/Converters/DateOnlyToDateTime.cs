using System.Globalization;
using System.Windows.Data;

namespace Common.Wpf.Converters;

/// <summary>Converts a DataOnly object to a DataTime object.</summary>
[ValueConversion( typeof( DateOnly ), typeof( DateTime ) )]
public class DateOnlyToDateTime : ConverterBase
{
	/// <inheritdoc/>
	public override object? Convert( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the DateOnly to a DateTime
		if( value is not null && value is DateOnly val )
		{
			return val.ToDateTime( TimeOnly.MinValue );
		}

		return Activator.CreateInstance( targetType );
	}

	/// <inheritdoc/>
	public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
	{
		// Convert the DateTime back to a DateOnly
		if( value is not null && value is DateTime val )
		{
			return DateOnly.FromDateTime( val );
		}

		return Activator.CreateInstance( targetType );
	}
}