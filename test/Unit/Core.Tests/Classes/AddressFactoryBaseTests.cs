namespace Core.Tests.Classes;

public class AddressFactoryBaseTests : AddressFactoryBase
{
	public AddressFactoryBaseTests()
	{
		UseAlpha2 = false;
		DefaultCountry = "USA";
		Postcodes = FakeData.GetPostcodes();
		PostcodeCount = Postcodes.Count;
		Provinces = FakeData.GetProvinces();
	}

	[Fact]
	public void CheckCountryCode_should_be_false()
	{
		// Arrange
		SetCountries( FakeData.GetISOCountries() );

		// Act
		bool result = CheckCountryCode( "US" );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void CheckCountryCode_should_be_true()
	{
		// Arrange
		SetCountries( FakeData.GetISOCountries() );

		// Act
		bool result = CheckCountryCode( "USA" );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void CheckProvinceCode_should_be_false()
	{
		// Arrange
		string code = string.Empty;

		// Act
		bool result = CheckProvinceCode( code );

		// Assert
		_ = result.Should().BeFalse();
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
		//UseAlpha2 = false;

		// Act
		string result = DefaultCountry;

		// Assert
		_ = result.Should().Be( "USA" );
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
	public void GetPostcode_should_return_null()
	{
		// Arrange
		string code = "";

		// Act
		Postcode? result = GetPostcode( code );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void GetProvinceName_length_should_be_0()
	{
		// Arrange
		string code = "ABCDEFGHIJK";

		// Act
		string result = GetProvinceName( code );

		// Assert
		_ = result.Length.Should().Be( 0 );
	}

	[Fact]
	public void GetProvinceName_length_should_be_gt_0()
	{
		// Arrange
		string code = "NY";

		// Act
		string result = GetProvinceName( code );

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void SetCountries_count_should_be_gt_0()
	{
		// Arrange
		UseAlpha2 = false;

		// Act
		SetCountries( FakeData.GetISOCountries() );

		// Assert
		_ = Countries.Count.Should().BeGreaterThan( 0 );
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