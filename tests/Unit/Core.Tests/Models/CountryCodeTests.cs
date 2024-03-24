namespace Core.Tests.Models;

public class CountryCodeTests
{
	[Fact]
	public void ToString_should_not_be_empty()
	{
		// Assign
		string code = "A";
		string name = "B";

		// Act
		CountryCode result = new( code, name );

		// Assert
		_ = result.ToString().Should().NotBeEmpty();
	}
}