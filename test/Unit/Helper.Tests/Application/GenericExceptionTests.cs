namespace Helper.Tests.Application;

public class GenericExceptionTests
{
	private const string cMsg = "Generic exception";

	[Fact]
	public void Constructor_should_be_GenericException()
	{
		// Arrange
		string msg = cMsg;
		Exception ex = new();

		// Act (with branch coverage)
		GenericException result = new();
		_ = new GenericException( msg );     // Message only
		_ = new GenericException( msg, ex ); // Message and inner exception

		// Assert
		_ = result.Should().BeAssignableTo<GenericException>();
	}

	[Fact]
	public void FormatException_should_contain_GenericException()
	{
		// Arrange
		Exception ex = new( "Base exception" );
		GenericException ge = new( cMsg );
		AggregateException ae = new( [ge, ex] );

		// Act  (with branch coverage)
		string result = GenericException.FormatException( ae ); // Aggregate exception
		_ = GenericException.FormatException( null );           // Null exception
		_ = GenericException.FormatException( null, ex );       // Null message and exception
		_ = GenericException.FormatException( ex );             // Exception
		_ = GenericException.FormatException( ge );             // Generic exception

		// Assert
		_ = result.Should().Contain( cMsg );
	}
}