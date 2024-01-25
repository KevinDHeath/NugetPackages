namespace Core.Tests.Converters;

public class GenericConverterTests
{
	[Fact]
	public void CharToBool_should_be_false()
	{
		// Arrange
		object obj = 'n';

		// Act
		bool? result = Generic.CharToBool( obj );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void CharToBool_should_be_true()
	{
		// Arrange
		object obj = 'Y';

		// Act
		bool? result = Generic.CharToBool( obj );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void DateTimeToDateOnly_should_have_value()
	{
		// Arrange
		object obj = DateTime.MinValue;

		// Act
		DateOnly? result = Generic.DateTimeToDateOnly( obj );

		// Assert
		_ = result.Should().HaveValue();
	}
}