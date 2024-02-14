namespace Configuration.Helper;

/// <summary>Class to contain the settings from a configuration file section.</summary>
public class SettingsSection : ISettingsSection
{
	#region Properties

	/// <inheritdoc />
	public IDictionary<string, string> Settings { get; }

	#endregion

	#region Constructor

	/// <summary>Constructor using a string comparer.</summary>
	/// <param name="comparer">Comparer to use for the key/value collection.</param>
	internal SettingsSection( StringComparer comparer )
	{
		Settings = new SortedDictionary<string, string>( comparer );
	}

	#endregion

	#region Internal Methods

	/// <summary>Adds or updates a setting in the key/value collection.</summary>
	/// <param name="key">Key of the setting.</param>
	/// <param name="value">Value of the setting.</param>
	/// <returns>True if the setting has been added or updated.</returns>
	internal bool AddSetting( string key, string value )
	{
		// Check the required parameters are supplied
		if( string.IsNullOrWhiteSpace( key ) ) { return false; }

		// Update or add the setting
		key = key.Trim();
		value = value.Trim();
		if( Settings.ContainsKey( key ) ) { Settings[key] = value; }
		else { Settings.Add( key, value ); }

		return true;
	}

	#endregion

	#region Public Methods

	/// <inheritdoc />
	public string GetSetting( string settingKey )
	{
		// Check the required parameter is supplied
		if( string.IsNullOrWhiteSpace( settingKey ) ) { return string.Empty; }
		settingKey = settingKey.Trim();

		// Return the setting value
		return Settings.ContainsKey( settingKey ) ? Settings[settingKey] : string.Empty;
	}

	#endregion
}