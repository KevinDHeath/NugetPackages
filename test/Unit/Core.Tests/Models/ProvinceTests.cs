namespace Core.Tests.Models;

public class ProvinceTests
{
	[Fact]
	public void Read_should_be_Province()
	{
		// Arrange
		var row = FakeData.GetProvinceRow();

		// Act
		Province result = Province.Read( row );

		// Assert
		_ = result.Should().BeAssignableTo<Province>();
	}
}