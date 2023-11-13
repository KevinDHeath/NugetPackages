namespace Configuration.Helper;

/// <summary>Interface for a Settings Store implementation class.</summary>
public interface ISettingsStore
{
	#region Properties

	/// <summary>Gets the location of a configuration settings.</summary>
	string Location { get; }

	/// <summary>Gets all the settings sections that apply to the SettingsStore object.</summary>
	IDictionary<string, SettingsSection> Sections { get; }

	/// <summary>Indicates if the setting store has been successfully initialized.</summary>
	bool IsInitialized { get; }

	/// <summary>
	/// Gets the application settings section object that applies to the SettingsStore object.
	/// </summary>
	ISettingsSection AppSettings { get; }

	/// <summary>
	/// Gets the connection strings section object that applies to the SettingsStore object.
	/// </summary>
	ISettingsSection ConnectionStrings { get; }

	/// <summary>Gets the folder information source.</summary>
	IOHelper.PathType Source { get; }

	#endregion

	#region Methods

	/// <summary>Adds or updates a setting in the AppSettings key/value collection.</summary>
	/// <param name="settingKey">Key of the setting.</param>
	/// <param name="settingValue">Value of the setting.</param>
	/// <returns>True if the setting has been added or updated.</returns>
	bool AddSetting( string settingKey, string settingValue );

	/// <summary>Returns the specified SettingsSection object.</summary>
	/// <param name="sectionName">The path to the section to be returned.</param>
	/// <returns>The specified SettingsSection object. A new section is returned if it is not found.</returns>
	ISettingsSection GetSection( string sectionName );

	/// <summary>Returns the value of the specified AppSettings key.</summary>
	/// <param name="settingKey">Key of the setting to be returned.</param>
	/// <returns>An empty string is returned if the setting is not found.</returns>
	string GetSetting( string settingKey );

	#endregion
}