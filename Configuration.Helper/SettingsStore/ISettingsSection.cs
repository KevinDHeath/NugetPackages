namespace Configuration.Helper;

/// <summary>Interface for a Settings Store configuration section.</summary>
public interface ISettingsSection
{
	/// <summary>Gets the collection of key/value settings.</summary>
	IDictionary<string, string> Settings { get; }

	/// <summary>Returns the value of the specified setting key.</summary>
	/// <param name="settingKey">Key of the setting to be returned.</param>
	/// <returns>An empty string is returned if the setting is not found.</returns>
	string GetSetting( string settingKey );
}