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
		User target = FakeData.CreateUser();

		// Act (with branch coverage)
		_ = source.Equals( target );
		_ = new User().Equals( null );
		_ = new User().Equals( new Address() );

		target.Name = null;
		_ = source.Equals( target );
		target.Name = source.Name;
		target.Email = null;
		_ = source.Equals( target );
		target.Email = source.Email;
		target.Age = null;
		_ = source.Equals( target );
		target.Age = source.Age;
		target.BirthDate = null;
		_ = source.Equals( target );
		target.BirthDate = source.BirthDate;
		target.Gender = Genders.Male;

		// Act
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void Equal_should_be_true()
	{
		// Arrange
		User source = FakeData.CreateUser();

		// Act
		bool result = source.Equals( source );

		// Assert
		_ = result.Should().BeTrue();
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
		User target = FakeData.CreateUser( mod: true );

		// Act (with branch coverage)
		new User().Update( null );
		new User().Update( new Address() );

		// Act
		target.Update( source );
		bool result = target.Equals( source );

		// Assert
		_ = result.Should().BeTrue();
	}
}