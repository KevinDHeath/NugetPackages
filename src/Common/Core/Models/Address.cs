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

	#region Constructor

	/// <summary>Initializes a new instance of the Address class.</summary>
	public Address()
    { }

	#endregion

	#region Internal Methods

	/// <inheritdoc/>
	internal new bool Equals( object? obj )
	{
		if( obj is null || obj is not Address other ) { return false; }

		if( other.Street != Street ) { return false; }
		if( other.City != City ) { return false; }
		if( other.Province != Province ) { return false; }
		if( other.Postcode != Postcode ) { return false; }
		if( other.Country != Country ) { return false; }

		return true;
	}

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

	internal static void UpdateAddress( Address obj, Address mod, Address cur, IList<string> sql,
		string prefix = "" )
	{
		if( ModelData.Changed( prefix + nameof( Street ), sql, obj.Street, mod.Street, cur.Street ) )
		{ mod.Street = cur.Street; }

		if( ModelData.Changed( prefix + nameof( City ), sql, obj.City, mod.City, cur.City ) )
		{ mod.City = cur.City; }

		if( ModelData.Changed( prefix + nameof( Province ), sql, obj.Province, mod.Province, cur.Province ) )
		{ mod.Province = cur.Province; }

		if( ModelData.Changed( prefix + nameof( Postcode ), sql, obj.Postcode, mod.Postcode, cur.Postcode ) )
		{ mod.Postcode = cur.Postcode; }

		if( ModelData.Changed( prefix + nameof( Country ), sql, obj.Country, mod.Country, cur.Country ) )
		{ mod.Country = cur.Country; }
	}

	#endregion
}