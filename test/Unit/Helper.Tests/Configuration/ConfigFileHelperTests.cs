using System.Security;

namespace Helper.Tests.Configuration;

public class ConfigFileHelperTests : ConfigFileHelper
{
	private readonly string _config = Path.Combine( Global.cTestFolder, Global.cConfigFileHelper );

	public ConfigFileHelperTests() : base( @"Helper.Tests.dll.config" )
	{
		// For branch coverage
		_ = GetConfiguration( "" ); // Configuration file empty
	}

	[Fact]
	public async Task AddSetting_should_be_true()
	{
		// Arrange
		FileInfo fi = new( _config + cJsonExtension );
		ISettingsStore store = await FileSettingsStore.CreateAsync( fi );

		// Act (with branch coverage)
		bool result = store.AddSetting( "key", "value" );
		_ = store.AddSetting( "key", "change" ); // Change value
		_ = store.AddSetting( "", "change" );    // Empty setting key

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void ConfigFile_should_not_be_empty()
	{
		// Act
		string result = ConfigFile;

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void ConnectionString_should_not_be_empty()
	{
		// Arrange
		string configFile = _config + cJsonExtension;
		ISettingsStore store = GetConfiguration( configFile );

		// Act
		string result = store.ConnectionStrings.GetSetting( "movies" );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void ConvertToSecureString_should_have_length()
	{
		// Arrange
		string value = @"abcdefg";
		string empty = string.Empty;

		// Act (with branch coverage)
		SecureString result = ConvertToSecureString( ref value );
		_ = ConvertToSecureString( ref empty ); // String value empty

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetArgument_should_be_empty()
	{
		// Arrange
		string key = "NotFound";

		// Act (with branch coverage)
		string result = GetArgument( key );
		_ = GetArgument( "" ); // Argument key empty

		// Assert
		_ = result.Should().BeEmpty();
	}

	[Fact]
	public void GetArgument_should_not_be_empty()
	{
		// Arrange
		string key = "key";
		Arguments.Add( key, "value" );

		// Act
		string result = GetArgument( key );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void GetSection_should_be_created()
	{
		// Arrange
		string configFile = _config + cExtension;
		ISettingsStore store = FileSettingsStore.Create( configFile );

		// Act
		ISettingsSection result = store.GetSection( "Custom" );

		// Assert
		_ = result.Should().BeAssignableTo<ISettingsSection>();
	}

	[Fact]
	public void GetSection_should_be_empty()
	{
		// Arrange
		ISettingsSection? section = Settings?.GetSection( "appSettings" );

		// Act
		string? result = section?.GetSetting( string.Empty );

		// Assert
		_ = result.Should().BeNullOrEmpty();
	}

	[Fact]
	public void GetSection_should_throw_ArgumentException()
	{
		// Arrange
		string configFile = _config + cExtension;
		ISettingsStore store = FileSettingsStore.Create( configFile );

		// Act
		Action act = () => store.GetSection( "" );

		// Assert
		_ = act.Should().Throw<ArgumentException>();
	}

	[Fact]
	public void GetSection_should_throw_ArgumentNullException()
	{
		// Arrange
		string configFile = _config + cExtension;
		ISettingsStore store = FileSettingsStore.Create( configFile );

		// Act
		Action act = () => store.GetSection( null );

		// Assert
		_ = act.Should().Throw<ArgumentNullException>();
	}

	[Fact]
	public void GetSecureSetting_should_have_length()
	{
		// Arrange
		string section = "Custom";
		string key = "Password";

		// Act (with branch coverage)
		SecureString result = GetSecureSetting( ConfigFile, section, key );
		_ = GetSecureSetting( ConfigFile, "section", "" ); // Empty key (section must be supplied)

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetSetting_should_not_be_empty()
	{
		// Arrange
		string key = "FavoriteMovie";

		// Act (with branch coverage)
		string? result1 = GetSetting( key );
		string? result2 = Settings?.GetSetting( key );
		_ = GetSetting( "" );        // Setting key empty
		_ = GetSetting( "setting" ); // Setting key not found

		// Assert
		_ = result1.Should().NotBeNullOrEmpty();
		_ = result2.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public void GetSetting_with_prefix_should_not_be_empty()
	{
		// Arrange
		string key = "Director";
		string prefix = "Favorite";

		// Act (with branch coverage)
		string? result = GetSetting( key, prefix );
		_ = GetSetting( key, "" );             // Empty prefix
		_ = GetSetting( "", "prefix" );        // Setting key empty
		_ = GetSetting( "setting", "prefix" ); // Setting key not found

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public void IsInitialized_should_be_false()
	{
		// Arrange
		string configFile = Path.Combine( Global.cTestFolder, "EmptyFile.txt" );

		// Act
		ISettingsStore store = FileSettingsStore.Create( configFile );

		// Assert
		_ = store.IsInitialized.Should().BeFalse();
	}
}