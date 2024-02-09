namespace Helper.Tests.Application;

public class GenericExceptionTests
{
	[Fact]
	public void Constructor_should_be_GenericException()
	{
		// Arrange
		string message = "Generic exception";
		Exception exception = new();

		// Act
		GenericException result = new();
		_ = new GenericException( message );
		_ = new GenericException( message, exception );

		// Assert
		_ = result.Should().BeAssignableTo<GenericException>();
	}

	[Fact]
	public void FormatException_should_contain_GenericException()
	{
		// Arrange
		Exception exception = new( "Base exception" );
		GenericException generic = new( "Generic exception" );
		AggregateException aggregate = new( [generic, exception] );

		// Act  (with branch coverage)
		_ = GenericException.FormatException( null );
		_ = GenericException.FormatException( null, exception );
		_ = GenericException.FormatException( exception );
		_ = GenericException.FormatException( generic );
		string result = GenericException.FormatException( aggregate );

		// Assert
		_ = result.Should().Contain( "GenericException" );
	}
}