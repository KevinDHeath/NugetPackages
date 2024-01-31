namespace Core.Tests.Models;

public class UserTests
{
	[Fact]
	public void Clone_should_be_Equal()
	{
		// Arrange
		User source = FakeData.CreateUser();

		// Act
		User target = (User)source.Clone();
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Equal_should_be_false()
	{
		// Arrange
		User source = FakeData.CreateUser();

		// Act
		bool result = source.Equals( null );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public async Task GetUsersAsync_list_should_be_gt_0()
	{
		// Act
		var result = await User.GetUsersAsync();

		// Assert
		_ = result.Count.Should().BeGreaterThanOrEqualTo( 1 );
	}

	[Fact]
	public void Update_should_be_true()
	{
		// Arrange
		User source = FakeData.CreateUser();
		User target = (User)source.Clone();
		source.Name = "mod";
		source.Email = "mod";
		source.BirthDate = new DateOnly( 2000, 1, 1 );
		source.Age = ModelBase.CalculateAge( source.BirthDate );
		source.Gender = Genders.Male;

		// Act
		target.Update( source );
		bool result = target.Equals( source );

		// Assert
		_ = result.Should().BeTrue();
	}
}