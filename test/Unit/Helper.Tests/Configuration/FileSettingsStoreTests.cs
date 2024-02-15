// Ignore Spelling: eq

namespace Helper.Tests.Configuration;

public class FileSettingsStoreTests
{
	private const string cBadConfig = "BadConfig.json";

	[Fact]
	public void FileFolderInfo_should_throw_ArgumentNullException()
	{
#nullable disable
		// Arrange                 
		DirectoryInfo di = null;

		// Act
		Action act = () => FileFolderInfo.Create( di );

		// Assert
		_ = act.Should().Throw<ArgumentNullException>();
#nullable enable
	}

	[Fact]
	public void FileSettingStore_should_not_be_initialized()
	{
		// Arrange
		string configFile = Path.Combine( Global.cTestFolder, cBadConfig );

		// Act
		ISettingsStore result = FileSettingsStore.Create( configFile );

		// Assert
		_ = result.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public void FileSettingStore_should_throw_ArgumentNullException()
	{
#nullable disable
		// Arrange                 
		string configFile = null;

		// Act
		Action act = () => FileSettingsStore.Create( configFile );

		// Assert
		_ = act.Should().Throw<ArgumentNullException>();
#nullable enable
	}

	[Fact]
	public async Task FileSettingStoreAsync_should_not_be_initialized()
	{
		// Arrange
		string configFile = Path.Combine( Global.cTestFolder, cBadConfig );
		FileInfo fi = new( configFile );

		// Act
		ISettingsStore result = await FileSettingsStore.CreateAsync( fi );

		// Assert
		_ = result.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public async Task FileSettingStoreAsync_should_throw_ArgumentNullException()
	{
#nullable disable
		// Arrange
		FileInfo fi = null;

		// Act
		Func<Task> act = async () => { await FileSettingsStore.CreateAsync( fi ); };

		// Assert
		_ = await act.Should().ThrowAsync<ArgumentNullException>();
#nullable enable
	}

	[Fact]
	public void IsInitialized_should_be_false()
	{
		// Arrange
		string configFile = Global.cInvalidFile;

		// Act
		ISettingsStore store = FileSettingsStore.Create( configFile );

		// Assert
		_ = store.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public async Task IsInitialized_Async_should_be_false()
	{
		// Arrange
		FileInfo fi = new( Global.cInvalidFile );

		// Act
		ISettingsStore result = await FileSettingsStore.CreateAsync( fi );

		// Assert
		_ = result.IsInitialized.Should().BeFalse();
	}

	[Fact]
	public void SortFileList_should_have_count_eq_0()
	{
		// Arrange
		DirectoryInfo di = new( Global.cTestFolder );
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( Global.cTestFolder ), false, ["*.exe"] );

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
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( Global.cTestFolder ), false, ["*.config"] );

		// Act
		string[] result = info.SortFileList();

		// Assert
		_ = result.Should().HaveCountGreaterThan( 0 );
	}

	[Fact]
	public void SortFolderList_should_have_count_eq_0()
	{
		// Arrange
		IFolderInfo info = FileFolderInfo.Create( new DirectoryInfo( Global.cInvalidPath ), true );

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