// Ignore Spelling: Alphanum

namespace Helper.Tests.Configuration;

public class AlphanumComparatorTests
{
	[Fact]
	public void Compare_should_be_equal()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act (with branch coverage)
		int result = comparer.Compare( "1", "1" );
		_ = comparer.Compare( "", "" );   // Empty strings
		_ = comparer.Compare( 123, 456 ); // Not strings

		// Assert
		result.Should().Be( 0 );
	}

	[Fact]
	public void Compare_should_be_greater_than()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act
		int result = comparer.Compare( "2", "1" );

		// Assert
		result.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void Compare_should_be_less_than()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act
		int result = comparer.Compare( "123", "456" );

		// Assert
		result.Should().BeLessThan( 0 );
	}
}