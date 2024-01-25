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
	public void DeepCopy_should_be_ReflectionClass()
	{
		// Arrange
		ComplexClass source = new();
		source.Headers.Add( @"foo", "bar" );
		source.Keys.Add( "bar" );

		// Act
		object result = ReflectionHelper.CreateDeepCopy( source )!;

		// Assert
		_ = result.Should().BeAssignableTo<ComplexClass>();
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