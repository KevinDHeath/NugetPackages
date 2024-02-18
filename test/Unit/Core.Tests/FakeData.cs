namespace Core.Tests;

internal class FakeData
{
	#region Address

	internal const string cAddrPrefix = "Address_";

	internal static Address CreateAddress( bool mod = false )
	{
		return new()
		{
			Street = !mod ? "900 Front St" : "mod",
			City = !mod ? "Santa Ana" : "mod",
			Province = !mod ? "CA" : "mod",
			Postcode = !mod ? "92705" : "mod",
			Country = !mod ? "USA" : "mod"
		};
	}


	internal static void AddAddressColumns( DataTable table )
	{
		table.Columns.Add( new DataColumn
		{
			ColumnName = cAddrPrefix + nameof( Address.Street ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = cAddrPrefix + nameof( Address.City ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = cAddrPrefix + nameof( Address.Province ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = cAddrPrefix + nameof( Address.Postcode ),
			DataType = typeof( string ),
			MaxLength = 20
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = cAddrPrefix + nameof( Address.Country ),
			DataType = typeof( string ),
			MaxLength = 50
		} );
	}

	internal static void SetAddressData( DataRow rtn )
	{
		Address data = CreateAddress();
		rtn[cAddrPrefix + nameof( Address.Street )] = data.Street;
		rtn[cAddrPrefix + nameof( Address.City )] = data.City;
		rtn[cAddrPrefix + nameof( Address.Province )] = data.Province;
		rtn[cAddrPrefix + nameof( Address.Postcode )] = data.Postcode;
		rtn[cAddrPrefix + nameof( Address.Country )] = data.Country;
	}

	#endregion

	#region Company

	internal static Company CreateCompany( int id = 1, bool mod = false )
	{
		return new()
		{
			Id = id,
			Name = !mod ? "Mega Allied Services" : @"o'mod", // for branch coverage
			Address = CreateAddress( mod ),
			GovernmentNumber = !mod ? "15-1235684" : "mod",
			PrimaryPhone = !mod ? "303-290-5086" : "mod",
			SecondaryPhone = !mod ? "303-290-8688" : "mod",
			Email = !mod ? "wyatt@gmail.com" : "mod",
			NaicsCode = !mod ? "531190" : "mod",
			Private = mod,
			DepositsCount = !mod ? 2 : 1,
			DepositsBal = !mod ? 9918.77m : null
		};
	}

	internal static DataRow GetCompanyRow()
	{
		#region Create Table

		DataTable table = new( "Companies" );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.Id ),
			DataType = typeof( int ),
			AllowDBNull = false,
			AutoIncrement = true
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.Name ),
			DataType = typeof( string ),
			AllowDBNull = false,
			MaxLength = 100
		} );

		AddAddressColumns( table );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.GovernmentNumber ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.PrimaryPhone ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.SecondaryPhone ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.Email ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.NaicsCode ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.Private ),
			DataType = typeof( char )
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.DepositsCount ),
			DataType = typeof( int )
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Company.DepositsBal ),
			DataType = typeof( decimal )
		} );

		DataColumn[] keys = [table.Columns[0]];
		table.PrimaryKey = keys;

		#endregion

		DataRow rtn = table.NewRow();
		Company data = CreateCompany();

		rtn[nameof( Company.Id )] = data.Id;
		rtn[nameof( Company.Name )] = data.Name;
		SetAddressData( rtn );
		rtn[nameof( Company.GovernmentNumber )] = data.GovernmentNumber;
		rtn[nameof( Company.PrimaryPhone )] = data.PrimaryPhone;
		rtn[nameof( Company.SecondaryPhone )] = data.SecondaryPhone;
		rtn[nameof( Company.Email )] = data.Email;
		rtn[nameof( Company.NaicsCode )] = data.NaicsCode;
		rtn[nameof( Company.Private )] = data.Private.HasValue && data.Private.Value ? 'y' : 'n';
		rtn[nameof( Company.DepositsCount )] = data.DepositsCount;
		rtn[nameof( Company.DepositsBal )] = data.DepositsBal;

		return rtn;
	}

	#endregion

	#region ISOCountry

	private const string cISOCountriesFile = "isocountries.json";

	internal static List<ISOCountry> GetISOCountries()
	{
		return Global.GetJsonList<ISOCountry>( cISOCountriesFile );
	}

	internal static DataRow GetISOCountryRow()
	{
		#region Create Table

		DataTable table = new( "ISOCountries" );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( ISOCountry.Id ),
			DataType = typeof( int ),
			AllowDBNull = false,
			AutoIncrement = true
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( ISOCountry.Alpha2 ),
			DataType = typeof( string ),
			MaxLength = 2,
			AllowDBNull = false
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( ISOCountry.Alpha3 ),
			MaxLength = 3,
			DataType = typeof( string ),
			AllowDBNull = false
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( ISOCountry.Name ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		DataColumn[] keys = [table.Columns[0]];
		table.PrimaryKey = keys;

		#endregion

		DataRow rtn = table.NewRow();
		rtn[nameof( ISOCountry.Id )] = 1;
		rtn[nameof( ISOCountry.Alpha2 )] = "A";
		rtn[nameof( ISOCountry.Alpha3 )] = "A";
		rtn[nameof( ISOCountry.Name )] = "A";

		return rtn;
	}

	#endregion

	#region Person

	internal static Person CreatePerson( int id = 1, bool mod = false )
	{
		return new()
		{
			Id = id,
			FirstName = !mod ? "Wyatt" : @"o'mod", // special character handling
			MiddleName = !mod ? null : "mod",
			LastName = !mod ? "Shelton" : "mod",
			Address = CreateAddress( mod ),
			GovernmentNumber = !mod ? "666-99-4814" : "mod",
			IdProvince = !mod ? "NY" : "mod",
			IdNumber = !mod ? "104000048" : "mod",
			HomePhone = !mod ? "(201) 854-0013" : null,
			BirthDate = !mod ? new DateOnly( 1982, 8, 10 ) : new DateOnly( 2000, 2, 15 )
		};
	}

	internal static DataRow GetPersonRow()
	{
		#region Create Table

		DataTable table = new( "People" );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.Id ),
			DataType = typeof( int ),
			AllowDBNull = false,
			AutoIncrement = true
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.FirstName ),
			DataType = typeof( string ),
			AllowDBNull = false,
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.MiddleName ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.LastName ),
			DataType = typeof( string ),
			AllowDBNull = false,
			MaxLength = 50
		} );

		AddAddressColumns( table );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.GovernmentNumber ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.IdProvince ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.IdNumber ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.HomePhone ),
			DataType = typeof( string ),
			MaxLength = 50
		} );


		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Person.BirthDate ),
			DataType = typeof( DateOnly )
		} );

		DataColumn[] keys = [table.Columns[0]];
		table.PrimaryKey = keys;

		#endregion

		DataRow rtn = table.NewRow();
		Person data = CreatePerson();

		rtn[nameof( Person.Id )] = data.Id;
		rtn[nameof( Person.FirstName )] = data.FirstName;
		rtn[nameof( Person.MiddleName )] = data.MiddleName;
		rtn[nameof( Person.LastName )] = data.LastName;
		SetAddressData( rtn );
		rtn[nameof( Person.GovernmentNumber )] = data.GovernmentNumber;
		rtn[nameof( Person.IdProvince )] = data.IdProvince;
		rtn[nameof( Person.IdNumber )] = data.IdNumber;
		rtn[nameof( Person.HomePhone )] = data.HomePhone;
		rtn[nameof( Person.BirthDate )] = data.BirthDate;

		return rtn;
	}

	#endregion

	#region Postcode

	private const string cPostCodesFile = "postcodes.json";

	internal static List<Postcode> GetPostcodes()
	{
		return Global.GetJsonList<Postcode>( cPostCodesFile );
	}

	internal static DataRow GetPostcodeRow()
	{
		#region Create Table

		DataTable table = new( "Postcodes" );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Postcode.Id ),
			DataType = typeof( int ),
			AllowDBNull = false,
			AutoIncrement = true
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Postcode.Code ),
			DataType = typeof( string ),
			MaxLength = 10,
			AllowDBNull = false
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Postcode.Province ),
			MaxLength = 10,
			DataType = typeof( string ),
			AllowDBNull = false
		} );
		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Postcode.County ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Postcode.City ),
			DataType = typeof( string ),
			MaxLength = 50
		} );

		DataColumn[] keys = [table.Columns[0]];
		table.PrimaryKey = keys;

		#endregion

		DataRow rtn = table.NewRow();
		rtn[nameof( Postcode.Id )] = 1;
		rtn[nameof( Postcode.Code )] = "A";
		rtn[nameof( Postcode.Province )] = "A";
		rtn[nameof( Postcode.County )] = "A";
		rtn[nameof( Postcode.City )] = "A";

		return rtn;
	}

	#endregion

	#region Province

	internal const string cProvincesFile = "provinces.json";

	internal static List<Province> GetProvinces()
	{
		return Global.GetJsonList<Province>( cProvincesFile );
	}

	internal static DataRow GetProvinceRow()
	{
		#region Create Table

		DataTable table = new( "Provinces" );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Province.Id ),
			DataType = typeof( int ),
			AllowDBNull = false,
			AutoIncrement = true
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Province.Code ),
			DataType = typeof( string ),
			MaxLength = 10,
		} );

		table.Columns.Add( new DataColumn
		{
			ColumnName = nameof( Province.Name ),
			DataType = typeof( string ),
			AllowDBNull = false,
			MaxLength = 50
		} );

		DataColumn[] keys = [table.Columns[0]];
		table.PrimaryKey = keys;

		#endregion

		DataRow rtn = table.NewRow();
		rtn[nameof( Province.Id )] = 1;
		rtn[nameof( Province.Code )] = "A";
		rtn[nameof( Province.Name )] = "A";

		return rtn;
	}

	#endregion

	#region User

	internal const string cUserFile = "user.json";

	internal static User CreateUser( int id = 1, bool mod = false )
	{
		DateOnly birthDate = !mod ? new DateOnly( 1981, 8, 5 ) : new DateOnly( 2000, 8, 5 );
		return new()
		{
			Id = id,
			Name = !mod ? "John Doe" : "mod",
			BirthDate = birthDate,
			Email = !mod ? "john@doe-family.com" : "mod",
			Gender = !mod ? Genders.Male : Genders.Unknown,
			Age = ModelBase.CalculateAge( birthDate )
		};
	}

	internal static List<User> GetUserList()
	{
		return (List<User>)User.GetUsers();
	}

	#endregion
}