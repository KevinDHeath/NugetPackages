namespace Core.Tests.Classes;

public class JsonHelperTests
{
	[Fact]
	public void DeserializeFile_should_be_null()
	{
		// Arrange
		string fileName = string.Empty;

		// Act
		User? result = JsonHelper.DeserializeFile<User>( fileName );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void DeserializeFile_should_be_User()
	{
		// Arrange
		string fileName = Path.Combine( Global.cDataFolder, FakeData.cUserFile );

		// Act
		User? result = JsonHelper.DeserializeFile<User>( fileName );

		// Assert
		_ = result.Should().BeAssignableTo<User>();
	}

	[Fact]
	public void DeserializeJson_should_be_null()
	{
		// Arrange
		string json = string.Empty;

		// Act
		User? result = JsonHelper.DeserializeJson<User>( ref json );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void DeserializeJson_should_be_User()
	{
		// Arrange
		string json = Global.GetFileContents( FakeData.cUserFile );

		// Act
		User? result = JsonHelper.DeserializeJson<User>( ref json );

		// Assert
		_ = result.Should().BeAssignableTo<User>();
	}

	[Fact]
	public void DeserializeList_should_have_count_gt_0()
	{
		// Arrange
		string? json = Global.GetFileContents( FakeData.cProvincesFile );

		// Act
		List<Province> result = JsonHelper.DeserializeList<Province>( ref json );

		// Assert
		_ = result.Should().HaveCountGreaterThan( 0 );
	}

	[Fact]
	public void DeserializeList_should_have_count_of_0()
	{
		// Arrange
		string? json = string.Empty;

		// Act
		List<Province> result = JsonHelper.DeserializeList<Province>( ref json );

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void ReadAppSettings_should_have_count_gt_0()
	{
		// Arrange
		string fileName = Global.cSettings;
		string? section = null;

		// Act
		Dictionary<string, string?> result = JsonHelper.ReadAppSettings( ref fileName, ref section, 3 );

		// Assert
		_ = result.Should().HaveCountGreaterThan( 0 );
	}

	[Fact]
	public void ReadAppSettings_should_have_count_of_0()
	{
		// Arrange
		string fileName = string.Empty;
		string? section = null;

		// Act
		Dictionary<string, string?> result = JsonHelper.ReadAppSettings( ref fileName, ref section, 3 );

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void ReadAppSettings_with_bad_max_depth_should_have_count_of_0()
	{
		// Arrange
		string fileName = Global.cSettings;
		string? section = @"connectionstrings";

		// Act
		Dictionary<string, string?> result = JsonHelper.ReadAppSettings( ref fileName, ref section );

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void ReadAppSettings_with_bad_section_should_have_count_of_0()
	{
		// Arrange
		string fileName = Global.cSettings;
		string? section = @"connectionstrings";

		// Act
		Dictionary<string, string?> result = JsonHelper.ReadAppSettings( ref fileName, ref section, 3 );

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void ReadJsonFromFile_should_not_be_null()
	{
		// Arrange
		string fileName = Global.cDataFolder + Global.cGlobalData;

		// Act
		string? result = JsonHelper.ReadJsonFromFile( fileName );

		// Assert
		_ = result.Should().NotBeNull();
	}

	[Fact]
	public void ReadJsonFromFile_should_return_null()
	{
		// Arrange
		string fileName = string.Empty;

		// Act
		string? result = JsonHelper.ReadJsonFromFile( fileName );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void Serialize_should_be_false()
	{
		// Arrange
		string outFile = Path.GetTempFileName();

		// Act
		var result = JsonHelper.Serialize<Global>( null, outFile );
		if( File.Exists( outFile ) ) { File.Delete( outFile ); }

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void Serialize_should_be_null()
	{
		// Act
		string? result = JsonHelper.Serialize<Global>( null );

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact]
	public void Serialize_should_be_true()
	{
		// Arrange
		Global obj = new();
		string outFile = Path.GetTempFileName();

		// Act
		var result = JsonHelper.Serialize( obj, outFile );
		if( File.Exists( outFile ) ) { File.Delete( outFile ); }

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void Serialize_should_not_be_null()
	{
		// Arrange
		Global obj = new();

		// Act
		string? result = JsonHelper.Serialize( obj );

		// Assert
		_ = result.Should().NotBeNull();
	}
}