namespace Core.Tests.Classes;

public class DataFactoryBaseTests : DataFactoryBase
{
	[Fact]
	public void GetStartIndex_should_be_gt_0()
	{
		// Arrange
		int total = 10;
		int count = 2;

		// Act
		int result = GetStartIndex( total, count );

		// Assert
		_ = result.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetStartIndex_should_be_0()
	{
		// Arrange
		int total = 1;
		int count = 2;

		// Act
		int result = GetStartIndex( total, count );

		// Assert
		_ = result.Should().Be( 0 );
	}

	[Fact]
	public void DeserializeJson_should_be_null()
	{
		// Arrange
		string folder = string.Empty;
		string file = string.Empty;
		JsonSerializerOptions options = Converters.JsonConverterTests.ConfigureConverters();

		// Act
		DataFactoryBaseTests? result = DeserializeJson<DataFactoryBaseTests>( folder, file, options );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void DeserializeJson_should_not_be_null()
	{
		// Arrange
		string folder = Global.cDataFolder;
		string file = Global.cGlobalData;
		JsonSerializerOptions options = Converters.JsonConverterTests.ConfigureConverters();

		// Act
		DataFactoryBaseTests? result = DeserializeJson<DataFactoryBaseTests>( folder, file, options );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void GetFileResource_should_be_null()
	{
		// Arrange
		string path = string.Empty;
		string file = string.Empty;

		// Act
		string? result = GetFileResource( path, file );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void GetFileResource_length_should_be_gt_0()
	{
		// Arrange
		string path = Global.cDataFolder;
		string file = FakeData.cUserFile;

		// Act
		string? result = GetFileResource( path, file );
		result ??= string.Empty;

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void ReturnItems_should_be_lt_list_count()
	{
		// Arrange
		List<User> list = FakeData.GetUserList();
		int count = 2;

		// Act
		IList<User> result = ReturnItems( list, count );

		// Assert
		_ = result.Count.Should().Be( count );
	}

	[Fact]
	public void ReturnItems_should_eq_list_count()
	{
		// Arrange
		List<User> list = FakeData.GetUserList();
		int count = list.Count + 1;

		// Act
		IList<User> result = ReturnItems( list, count );

		// Assert
		_ = result.Count.Should().Be( list.Count );
	}

	[Fact]
	public void Serialize_should_be_true()
	{
		// Arrange
		string outFile = Path.GetTempFileName();

		// Act
		var result = SerializeJson( this, "", outFile, JsonHelper.DefaultSerializerOptions() );
		if( File.Exists( outFile ) ) { File.Delete( outFile ); }

		// Assert
		_ = result.Should().BeTrue();
	}
}