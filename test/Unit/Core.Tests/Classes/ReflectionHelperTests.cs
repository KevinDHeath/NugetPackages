namespace Core.Tests.Classes;

public class ReflectionHelperTests
{
	[Fact]
	public void AddCurrentPath_should_be_fully_qualified()
	{
		// Arrange
		const string cFileName = "sample.txt";

		// Act
		string result = ReflectionHelper.AddCurrentPath( cFileName );

		// Assert
		_ = Path.IsPathFullyQualified( result ).Should().BeTrue();
	}

	[Fact]
	public void AddCurrentPath_should_end_with_filename()
	{
		// Arrange
		const string cFileName = "abc.txt";

		// Act
		string result = ReflectionHelper.AddCurrentPath( cFileName );

		// Assert
		_ = result.Should().EndWith( cFileName );
	}

	[Fact]
	public void DeepCopy_should_be_ComplexClass()
	{
		// Arrange
		ComplexClass source = new();

		// Act
		object result = ReflectionHelper.CreateDeepCopy( source )!;

		// Assert
		_ = result.Should().BeAssignableTo<ComplexClass>();
	}

	[Fact]
	public void DeepCopy_should_be_date_array()
	{
		// Arrange (with branch coverage)
		DateTime[] obj = [new( 2001, 1, 1 ), new( 2001, 1, 2 )];

		// Act
		object result = ReflectionHelper.CreateDeepCopy( obj )!;

		// Assert
		_ = result.Should().BeAssignableTo<Array>();
	}

	[Fact]
	public void DeepCopy_should_be_int_array()
	{
		// Arrange (with branch coverage)
		int[] obj = [1, 2, 3];

		// Act
		object result = ReflectionHelper.CreateDeepCopy( obj )!;

		// Assert
		_ = result.Should().BeAssignableTo<Array>();
	}

	[Fact]
	public void IsEqual_should_be_true()
	{
		// Arrange
		Person source = FakeData.CreatePerson();
		object target = ReflectionHelper.CreateDeepCopy( source )!;

		// Act
		bool result = ReflectionHelper.IsEqual( source, target );

		// Assert
		_ = result.Should().BeTrue();
	}
}