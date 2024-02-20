namespace Core.Tests.Models;

public class CompanyTests
{
	[Fact]
	public void Clone_should_be_equal()
	{
		// Arrange
		Company source = FakeData.CreateCompany();

		// Act
		Company target = (Company)source.Clone();
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Equal_should_be_false()
	{
		// Arrange
		Company source = FakeData.CreateCompany();
		Company target = FakeData.CreateCompany();
		target.Id = 2;

		// Act
		_ = source.Equals( target );

		// Act
		bool result = source.Equals( target );

		// Assert
		_ = result.Should().BeFalse();
		FakeData.BranchCoverageCompany( FakeData.Method.Equal, source, target );
	}

	[Fact]
	public void GetSerializerOptions_should_be_JsonSerializerOptions()
	{
		// Act
		JsonSerializerOptions result = Company.GetSerializerOptions();

		// Assert
		_ = result.Should().BeAssignableTo<JsonSerializerOptions>();
	}

	[Fact]
	public void Name_should_be_empty()
	{
		// Arrange
		Company company = new();

		// Act
		string result = company.Name;

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void Read_should_be_Company()
	{
		// Arrange
		DataRow row = FakeData.GetCompanyRow();
		row[nameof( Company.Private )] = DBNull.Value;

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
		FakeData.BranchCoverageCompany( FakeData.Method.Update, source );

	}

	[Fact]
	public void UpdateSQL_should_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetCompanyRow();
		Company obj = FakeData.CreateCompany( mod: true );
		Company mod = (Company)obj.Clone();

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void UpdateSQL_should_not_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetCompanyRow();
		Company obj = Company.Read( row, FakeData.cAddrPrefix );
		Company mod = FakeData.CreateCompany( mod: true );

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().NotBeEmpty();
		FakeData.BranchCoverageCompany( FakeData.Method.UpdateSQL, obj, mod, row );
	}

	[Fact]
	public void UpdateSQL_with_different_id_should_be_empty()
	{
		// Arrange
		DataRow row = FakeData.GetCompanyRow();
		Company obj = Company.Read( row, FakeData.cAddrPrefix );
		Company mod = FakeData.CreateCompany( mod: true );
		mod.Id = 10;

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void UpdateSQL_with_null_private_should_contain_NULL()
	{
		// Arrange
		DataRow row = FakeData.GetCompanyRow();
		Company obj = Company.Read( row, FakeData.cAddrPrefix );
		Company mod = FakeData.CreateCompany( mod: true );
		mod.Private = null;

		// Act
		string result = Company.UpdateSQL( row, obj, mod, FakeData.cAddrPrefix );

		// Assert
		_ = result.Should().Contain( "[Private]=NULL" );
	}
}