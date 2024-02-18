namespace Core.Tests.Models;

public class PersonTests
{
	[Fact]
	public void Clone_should_be_equal()
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
		Person target = FakeData.CreatePerson();

		// Act (with branch coverage)
		_ = source.Equals( target );
		_ = new Person().Equals( null );
		_ = new Person().Equals( new Address() );

		target.Id = 2;
		_ = source.Equals( target );
		target.Id = source.Id;
		target.FirstName = "mod";
		_ = source.Equals( target );
		target.FirstName = source.FirstName;
		target.MiddleName = "X";
		_ = source.Equals( target );
		target.MiddleName = source.MiddleName;
		target.LastName = "mod";
		_ = source.Equals( target );
		target.LastName = source.LastName;
		target.GovernmentNumber = null;
		_ = source.Equals( target );
		target.GovernmentNumber = source.GovernmentNumber;
		target.IdProvince = null;
		_ = source.Equals( target );
		target.IdProvince = source.IdProvince;
		target.IdNumber = null;
		_ = source.Equals( target );
		target.IdNumber = source.IdNumber;
		target.HomePhone = null;
		_ = source.Equals( target );
		target.HomePhone = source.HomePhone;
		target.BirthDate = null;
		_ = source.Equals( target );
		target.BirthDate = source.BirthDate;
		target.Address.Street = null;

		// Act
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void FullName_should_be_empty()
	{
		// Arrange
		Person person = new();

		// Act
		string result = person.FullName;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void FullName_should_not_be_empty()
	{
		// Arrange
		Person person = FakeData.CreatePerson();

		// Act
		string result = person.FullName;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void GetSerializerOptions_should_be_JsonSerializerOptions()
	{
		// Act
		JsonSerializerOptions result = Person.GetSerializerOptions();

		// Assert
		_ = result.Should().BeAssignableTo<JsonSerializerOptions>();
	}

	[Fact]
	public void Read_should_be_Person()
	{
		// Arrange
		DataRow row = FakeData.GetPersonRow();

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
		_ = target.FullName;

		// Act
		target.Update( source );
		bool result = target.Equals( source );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void UpdateSQL_should_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetPersonRow();
		Person obj = FakeData.CreatePerson( mod: true );
		Person mod = (Person)obj.Clone();

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void UpdateSQL_should_not_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetPersonRow();
		Person obj = Person.Read( row, FakeData.cAddrPrefix );
		Person mod = FakeData.CreatePerson( mod: true );

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void UpdateSQL_with_different_id_should_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetPersonRow();
		Person obj = FakeData.CreatePerson( mod: true );
		Person mod = (Person)obj.Clone();
		mod.Id = 10;

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void UpdateSQL_with_null_date_should_contain_NULL()
	{
		// Arrange
		DataRow row = FakeData.GetPersonRow();
		Person obj = Person.Read( row, FakeData.cAddrPrefix );
		Person mod = FakeData.CreatePerson( mod: true );
		mod.BirthDate = null;

		// Act
		string result = Person.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().Contain( "[BirthDate]=NULL" );
	}
}