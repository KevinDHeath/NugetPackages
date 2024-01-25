namespace Core.Tests.Classes;

public class ResultsSetTests
{
	[Fact]
	public void Results_should_have_values()
	{
		// Arrange
		string? json = FakeData.GetUserListJson();
		List<User> list = JsonHelper.DeserializeList<User>( ref json );

		// Act
		ResultsSet<User> result = new( 5 )
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