namespace Core.Tests.Models;

public class CompanyTests
{
	[Fact]
	public void Clone_should_be_Equal()
	{
		// Arrange
		Company source = FakeData.CreateCompany();
		Company target = (Company)source.Clone();

		// Act
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Equal_should_be_false()
	{
		// Arrange
		Company source = FakeData.CreateCompany();

		// Act
		bool result = source.Equals( null );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void GetSerializerOptions_should_be_true()
	{
		// Act
		var result = Company.GetSerializerOptions();

		// Assert
		_ = result.Should().BeAssignableTo<JsonSerializerOptions>();
	}

	[Fact]
	public void Read_should_be_Company()
	{
		// Arrange
		var row = FakeData.GetCompanyRow();

		// Act
		Company result = Company.Read( row, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeAssignableTo<Company>();
	}

	[Fact]
	public void Update_should_be_true()
	{
		// Arrange
		Company source = FakeData.CreateCompany();
		Company target = FakeData.CreateCompany( mod: true );

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
		var row = FakeData.GetCompanyRow();
		Company obj = Company.Read( row, FakeData.cAddrPrefix );
		Company mod = FakeData.CreateCompany( mod: true );

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix ); ;

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void UpdateSQL_length_should_be_0()
	{
		// Arrange
		var row = FakeData.GetCompanyRow();
		Company obj = FakeData.CreateCompany( mod: true );
		Company mod = (Company)obj.Clone();

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix ); ;

		// Assert
		_ = result.Length.Should().BeLessThan( 1 );
	}
}