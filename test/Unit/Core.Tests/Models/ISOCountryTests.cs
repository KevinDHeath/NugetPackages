namespace Core.Tests.Models;

public class ISOCountryTests
{
	[Fact]
	public void Read_should_be_ISOCountry()
	{
		// Arrange
		var row = FakeData.GetISOCountryRow();

		// Act
		ISOCountry result = ISOCountry.Read( row );

		// Assert
		_ = result.Should().BeAssignableTo<ISOCountry>();
	}
}