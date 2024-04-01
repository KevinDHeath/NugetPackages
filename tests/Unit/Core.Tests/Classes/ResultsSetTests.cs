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
	public void Results_list_should_be_empty()
	{
		// Act (with code coverage)
		ResultsSet<User> result = new( 0 );
		_ = new ResultsSet<User>();

		// Assert
		_ = result.Results.Should().BeEmpty();
	}

	[Fact]
	public void Results_list_should_not_be_empty()
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
		_ = result.Results.Should().NotBeEmpty();
	}
}