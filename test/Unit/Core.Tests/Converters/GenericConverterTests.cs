namespace Core.Tests.Converters;

public class GenericConverterTests
{
	[Fact]
	public void CharToBool_should_be_false()
	{
		// Arrange
		object obj = 'x';

		// Act
		bool? result = Generic.CharToBool( obj );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void CharToBool_should_be_null()
	{
		// Arrange
		object obj = DateTime.Now;

		// Act
		bool? result = Generic.CharToBool( obj );

		// Assert
		_ = result.Should().BeNull();
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
	public void DateTimeToDateOnly_should_be_null()
	{
		// Arrange
		string obj = "ABC";

		// Act
		DateOnly? result = Generic.DateTimeToDateOnly( obj );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void DateTimeToDateOnly_should_not_be_null()
	{
		// Arrange
		object obj = DateTime.MinValue;

		// Act
		DateOnly? result = Generic.DateTimeToDateOnly( obj );

		// Assert
		_ = result.Should().NotBeNull();
	}
}