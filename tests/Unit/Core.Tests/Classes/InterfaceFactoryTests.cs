namespace Core.Tests.Classes;

public class InterfaceFactoryTests
{
	[Fact]
	public void CanConvert_should_be_false()
	{
		// Arrange
		InterfaceFactory obj = new( typeof( User ), typeof( IUser ) );

		// Act
		bool result = obj.CanConvert( typeof( ICompany ) );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void CreateConverter_should_be_JsonConverter()
	{
		// Arrange
		InterfaceFactory obj = new( typeof( User ), typeof( IUser ) );

		// Act
		JsonConverter result = obj.CreateConverter( typeof( User ), JsonHelper.DefaultSerializerOptions() );

		// Assert
		_ = result.Should().BeAssignableTo<JsonConverter>();
	}
}