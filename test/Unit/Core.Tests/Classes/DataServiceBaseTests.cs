namespace Core.Tests.Classes;

public class DataServiceBaseTests
{
	private const string cBaseUri = "https://httpbin.org";
	private const string cReason = "Not online";
	private const bool cSkip = false;

	[SkippableFact]
	public void DeleteResource_should_not_be_null()
	{
		Skip.If( cSkip, cReason );

		// Arrange
		DataServiceBase service = new( cBaseUri );

		// Act
		DataServiceBaseTests? result = service.DeleteResource<DataServiceBaseTests>( @"\anything\1" );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[SkippableFact]
	public void GetResource_should_not_be_null()
	{
		Skip.If( cSkip, cReason );

		// Arrange
		DataServiceBase service = new( cBaseUri );

		// Act
		string? result = service.GetResource( @"\anything\1" );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[SkippableFact]
	public void PostResource_should_not_be_null()
	{
		Skip.If( cSkip, cReason );

		// Arrange
		DataServiceBase service = new( cBaseUri );

		// Act
		DataServiceBaseTests? result = service.PostResource( @"\anything", this );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[SkippableFact]
	public void PutResource_should_not_be_null()
	{
		Skip.If( cSkip, cReason );

		// Arrange
		DataServiceBase service = new( cBaseUri );

		// Act
		DataServiceBaseTests? result = service.PutResource( @"\anything", this );

		// Assert
		_ = result.Should().NotBeNull();
	}
}