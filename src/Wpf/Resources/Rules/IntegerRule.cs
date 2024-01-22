// Ignore Spelling: Rqd

using System.Globalization;
using System.Windows.Controls;
using Common.Wpf.Converters;

namespace Common.Wpf.Rules;

/// <summary>Validation rule for integer values with an optional minimum and maximum range.</summary>
public class IntegerRule : RuleBase
{
	#region Properties and Constructor

	/// <summary>Minimum value. The default is <c>-2,147,483,648</c>.</summary>
	public int Min { get; set; }

	/// <summary>Maximum value. The default is <c>2,147,483,647</c>.</summary>
	public int Max { get; set; }

	/// <summary>Indicates if a value is required. The default is <see langword="false"/>.</summary>
	public bool Rqd { get; set; }

	/// <summary>Initializes a new instance of the IntegerRule class.</summary>
	public IntegerRule()
	{
		Min = int.MinValue;
		Max = int.MaxValue;
	}

	#endregion

	/// <inheritdoc/>
	public override ValidationResult Validate( object value, CultureInfo cultureInfo )
	{
		int res;
		if( value is null ) // Handle null value
		{
			if( Rqd ) { return new ValidationResult( false, cEnterValue ); }
			else { return ValidationResult.ValidResult; }
		}
		else if( value is string str ) // Handle RawProposedValue
		{
			if( string.IsNullOrWhiteSpace( str ) )
			{
				if( Rqd ) { return new ValidationResult( false, cEnterValue ); }
				else { return ValidationResult.ValidResult; }
			}

			str = str.Trim();
			bool ok = StringConverter.TryParse( ref str, out res, cultureInfo );
			if( !ok ) { return PleaseEnter( Property, cNumeric ); }
		}
		else if( value is int val ) // Handle ConvertedProposedValue
		{
			res = val;
		}
		else { return new ValidationResult( false, cUnknownValType ); }

		// Check value is within the range
		if( res < Min || res > Max )
		{
			return new ValidationResult( false, string.Format( cMinMaxRange, Min, Max ) );
		}

		return ValidationResult.ValidResult;
	}
}