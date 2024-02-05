using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;
using Common.Core.Classes;

namespace Common.Core.Models;

/// <summary>This class contains details of an Address.</summary>
public class Address : ModelBase, IEquatable<object>
{
	#region Private Variables

	private string? _street;
	private string? _city;
	private string? _province;
	private string? _postcode;
	private string? _country;

	#endregion

	#region Properties

	/// <summary>Gets or sets the Street.</summary>
	[MaxLength(50)]
	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? Street
	{
		get => _street;
		set
		{
			if( value != _street )
			{
				_street = SetNullString( value );
				OnPropertyChanged( nameof( Street ) );
				OnPropertyChanged( nameof( FullAddress ) );
			}
		}
	}

	/// <summary>Gets or sets the City.</summary>
	[MaxLength( 50 )]
	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? City
	{
		get => _city;
		set
		{
			if( value != _city )
			{
				_city = SetNullString( value );
				OnPropertyChanged( nameof( City ) );
				OnPropertyChanged( nameof( FullAddress ) );
			}
		}
	}

	/// <summary>Gets or sets the Province.</summary>
	[MaxLength( 50 )]
	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? Province
	{
		get => _province;
		set
		{
			if( value != _province )
			{
				_province = SetNullString( value );
				OnPropertyChanged( nameof( Province ) );
				OnPropertyChanged( nameof( FullAddress ) );
			}
		}
	}

	/// <summary>Gets or sets the Postal Code.</summary>
	[MaxLength( 20 )]
	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? Postcode
	{
		get => _postcode;
		set
		{
			if( value != _postcode )
			{
				_postcode = SetNullString( value );
				OnPropertyChanged( nameof( Postcode ) );
			}
		}
	}

	/// <summary>Gets or sets the Country.</summary>
	[MaxLength( 50 )]
	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public string? Country
	{
		get => _country;
		set
		{
			if( value != _country )
			{
				_country = SetNullString( value );
				OnPropertyChanged( nameof( Country ) );
			}
		}
	}

	/// <summary>Gets the Full Address.</summary>
	[NotMapped]
	[JsonIgnore]
	public string FullAddress
	{
		get
		{
			var values = new[] { Street?.Trim(), City?.Trim(), Province?.Trim() };
			return string.Join( ", ", values.Where( s => !string.IsNullOrEmpty( s ) ) );
		}
	}

	#endregion

	#region Public Methods

	/// <inheritdoc/>
	/// <param name="obj">An Address object to compare with this object.</param>
	public new bool Equals( object? obj )
	{
		if( obj is null || obj is not Address other ) { return false; }

		if( other.Street != Street ) { return false; }
		if( other.City != City ) { return false; }
		if( other.Province != Province ) { return false; }
		if( other.Postcode != Postcode ) { return false; }
		if( other.Country != Country ) { return false; }

		return true;
	}

	/// <summary>Builds the SQL script for any value changes.</summary>
	/// <param name="obj">Address object containing the original values.</param>
	/// <param name="mod">Address object containing the modified values.</param>
	/// <param name="cur">Address object containing the current values.</param>
	/// <param name="list">List of SQL script changes.</param>
	/// <param name="prefix">Table column name prefix for Address fields (if required).</param>
	/// <returns><see langword="true"/> if the current object is equal to the object containing the
	/// original values; otherwise, <see langword="false"/>.</returns>
	/// <remarks>This method assumes that the table column names are the same as the property names.</remarks>
	public static bool UpdateAddress( Address obj, Address mod, Address cur, IList<string> list,
		string prefix = "" )
	{
		if( !cur.Equals( obj ) )
		{
			mod.Street = cur.Street;
			mod.City = cur.City;
			mod.Province = cur.Province;
			mod.Postcode = cur.Postcode;
			mod.Country = cur.Country;
			return false;
		}

		if( obj.Street != mod.Street ) { ModelData.SetSQLColumn( prefix + nameof( Street ), mod.Street, list ); }
		if( obj.City != mod.City ) { ModelData.SetSQLColumn( prefix + nameof( City ), mod.City, list ); }
		if( obj.Province != mod.Province ) { ModelData.SetSQLColumn( prefix + nameof( Province ), mod.Province, list ); }
		if( obj.Postcode != mod.Postcode ) { ModelData.SetSQLColumn( prefix + nameof( Postcode ), mod.Postcode, list ); }
		if( obj.Country != mod.Country ) { ModelData.SetSQLColumn( prefix + nameof( Country ), mod.Country, list ); }
		return true;
	}

	#endregion

	#region Internal Methods

	internal static Address BuildAddress( DataRow row, string prefix = "" )
	{
		return new Address()
		{
			Street = row.Field<string?>( prefix + nameof( Street ) ),
			City = row.Field<string?>( prefix + nameof( City ) ),
			Province = row.Field<string?>( prefix + nameof( Province ) ),
			Postcode = row.Field<string?>( prefix + nameof( Postcode ) ),
			Country = row.Field<string?>( prefix + nameof( Country ) )
		};
	}

	#endregion
}