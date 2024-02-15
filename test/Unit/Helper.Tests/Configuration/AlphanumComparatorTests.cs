// Ignore Spelling: Alphanum eq

namespace Helper.Tests.Configuration;

public class AlphanumComparatorTests
{
	public AlphanumComparatorTests()
	{
		// For branch coverage
		AlphanumComparator comparer = new();
		_ = comparer.Compare( "", "" );   // Empty strings
		_ = comparer.Compare( 123, 456 ); // Numeric values
	}

	[Fact]
	public void X_should_be_eq_Y()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act
		int result = comparer.Compare( "1", "1" );

		// Assert
		result.Should().Be( 0 );
	}

	[Fact]
	public void X_should_be_gt_Y()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act
		int result = comparer.Compare( "2", "1" );

		// Assert
		result.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void X_should_be_lt_Y()
	{
		// Arrange
		AlphanumComparator comparer = new();

		// Act
		int result = comparer.Compare( "123", "456" );

		// Assert
		result.Should().BeLessThan( 0 );
	}
}