using System.Security;

namespace Helper.Tests.Configuration;

public class ConfigFileHelperTests : ConfigFileHelper
{
	public ConfigFileHelperTests() : base( @"Helper.Tests.dll.config" )
	{
		// For code coverage
		_ = Settings?.GetSection( "section" );

		string empty = string.Empty;
		_ = GetArgument( empty );      // Argument key empty
		_ = GetConfiguration( empty ); // Configuration file empty
		_ = GetSetting( empty );       // Setting key empty
		_ = GetSetting( empty, "prefix" );      // Setting key empty
		_ = ConvertToSecureString( ref empty ); // String value empty
		_ = GetSetting( "setting" );            // Setting key not found
		_ = GetSetting( "setting", "prefix" );  // Setting key not found
	}

	[Fact]
	public async Task AddSetting_should_be_true()
	{
		// Arrange
		FileInfo fi = new( @"Testdata\ConfigFileHelper" + cJsonExtension );
		ISettingsStore store = await FileSettingsStore.CreateAsync( fi );

		// Act
		bool result = store.AddSetting( "key", "value" );

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
		string configFile = @"Testdata\ConfigFileHelper" + cJsonExtension;
		ISettingsStore store = GetConfiguration( configFile );

		// Act
		string result = store.ConnectionStrings.GetSetting( "movies" );

		// Assert
		_ = result.Should().NotBeEmpty();
	}

	[Fact]
	public void ConvertToSecureString_should_have_value()
	{
		// Arrange
		string value = @"abcdefg";

		// Act
		SecureString result = ConvertToSecureString( ref value );

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetArgument_should_be_empty()
	{
		// Arrange
		string key = "NotFound";

		// Act
		string result = GetArgument( key );

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
	public void GetSecureSetting_should_have_value()
	{
		// Arrange
		string section = "Custom";
		string key = "Password";

		// Act
		SecureString result = GetSecureSetting( ConfigFile, section, key );

		// Assert
		_ = result.Length.Should().BeGreaterThan( 0 );
	}

	[Fact]
	public void GetSetting_should_not_be_empty()
	{
		// Arrange
		string key = "FavoriteMovie";

		// Act
		string? result = GetSetting( key );
		string? result1 = Settings?.GetSetting( key );

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
		_ = result1.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public void GetSetting_with_prefix_should_not_be_empty()
	{
		// Arrange
		string key = "Director";
		string prefix = "Favorite";

		// Act
		string? result = GetSetting( key, prefix );

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
	}
}