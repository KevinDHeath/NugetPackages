using System;
using System.Globalization;

namespace Common.Wpf;

internal class StringConverter
{
	internal static bool TryParse( ref string value, out DateOnly result, CultureInfo? culture = null )
	{
		return DateOnly.TryParse( value, culture, DateTimeStyles.None, out result );
	}

	internal static bool TryParse( ref string value, out DateTime result, CultureInfo? culture = null )
	{
		return DateTime.TryParse( value, culture, DateTimeStyles.None, out result );
	}

	internal static bool TryParse( ref string value, out int result, CultureInfo? culture = null )
	{
		return int.TryParse( value, NumberStyles.Integer | NumberStyles.AllowThousands |
			NumberStyles.AllowTrailingSign, culture, out result );
	}

	internal static bool TryParse( ref string value, out decimal result, CultureInfo? culture = null )
	{
		return decimal.TryParse( value, NumberStyles.Currency, culture, out result );
	}
}