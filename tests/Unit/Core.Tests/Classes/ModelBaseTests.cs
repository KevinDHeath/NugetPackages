namespace Core.Tests.Classes;

public class ModelBaseTests : ModelBase
{
	#region Property and Constructor

	private int _age;
	public int Age
	{
		get => _age;
		set { _age = value; OnPropertyChanged(); }
	}

	public ModelBaseTests()
	{
		// For branch coverage
		OnPropertyChanged();
	}

	#endregion

	[Fact]
	public void CalculateAge_from_DateOnly_should_be_null()
	{
		// Arrange
		DateOnly? date = null;

		// Act
		int? result = CalculateAge( date );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void CalculateAge_from_DateTime_should_be_null()
	{
		// Arrange
		DateTime? date = null;

		// Act
		int? result = CalculateAge( date );

		// Assert
		_ = result.Should().BeNull();
	}

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
	public void OnPropertyChanged_should_be_raised()
	{
		// Arrange
		using var subject = this.Monitor();

		// Act
		Age = 20;

		// Assert
		// https://fluentassertions.com/eventmonitoring/
		_ = subject.Should().RaisePropertyChangeFor( x => x.Age );
	}

	[Theory]
	[InlineData( null, true )]
	[InlineData( "", true )]
	[InlineData( "A", false )]
	public void SetNull_Theory( string? value, bool result )
	{
		string? str = SetNullString( value );
		bool res = str is null;

		_ = res.Should().Be( result );
	}
}