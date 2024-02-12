// Ignore Spelling: eq

namespace Helper.Tests.Configuration;

public class FileSettingsStoreTests
{
	[Fact]
	public void FileSettingStore_should_not_be_initialized()
	{
		// Arrange
		string configFile = @".\Testdata\BadConfig.json";

		// Act
		ISettingsStore result = FileSettingsStore.Create( configFile );

		// Assert
		_ = result.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public async Task FileSettingStoreAsync_should_not_be_initialized()
	{
		// Arrange
		string configFile = @".\Testdata\BadConfig.json";
		FileInfo fi = new( configFile );

		// Act
		ISettingsStore result = await FileSettingsStore.CreateAsync( fi );

		// Assert
		_ = result.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public void SortFileList_should_have_count_eq_0()
	{
		// Arrange
		DirectoryInfo di = new( @".\Testdata" );
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( @".\Testdata" ), false, ["*.exe"] );

		// Act
		string[] result = info.SortFileList();
		_ = info.SortFolderList();

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void SortFileList_should_have_count_gt_0()
	{
		// Arrange
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( @".\Testdata" ), false, ["*.config"] );

		// Act
		string[] result = info.SortFileList();

		// Assert
		_ = result.Should().HaveCountGreaterThan( 0 );
	}

	[Fact]
	public void SortFolderList_should_have_count_eq_0()
	{
		// Arrange
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( @".\DoesNotExist" ), true );

		// Act
		string[] result = info.SortFolderList();
		_ = info.SortFileList();

		// Assert
		_ = result.Should().HaveCount( 0 );
	}

	[Fact]
	public void SortFolderList_should_have_count_gt_0()
	{
		// Arrange
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( @".\" ), true );

		// Act
		string[] result = info.SortFolderList();

		// Assert
		_ = result.Should().HaveCountGreaterThan( 0 );
	}
}