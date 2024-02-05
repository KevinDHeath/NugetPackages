namespace Core.Tests.Classes;

public class ResultsSetTests
{
	[Fact]
	public void Max_should_be_10()
	{
		// Arrange
		int? max = null;

		// Act
		ResultsSet<User> result = new( max );

		// Assert
		_ = result.Max.Should().Be( 10 );
	}

	[Fact]
	public void Results_should_have_values()
	{
		// Arrange
		List<User> list = FakeData.GetUserList();

		// Act
		ResultsSet<User> result = new( list.Count )
		{
			Next = string.Empty,
			Previous = string.Empty,
			Results = list,
			Total = list.Count,
		};

		// Assert
		_ = result.Results.Count.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void Results_should_not_have_values()
	{
		// Act
		ResultsSet<User> result = new( 0 );

		// Assert
		_ = result.Results.Count.Should().Be( 0 );
	}
}