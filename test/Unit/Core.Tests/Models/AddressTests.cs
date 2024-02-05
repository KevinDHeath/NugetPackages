namespace Core.Tests.Models;

public class AddressTests
{
	[Fact]
	public void FullAddress_length_should_be_0()
	{
		// Arrange
		Address address = new();

		// Act
		string result = address.FullAddress;

		// Assert
		_ = result.Length.Should().Be( 0 );
	}

	[Fact]
	public void FullAddress_length_should_be_gt_0()
	{
		// Arrange
		Address address = FakeData.CreateAddress();

		// Act
		string result = address.FullAddress;

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
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