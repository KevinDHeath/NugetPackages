using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Wpf.Converters;

// https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.ivalueconverter

/// <summary>Base class for IValueConverter converters.</summary>
public abstract class ConverterBase : IValueConverter
{
	/// <summary>Converts the binding source to target.</summary>
	/// <param name="value">The value produced by the binding source.</param>
	/// <param name="targetType">The type of the binding target property.</param>
	/// <param name="parameter">The converter parameter to use.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
	/// <remarks>The data binding engine calls this method when it propagates a
	/// value from the binding source to the binding target.
	/// </remarks>
	public abstract object? Convert( object value, Type targetType, object parameter, CultureInfo culture );

	/// <summary>Converts the binding target back to source.</summary>
	/// <param name="value">The value that is produced by the binding target.</param>
	/// <param name="targetType">The type to convert to.</param>
	/// <param name="parameter">The converter parameter to use.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
	/// <remarks>The data binding engine calls this method when it propagates a value from
	/// the binding target to the binding source.
	/// The implementation of this method must be the inverse of the Convert method.
	/// </remarks>
	public abstract object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture );
}