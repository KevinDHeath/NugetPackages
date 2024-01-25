namespace Core.Tests.Models;

public class PersonTests
{
	[Fact]
	public void Clone_should_be_Equal()
	{
		// Arrange
		Person source = FakeData.CreatePerson();

		// Act
		Person target = (Person)source.Clone();
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Equals_should_be_false()
	{
		// Arrange
		Person source = FakeData.CreatePerson();

		// Act
		bool result = source.Equals( null );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void FullName_should_be_gt_0()
	{
		// Arrange
		Person person = FakeData.CreatePerson();

		// Act
		string result = person.FullName;

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetSerializerOptions_should_be_true()
	{
		// Act
		var result = Person.GetSerializerOptions();

		// Assert
		_ = result.Should().BeAssignableTo<System.Text.Json.JsonSerializerOptions>();
	}

	[Fact]
	public void Read_should_be_Person()
	{
		// Arrange
		var row = FakeData.GetPersonRow();

		// Act
		Person result = Person.Read( row, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeAssignableTo<Person>();
	}

	[Fact]
	public void Update_should_be_true()
	{
		// Arrange
		Person source = FakeData.CreatePerson();
		Person target = FakeData.CreatePerson( mod: true );

		// Act
		target.Update( source );
		bool result = target.Equals( source );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void UpdateSQL_length_should_be_gt_0()
	{
		// Arrange
		var row = FakeData.GetPersonRow();
		Person obj = Person.Read( row, FakeData.cAddrPrefix );
		Person mod = FakeData.CreatePerson( mod: true );

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix ); ;

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void UpdateSQL_length_should_be_0()
	{
		// Arrange
		var row = FakeData.GetPersonRow();
		Person obj = FakeData.CreatePerson( mod: true );
		Person mod = (Person)obj.Clone();

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix ); ;

		// Assert
		_ = result.Length.Should().BeLessThan( 1 );
	}
}