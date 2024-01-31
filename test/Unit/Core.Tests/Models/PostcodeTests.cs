namespace Core.Tests.Models;

public class PostcodeTests
{
	[Fact]
	public void Read_should_be_Postcode()
	{
		// Arrange
		var row = FakeData.GetPostcodeRow();

		// Act
		Postcode result = Postcode.Read( row );

		// Assert
		_ = result.Should().BeAssignableTo<Postcode>();
	}
}