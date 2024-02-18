// https://apievangelist.com/2021/08/12/what-is-httpbin-and-what-can-you-do-with-the-api/

namespace Core.Tests.Classes;

public class DataServiceBaseTests
{
	private readonly bool _Online = true; // Set to false to skip online tests

	#region Constructor and variables

	private const string cSkipReason = "Online is false";

	private readonly DataServiceBase _httpbin;
	private readonly DataServiceBase _local;

	public DataServiceBaseTests()
	{
		_httpbin = new( "https://httpbin.org", 20 );
		_local = new( @"http:\\localhost" ); // (with branch coverage)

		if( _Online )
		{
			try { _ = _httpbin.GetResource( "status/200" ); }
			catch( Exception ) { _Online = false; }
		}
	}

	#endregion

	[SkippableFact]
	public void DeleteResource_should_be_null()
	{
		// Arrange
		Skip.If( !_Online, cSkipReason );
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
		Skip.If( !_Online, cSkipReason );
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
		Skip.If( !_Online, cSkipReason );
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
		Skip.If( !_Online, cSkipReason );
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
		Skip.If( !_Online, cSkipReason );
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

	[Fact]
	public void PostResource_with_null_should_return_null()
	{
		// Arrange
		string uri = "https://httpbin.org/status/200";

		// Act
		object? result = null;
		try { result = _httpbin.PostResource<Global>( uri, null ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().BeNull();
	}

	[SkippableFact]
	public void PutResource_should_be_null()
	{
		// Arrange
		Skip.If( !_Online, cSkipReason );
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
		Skip.If( !_Online, cSkipReason );
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

	[Fact]
	public void PutResource_with_null_should_return_null()
	{
		// Arrange
		string uri = "https://httpbin.org/status/200";

		// Act
		object? result = null;
		try { result = _httpbin.PutResource<Global>( uri, null ); }
		catch( Exception ) { }

		// Assert
		_ = result.Should().BeNull();
	}
}