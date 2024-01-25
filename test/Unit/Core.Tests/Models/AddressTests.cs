namespace Core.Tests.Models;

public class AddressTests
{
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
}