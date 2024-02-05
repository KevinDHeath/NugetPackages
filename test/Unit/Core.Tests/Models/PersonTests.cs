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
		Person target = FakeData.CreatePerson();

		// Act (plus branch coverage)
		_ = source.Equals( target );

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
		target.Address.Street = null;
		_ = source.Equals( target );
		target.Address.Street = source.Address.Street;
		target.Address.City = null;
		_ = source.Equals( target );
		target.Address.City = source.Address.City;
		target.Address.Province = null;
		_ = source.Equals( target );
		target.Address.Province = source.Address.Province;
		target.Address.Postcode = null;
		_ = source.Equals( target );
		target.Address.Postcode = source.Address.Postcode;
		target.Address.Country = null;
		_ = source.Equals( target );
		target.Address.Country = source.Address.Country;
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

		bool result = source.Equals( target );

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
		_ = result.Should().BeAssignableTo<JsonSerializerOptions>();
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
		_ = target.FullName;

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