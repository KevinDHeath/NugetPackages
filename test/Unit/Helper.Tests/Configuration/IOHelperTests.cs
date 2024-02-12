namespace Helper.Tests.Configuration;

public class IOHelperTests
{
	[Fact]
	public void DoesDirectoryExist_should_throw_DirectoryNotFoundException()
	{
		// Arrange
		string directoryName = @".\TestdataX";

		// Act
		Action act = () => IOHelper.DoesDirectoryExist( directoryName, true );

		// Assert
		_ = act.Should().Throw<DirectoryNotFoundException>();
	}

	[Fact]
	public void GetDirectoryInfo_should_be_null()
	{
		// Arrange
		string directoryName = string.Empty;

		// Act
		DirectoryInfo? result = IOHelper.GetDirectoryInfo( directoryName );

		// Assert
		result.Should().BeNull();
	}

	[Fact]
	public void GetDirectoryInfo_should_not_be_null()
	{
		// Arrange
		string directoryName = @".\Testdata";

		// Act
		DirectoryInfo? result = IOHelper.GetDirectoryInfo( directoryName );
		_ = IOHelper.GetDirectoryInfo( string.Empty );

		// Assert
		result.Should().NotBeNull();
	}

	[Fact]
	public void GetDirectoryName_should_have_value()
	{
		// Arrange
		string directoryName = @".\Testdata";

		// Act
		string result = IOHelper.GetDirectoryName( directoryName );

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void RemoveLastDirSeparator_should_not_end_with_separator()
	{
		// Arrange
		string directoryName = @".\Testdata\";

		// Act
		string result = IOHelper.RemoveLastDirSeparator( directoryName );
		_ = IOHelper.RemoveLastDirSeparator( @".\Testdata" );

		// Assert
		result.Should().NotEndWith( @"\" );
	}

	[Fact]
	public void DoesFileExist_should_throw_FileNotFoundException()
	{
		// Arrange
		string fileName = @"filename.xyz";

		// Act
		Action act = () => IOHelper.DoesFileExist( fileName, true );

		// Assert
		_ = act.Should().Throw<FileNotFoundException>();
	}

	[Fact]
	public void CheckIfLocal_should_be_Cloud()
	{
		// Arrange
		string fileName = @"filename.xyz";

		// Act
		IOHelper.PathType result = IOHelper.CheckIfLocal( fileName );

		// Assert
		result.Should().Be( IOHelper.PathType.Cloud );
	}

	[Fact]
	public void GetFileNameWithoutExtension_should_have_value()
	{
		// Arrange
		string fileName = @"filename.xyz";

		// Act
		string result = IOHelper.GetFileNameWithoutExtension( fileName );

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void GetFileNames_should_not_be_empty()
	{
		// Arrange
		string path = @".\Testdata";
		string searchPattern = @"*.config";

		// Act
		IList<string> result = IOHelper.GetFileNames( path, searchPattern );

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void ReadAllText_should_not_be_empty()
	{
		// Arrange
		string path = @".\Testdata\ConfigFileHelper.json";

		// Act
		string result1 = IOHelper.ReadAllText( path );
		string result2 = IOHelper.ReadAllText( path, System.Text.Encoding.UTF8 );

		// Assert
		result1.Should().NotBeEmpty();
		result2.Should().NotBeEmpty();
	}
}