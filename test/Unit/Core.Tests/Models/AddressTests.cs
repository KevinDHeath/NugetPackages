namespace Core.Tests.Models;

public class AddressTests
{
	[Fact]
	public void Equals_should_be_false()
	{
		// Arrange
		Address source = FakeData.CreateAddress();
		Address target = FakeData.CreateAddress();

		// Act (with branch coverage)
		_ = source.Equals( target );
		_ = new Address().Equals( null );
		_ = new Address().Equals( new User() );

		target.Street = null;
		_ = source.Equals( target );
		target.Street = source.Street;
		target.City = null;
		_ = source.Equals( target );
		target.City = source.City;
		target.Province = null;
		_ = source.Equals( target );
		target.Province = source.Province;
		target.Postcode = null;
		_ = source.Equals( target );
		target.Postcode = source.Postcode;
		target.Country = null;

		// Act
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void FullAddress_should_be_empty()
	{
		// Arrange
		Address address = new();

		// Act
		string result = address.FullAddress;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void FullAddress_should_not_be_empty()
	{
		// Arrange
		Address address = FakeData.CreateAddress();

		// Act
		string result = address.FullAddress;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void UpdateAddress_should_be_false()
	{
		// Arrange
		Address cur = FakeData.CreateAddress();
		Address org = FakeData.CreateAddress( mod: true );
		Address mod = org;

		// Act
		bool result = Address.UpdateAddress( org, mod, cur, new List<string>() ) ;

		// Assert
		_ = result.Should().BeFalse();
	}
}