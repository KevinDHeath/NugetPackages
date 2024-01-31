namespace Core.Tests.Models;

public class CountryCodeTests
{
	[Fact]
	public void ToString_length_should_be_gt_0()
	{
		// Act
		CountryCode result = new( "A", "B" );

		// Assert
		_ = result.ToString().Length.Should().BeGreaterThan( 0 );
	}
}