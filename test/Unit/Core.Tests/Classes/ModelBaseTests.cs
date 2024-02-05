namespace Core.Tests.Classes;

public class ModelBaseTests : ModelBase
{
	[Fact]
	public void CalculateAge_should_be_gt_0()
	{
		// Arrange
		int year = DateTime.Now.Year - 30;
		int month = DateTime.Now.Month;
		DateOnly? date = new( year, month, 10 );

		// Act
		int? result = CalculateAge( date );

		// Assert
		_ = result.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void CalculateAge_DateOnly_should_return_null()
	{
		// Arrange
		DateOnly? date = null;

		// Act
		int? result = CalculateAge( date );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void CalculateAge_DateTime_should_return_null()
	{
		// Arrange
		DateTime? date = null;

		// Act
		int? result = CalculateAge( date );

		// Assert
		_ = result.Should().BeNull();
	}

	[Theory]
	[InlineData( null, true )]
	[InlineData( "", true )]
	[InlineData( "A", false )]
	public void SetNull( string? value, bool result )
	{
		string? str = SetNullString( value );
		bool res = str is null;

		_ = res.Should().Be( result );
	}
}