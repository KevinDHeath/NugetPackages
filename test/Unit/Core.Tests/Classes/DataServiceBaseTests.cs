namespace Core.Tests.Classes;

public class DataServiceBaseTests
{
	#region Constructor and variables

	private readonly bool _notOnline = false;
	private const string cSkipReason = "Not online";

	private readonly DataServiceBase _httpbin;
	private readonly DataServiceBase _local;

	public DataServiceBaseTests()
	{
		_httpbin = new( "https://httpbin.org", 20 );
		try { _ = _httpbin.GetResource( "status/200" ); }
		catch( Exception ) { _notOnline = true; } // Set to true to skip online tests
		_local = new( @"http:\\localhost" ); // (for branch coverage)
	}

	#endregion

	[SkippableFact]
	public void DeleteResource_should_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = @"status/200";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.DeleteResource<DataServiceBaseTests>( uri ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().BeNull();
	}

	[SkippableFact]
	public void DeleteResource_should_not_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = @"\anything\1";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.DeleteResource<DataServiceBaseTests>( uri ); }
		catch ( Exception ) { }

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void DeleteResource_should_throw_AggregateException()
	{
		// Arrange
		string uri = @"\anything\1";

		// Act
		Action act = () => _local.DeleteResource<DataServiceBaseTests>( uri );

		// Assert
		_ = act.Should().Throw<AggregateException>().WithInnerException<HttpRequestException>();
	}

	[SkippableFact]
	public void GetResource_should_not_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = @"\anything\1";

		// Act
		string? result = null;
		try { result = _httpbin.GetResource( uri ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void GetResource_should_throw_AggregateException()
	{
		// Arrange
		string uri = @"\anything\1";

		// Act
		Action act = () => _local.GetResource( uri );

		// Assert
		_ = act.Should().Throw<AggregateException>().WithInnerException<HttpRequestException>();
	}

	[SkippableFact]
	public void PostResource_should_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = "https://httpbin.org/status/200";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.PostResource( uri, this ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().BeNull();
	}

	[SkippableFact]
	public void PostResource_should_not_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = @"\anything\1";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.PostResource( uri, this ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void PostResource_should_throw_AggregateException()
	{
		// Arrange
		string uri = @"\anything\1";

		// Act
		Action act = () => _local.PostResource( uri, this );

		// Assert
		_ = act.Should().Throw<AggregateException>().WithInnerException<HttpRequestException>();
	}

	[SkippableFact]
	public void PutResource_should_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = "status/200";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.PutResource( uri, this ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().BeNull();
	}

	[SkippableFact]
	public void PutResource_should_not_be_null()
	{
		// Arrange
		Skip.If( _notOnline, cSkipReason );
		string uri = @"\anything\1";

		// Act
		DataServiceBaseTests? result = null;
		try { result = _httpbin.PutResource( uri, this ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void PutResource_should_throw_AggregateException()
	{
		// Arrange
		string uri = @"\anything\1";

		// Act
		Action act = () => _local.PutResource( uri, this );

		// Assert
		_ = act.Should().Throw<AggregateException>().WithInnerException<HttpRequestException>();
	}
}