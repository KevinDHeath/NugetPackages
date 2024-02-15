using System.Text;

namespace Helper.Tests.Configuration;

public class IOHelperTests
{
	private const string cConfigFile = Global.cConfigFileHelper + ConfigFileHelper.cJsonExtension;

	public IOHelperTests()
	{
		// For branch coverage
		_ = IOHelper.Combine( "", "" );                  // Empty paths
		_ = IOHelper.GetExtension( "" );                 // Empty path
		_ = IOHelper.GetFileName( Global.cInvalidFile ); // File does not exist
		_ = IOHelper.GetFileNameWithoutExtension( "" );  // Empty path
	}

	[Fact]
	public void CheckIfLocal_should_be_Cloud()
	{
		// Arrange
		string path = "filename.xyz";

		// Act (with branch coverage)
		IOHelper.PathType result = IOHelper.CheckIfLocal( path );
		_ = IOHelper.CheckIfLocal( "" );           // PathType.Unknown
		_ = IOHelper.CheckIfLocal( @".\" + path ); // PathType.Local
		_ = IOHelper.CheckIfLocal( @"\\" + path ); // PathType.Network

		// Assert
		result.Should().Be( IOHelper.PathType.Cloud );
	}

	[Fact]
	public void DoesDirectoryExist_should_be_true()
	{
		// Arrange
		string directoryName = Global.cTestFolder;

		// Act (with branch coverage)
		bool result = IOHelper.DoesDirectoryExist( directoryName, throwNotFound: true );
		_ = IOHelper.DoesDirectoryExist( "" ); // Empty directory name

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void DoesDirectoryExist_should_throw_DirectoryNotFoundException()
	{
		// Arrange
		string directoryName = Global.cInvalidPath;

		// Act
		Action act = () => IOHelper.DoesDirectoryExist( directoryName, throwNotFound: true );

		// Assert
		_ = act.Should().Throw<DirectoryNotFoundException>();
	}

	[Fact]
	public void DoesFileExist_should_throw_FileNotFoundException()
	{
		// Arrange
		string fileName = Global.cInvalidFile;

		// Act
		Action act = () => IOHelper.DoesFileExist( fileName, throwNotFound: true );

		// Assert
		_ = act.Should().Throw<FileNotFoundException>();
	}

	[Fact]
	public void GetDirectoryInfo_should_be_null()
	{
		// Act
		DirectoryInfo? result = IOHelper.GetDirectoryInfo( Global.GetLongPath() );

		// Assert
		result.Should().BeNull();
	}

	[Fact]
	public void GetDirectoryInfo_should_not_be_null()
	{
		// Arrange
		string directoryName = Global.cTestFolder;

		// Act (with branch coverage)
		DirectoryInfo? result = IOHelper.GetDirectoryInfo( directoryName );
		_ = IOHelper.GetDirectoryInfo( "" ); // Empty directory name

		// Assert
		result.Should().NotBeNull();
	}

	[Fact]
	public void GetDirectoryName_should_have_value()
	{
		// Arrange
		string directoryName = Global.cTestFolder;

		// Act (with branch coverage)
		string result = IOHelper.GetDirectoryName( directoryName );
		_ = IOHelper.GetDirectoryName( "" ); // Empty directory name

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void GetFileInfo_should_be_null()
	{
		// Arrange
		string fileName = @"\" + Global.cInvalidFile;

		// Act
		FileInfo? result = IOHelper.GetFileInfo( Global.GetLongPath( fileName ) );

		// Assert
		result.Should().BeNull();
	}

	[Fact]
	public void GetFileNames_should_not_be_empty()
	{
		// Arrange
		string path = Global.cTestFolder;
		string searchPattern = "*.config";

		// Act (with branch coverage)
		IList<string> result = IOHelper.GetFileNames( path, searchPattern );
		_ = IOHelper.GetFileNames( "", "" ); // Empty path and search pattern

		// Assert
		result.Should().NotBeEmpty();
	}
	[Fact]
	public void GetFileNameWithoutExtension_should_have_value()
	{
		// Arrange
		string fileName = Global.cInvalidFile;

		// Act
		string result = IOHelper.GetFileNameWithoutExtension( fileName );

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void ReadAllText_should_be_empty()
	{
		// Arrange
		string file = cConfigFile;

		// Act
		string result1 = IOHelper.ReadAllText( Global.GetLongPath( file ) ); ;
		string result2 = IOHelper.ReadAllText( Global.GetLongPath( file ), Encoding.UTF8 );

		// Assert
		result1.Should().BeEmpty();
		result2.Should().BeEmpty();
	}

	[Fact]
	public void ReadAllText_should_not_be_empty()
	{
		// Arrange
		System.Text.Encoding encoding = Encoding.UTF8;
		string path = Path.Combine( Global.cTestFolder, cConfigFile );

		// Act (with branch coverage)
		string result1 = IOHelper.ReadAllText( path );
		string result2 = IOHelper.ReadAllText( path, encoding );
		_ = IOHelper.ReadAllText( "" );           // Empty path
		_ = IOHelper.ReadAllText( "", encoding ); // Empty path with encoding

		// Assert
		result1.Should().NotBeEmpty();
		result2.Should().NotBeEmpty();
	}

	[Fact]
	public void RemoveLastDirSeparator_should_not_end_with_separator()
	{
		// Arrange
		string directoryName = Global.cTestFolder + @"\";

		// Act (with branch coverage)
		string result = IOHelper.RemoveLastDirSeparator( directoryName );
		_ = IOHelper.RemoveLastDirSeparator( Global.cTestFolder ); // No last separator
		_ = IOHelper.RemoveLastDirSeparator( "" );                 // Empty path

		// Assert
		result.Should().NotEndWith( @"\" );
	}
}