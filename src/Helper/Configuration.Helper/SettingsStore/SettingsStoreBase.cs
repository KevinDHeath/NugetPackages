using System.Xml.Linq;
using System.Text.Json;

namespace Configuration.Helper;

/// <summary>Base class for Settings Store implementations.</summary>
public abstract class SettingsStoreBase : ISettingsStore
{
	#region ISettingsStore Implementation

	#region Properties

	/// <inheritdoc />
	public string Location { get; protected set; }

	/// <inheritdoc />
	public IDictionary<string, SettingsSection> Sections { get; }

	/// <inheritdoc />
	public bool IsInitialized { get; protected set; }

	/// <inheritdoc />
	public ISettingsSection AppSettings => GetSection( cAppSettings );

	/// <inheritdoc />
	public ISettingsSection ConnectionStrings => GetSection( cConnections );

	/// <inheritdoc />
	public IOHelper.PathType Source { get; protected set; }

	#endregion

	#region Methods

	/// <inheritdoc />
	public bool AddSetting( string settingKey, string settingValue )
	{
		SettingsSection? section = Sections.TryGetValue( cAppSettings, out SettingsSection? value ) ? value : null;

		// Add the setting value to the AppSettings section
		return section != null && section.AddSetting( settingKey, settingValue );
	}

	/// <inheritdoc />
	/// <exception cref="ArgumentNullException">Thrown if the parameter is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException">Thrown if the parameter is empty.</exception>
	public ISettingsSection GetSection( string sectionName )
	{
		// Check the required parameter is supplied
		const string cMethod = @"SettingsStoreBase.GetSection";
		if( null == sectionName ) { throw new ArgumentNullException( nameof( sectionName ), cMethod ); }
		sectionName = sectionName.Trim();
		if( sectionName.Length == 0 ) { throw new ArgumentException( cMethod, nameof( sectionName ) ); }

		// Return the section if it exists
		if( Sections.TryGetValue( sectionName, out SettingsSection? value ) ) { return value; }

		// Return a new section if not found
		var retValue = new SettingsSection( _comparer );
		Sections.Add( sectionName, retValue );
		return retValue;
	}

	/// <inheritdoc />
	public string GetSetting( string settingKey )
	{
		return AppSettings.GetSetting( settingKey );
	}

	#endregion

	#endregion

	/// <summary>Configuration settings file extension.</summary>
	protected string fileExtension = string.Empty;

	#region Constants and Protected Constructor

	private const string cAppSettings = @"appSettings"; // Application settings section name
	private const string cAppSettingKey = @"key"; // Application setting key name
	private const string cConnections = @"connectionStrings"; // Connection strings section name
	private const string cConnectionKey = @"name"; // Connection string key name
	private readonly StringComparer _comparer = StringComparer.CurrentCultureIgnoreCase;

	/// <summary>Default constructor.</summary>
	protected SettingsStoreBase()
	{
		Location = string.Empty;
		Sections = new Dictionary<string, SettingsSection>( _comparer )
		{
			// Add the standard sections
			{cAppSettings, new SettingsSection( _comparer )},
			{cConnections, new SettingsSection( _comparer )}
		};
	}

	#endregion

	#region Private Methods

	private void ProcessSetting( JsonElement element, string sectionName )
	{
		if( element.ValueKind != JsonValueKind.Object ) return;

		foreach( var item in element.EnumerateObject() )
		{
			string? val = item.Value.GetString();
			AddSetting( ref sectionName, item.Name, val ?? string.Empty );
		}
	}

	private void ProcessSetting( XElement elem )
	{
		// The element must have a parent section
		if( null == elem || null == elem.Parent ) { return; }
		var sectionName = elem.Parent.Name.LocalName;

		// Check for application setting
		var settingKey = elem.Attribute( cAppSettingKey ) ?? elem.Attribute( cConnectionKey );

		if( null == settingKey ) { return; } // Setting type not supported

		// Get the setting value
		XAttribute? settingVal = null;
		switch( settingKey.Name.LocalName )
		{
			case cAppSettingKey:
				settingVal = elem.Attribute( @"value" );
				break;
			case cConnectionKey:
				settingVal = elem.Attribute( @"connectionString" );
				break;
		}

		if( null != settingVal )
		{
			// Add the value to the section
			AddSetting( ref sectionName, settingKey.Value, settingVal.Value );
		}
	}

	private void AddSetting( ref string sectionName, string settingKey, string settingVal )
	{
		// Try getting existing section
		var section = Sections.TryGetValue( sectionName, out SettingsSection? value ) ? value : null;

		if( null == section )
		{
			// Create a new section
			section = new SettingsSection( _comparer );
			Sections.Add( sectionName, section );
		}

		// Add the setting
		section.AddSetting( settingKey, settingVal );
	}

	#endregion

	#region Protected Methods

	/// <summary>Initializes the Setting Store object.</summary>
	/// <param name="config">String containing the configuration file contents.</param>
	/// <exception cref="ArgumentNullException">Thrown if the parameter is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException">Thrown if the parameter is empty.</exception>
	/// <exception cref="JsonException">The JSON text to parse does not represent a valid single JSON value.</exception>
	protected void LoadFromStream( ref string config )
	{
		// Check the required parameter is supplied
		const string cMethod = @"SettingsStoreBase.LoadFromStream";
		if( null == config ) { throw new ArgumentNullException( nameof( config ), cMethod ); }

		// Check the required parameter has a value
		config = config.Trim();
		if( config.Length == 0 ) { throw new ArgumentException( cMethod, nameof( config ) ); }

		if( ConfigFileHelper.cJsonExtension.Equals( fileExtension, StringComparison.CurrentCultureIgnoreCase ) )
		{
			// Convert the string to JSON
			using var doc = JsonDocument.Parse( config, new JsonDocumentOptions
				{ AllowTrailingCommas = true, MaxDepth = 2 } );
			{
				var root = doc.RootElement;
				if( root.ValueKind == JsonValueKind.Object )
				{
					// find name of section
					foreach( var element in root.EnumerateObject() )
					{
						string section = element.Name;
						ProcessSetting( element.Value, section );
					}
				}
			}
		}
		else
		{
			// Convert the string to XML
			var xml = XElement.Parse( config );

			// Process each configuration settings
			foreach( var elem in xml.Descendants( @"add" ) )
			{
				ProcessSetting( elem );
			}
		}
	}

	#endregion
}