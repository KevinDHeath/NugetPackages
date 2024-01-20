using System.Reflection;
using System.Security;

namespace Configuration.Helper;

/// <summary>Helper class for Configuration file access.</summary>
public abstract class ConfigFileHelper
{
	#region Properties and Constants

	/// <summary>Gets or sets the configuration file name.</summary>
	protected string ConfigFile
	{
		get { return _configFile ?? string.Empty; }
		set { Initialize( ref value ); }
	}

	/// <summary>Gets the collection of program arguments.</summary>
	public IDictionary<string, string> Arguments { get; }

	/// <summary>Gets the current settings.</summary>
	public ISettingsStore? Settings { get; private set; }

	/// <summary>Configuration file extension (suffix) including the period.</summary>
	public const string cExtension = ".config";

	/// <summary>JSON configuration file extension (suffix) including the period.</summary>
	public const string cJsonExtension = ".json";

	#endregion

	#region Instance Variables

	/// <summary>Configuration file information.</summary>
	private string? _configFile;

	#endregion

	#region Constructor and Initialization

	/// <summary>Initializes a new instance of the class using a configuration file name.</summary>
	/// <param name="configFile">Configuration filename.</param>
	protected ConfigFileHelper( string configFile )
	{
		ConfigFile = configFile; // This triggers the initialization
		Arguments = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
	}

	/// <summary>Initializes the configuration file settings.</summary>
	/// <param name="configFile">Configuration file name.</param>
	protected virtual void Initialize( ref string configFile )
	{
		_configFile = IOHelper.GetFullPath( configFile );
		Settings = GetConfiguration( _configFile );
	}

	#endregion

	#region Protected Methods

	/// <summary>Returns the setting value in the configuration file for a given key.</summary>
	/// <param name="settingKey">Key of the setting.</param>
	/// <returns><see langword="null"/> is returned if the setting is not found.</returns>
	protected string? GetSetting( string settingKey )
	{
		// Check the required parameter is supplied
		if( string.IsNullOrWhiteSpace( settingKey ) )
		{
			return string.Empty;
		}

		// Return the setting value
		settingKey = settingKey.Trim();
		return Settings?.AppSettings.GetSetting( settingKey );
	}

	/// <summary>Returns the setting value in the configuration file for a given key and prefix.</summary>
	/// <param name="settingKey">Key of the setting.</param>
	/// <param name="prefix">Prefix for the setting.</param>
	/// <returns><see langword="null"/> is returned if the setting is not found.</returns>
	protected string? GetSetting( string settingKey, string prefix )
	{
		// Check the required parameter is supplied
		if( string.IsNullOrEmpty( settingKey ) )
		{
			return string.Empty;
		}

		// Return the setting value
		string? retValue = null;
		if( !string.IsNullOrWhiteSpace( prefix ) )
		{
			retValue = GetSetting( prefix.Trim() + settingKey );
		}

		return retValue ?? GetSetting( settingKey );
	}

	#endregion

	#region Public Methods

	/// <summary>Returns the argument value in the configuration file for a given key.</summary>
	/// <param name="argumentKey">Key of the argument.</param>
	/// <returns>An empty string is returned if the argument key is not found.</returns>
	public string GetArgument( string argumentKey )
	{
		// Check the required parameter is supplied
		if( string.IsNullOrWhiteSpace( argumentKey ) )
		{
			return string.Empty;
		}

		// Return the setting value
		argumentKey = argumentKey.Trim();
		return Arguments.TryGetValue( argumentKey, out string? value ) ? value : string.Empty;
	}

	#endregion

	#region Public Static Functions

	/// <summary>Gets a Setting Store object.</summary>
	/// <param name="configFile">Configuration file name.</param>
	/// <returns>ISettingsStore object.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the parameter is <see langword="null"/>.</exception>
	public static ISettingsStore GetConfiguration( string configFile )
	{
		// Check the required parameter is supplied
		ArgumentNullException.ThrowIfNull( configFile );

		var exists = IOHelper.DoesFileExist( configFile );
		if( !exists )
		{
			// No path so try using current location
			var path = IOHelper.Combine( Assembly.GetExecutingAssembly().Location, IOHelper.GetFileName( configFile ) );
			if( configFile != path )
			{
				configFile = IOHelper.GetFullPath( configFile );
			}
		}

		return FileSettingsStore.Create( configFile );
	}

	/// <summary>Converts a string to a secure string.</summary>
	/// <param name="strValue">Value to secure - this will be empty on return.</param>
	/// <returns>If the string value is <see langword="null"/>, empty, or whitespace, then the
	/// return value will have a length of zero.</returns>
	public static SecureString ConvertToSecureString( ref string strValue )
	{
		// Set the return value
		var retValue = new SecureString();

		// Check the required parameter is supplied
		if( string.IsNullOrWhiteSpace( strValue ) || strValue.Length == 0 )
		{
			retValue.MakeReadOnly();
			return retValue;
		}

		// Populate the secure string and clear the parameter value
		Array.ForEach( strValue.ToCharArray(), retValue.AppendChar );
		strValue = string.Empty;

		// Return the secure string as read-only
		retValue.MakeReadOnly();
		return retValue;
	}

	/// <summary>Gets a secure string value of a configuration file key.</summary>
	/// <param name="configFile">Full path and name of the configuration file.</param>
	/// <param name="sectionName">Name of the section name containing the key.</param>
	/// <param name="key">Key of the value to return.</param>
	/// <returns>If the key cannot be retrieved then the return value will have a length of zero.</returns>
	public static SecureString GetSecureSetting( string configFile, string sectionName, string key )
	{
		string? str = null;
		if( IOHelper.DoesFileExist( configFile ) )
		{
			var config = GetConfiguration( configFile );
			var section = config.GetSection( sectionName );
			str = null != section ? section.GetSetting( key ) : string.Empty;
		}

		return str is not null ? ConvertToSecureString( ref str ) : new SecureString();
	}

	#endregion
}