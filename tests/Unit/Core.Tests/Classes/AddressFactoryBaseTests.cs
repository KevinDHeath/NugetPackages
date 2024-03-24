namespace Core.Tests.Classes;

[Collection( "Sequential" )]
public class AddressFactoryAlpha2Tests : AddressFactoryBase
{
	[Fact]
	public void CheckCountryCode_should_be_false()
	{
		// Arrange
		string code = "123";

		// Act
		bool result = CheckCountryCode( code );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void CheckCountryCode_null_should_be_false()
	{
		// Arrange
		string? code = null;

		// Act
		bool result = CheckCountryCode( code );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void Countries_list_should_be_empty()
	{
		// Arrange (with branch coverage)
		UseAlpha2 = true;
		_ = DefaultCountry;

		// Assert
		_ = Countries.Should().BeEmpty();
	}

	[Fact]
	public void GetPostcode_should_be_null()
	{
		// Arrange
		string? code = null;

		// Act
		Postcode? result = GetPostcode( code );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void GetProvinceName_should_be_empty()
	{
		// Arrange
		UseAlpha2 = true;

		// Act (with branch coverage)
		string result = GetProvinceName( null );
		string result1 = GetProvinceName( "ABCDEFGHIJK" );
		string result2 = GetProvinceName( "ABC" );

		// Assert
		_ = result.Should().BeEmpty();
		_ = result1.Should().BeEmpty();
		_ = result2.Should().BeEmpty();
	}

	[Fact]
	public void Postcode_list_should_be_empty()
	{
		// Arrange
		UseAlpha2 = true;

		// Assert
		_ = Postcodes.Should().BeEmpty();
	}

	[Fact]
	public void Provinces_list_should_be_empty()
	{
		// Arrange
		UseAlpha2 = true;

		// Assert
		_ = Provinces.Should().BeEmpty();
	}

	[Fact]
	public void UseAlpha2_should_be_true()
	{
		// Arrange
		UseAlpha2 = true;

		// Act
		bool result = UseAlpha2;

		// Assert
		_ = result.Should().BeTrue();
	}
}

[Collection( "Sequential" )]
public class AddressFactoryAlpha3Tests : AddressFactoryBase
{
	#region Constructor

	public AddressFactoryAlpha3Tests()
	{
		UseAlpha2 = false;
		_ = DefaultCountry;
		DefaultCountry = "USA";

		SetCountries( FakeData.GetISOCountries() );
		Postcodes = FakeData.GetPostcodes();
		PostcodeCount = Postcodes.Count;
		Provinces = FakeData.GetProvinces();
	}

	#endregion

	[Fact]
	public void CheckCountryCode_should_be_false()
	{
		// Arrange
		SetCountries( FakeData.GetISOCountries() );

		// Act
		bool result = CheckCountryCode( "1234" );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void CheckCountryCode_should_be_true()
	{
		// Arrange
		string code = "USA";

		// Act
		bool result = CheckCountryCode( code );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void CheckProvinceCode_should_be_false()
	{
		// Act (with branch coverage)
		bool result = CheckProvinceCode( null );
		bool result1 = CheckProvinceCode( "ABCDEFGHIJK" );
		bool result2 = CheckProvinceCode( "ABC" );

		// Assert
		_ = result.Should().BeFalse();
		_ = result1.Should().BeFalse();
		_ = result2.Should().BeFalse();
	}

	[Fact]
	public void CheckProvinceCode_should_be_true()
	{
		// Arrange
		string code = "CA";

		// Act
		bool result = CheckProvinceCode( code );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void DefaultCountry_should_be_USA()
	{
		// Arrange
		string code = "USA";

		// Act
		string result = DefaultCountry;

		// Assert
		_ = result.Should().Be( code );
	}

	[Fact]
	public void GetPostcode_should_not_be_null()
	{
		// Arrange
		string code = "10001-4514";

		// Act
		Postcode? result = GetPostcode( code );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void GetPostcode_should_be_null()
	{
		// Arrange
		string code = string.Empty;

		// Act
		Postcode? result = GetPostcode( code );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void GetProvinceName_should_be_empty()
	{
		// Arrange
		string code = "ABCDEFGHIJK";

		// Act
		string result = GetProvinceName( code );

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void GetProvinceName_should_not_be_empty()
	{
		// Arrange
		string code = "NY";

		// Act
		string result = GetProvinceName( code );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void SetCountries_should_not_be_empty()
	{
		// Arrange (with branch coverage)
		List<ISOCountry> list = FakeData.GetISOCountries();
		UseAlpha2 = true;
		SetCountries( list );
		UseAlpha2 = false;

		// Act
		SetCountries( list );

		// Assert
		_ = Countries.Should().NotBeEmpty();
	}

	[Fact]
	public void UseAlpha2_should_be_false()
	{
		// Arrange
		UseAlpha2 = false;

		// Act
		bool result = UseAlpha2;

		// Assert
		_ = result.Should().BeFalse();
	}
}