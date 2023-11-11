namespace Configuration.Helper.SettingsStore;

/// <summary>Settings Store factory implementation using a Windows disk file.</summary>
public sealed class FileSettingsStore : SettingsStoreBase
{
	#region Factory Pattern

	/// <inheritdoc />
	private FileSettingsStore()
	{ }

	/// <summary>
	/// Synchronous factory method to create an ISettingsStore object from a File System configuration file.
	/// </summary>
	/// <param name="configFile">Full path and file name of the configuration file.
	/// When no value is passed a new object will be created without a location.</param>
	/// <returns>FileSettingsStore object implementing the ISettingStore interface.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the parameter is null.</exception>
	public static ISettingsStore Create( string configFile = "" )
	{
		// Check the required parameter is supplied
		const string cMethod = @"FileSettingsStore.Create";
		if( null == configFile )
		{
			throw new ArgumentNullException( nameof( configFile ), cMethod );
		}

		// Check the parameter has a value
		configFile = configFile.Trim();
		var retValue = new FileSettingsStore();
		if( configFile.Length == 0 )
		{
			retValue.IsInitialized = true;
			return retValue;
		}

		// Create a Settings Store using a disk file name
		var fileInfo = IOHelper.GetFileInfo( configFile );
		if( fileInfo is not null ) { retValue.Initialize( fileInfo ); }

		return retValue;
	}

	/// <summary>
	/// Asynchronous factory method to create an ISettingsStore object from a File System configuration file.
	/// </summary>
	/// <param name="configFile">Configuration file containing the settings.</param>
	/// <returns>FileSettingsStore object implementing the ISettingStore interface.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the parameter is null.</exception>
	public static async Task<ISettingsStore> CreateAsync( FileInfo configFile )
	{
		// Check the required parameter is supplied
		const string cMethod = @"FileSettingsStore.CreateAsync";
		if( null == configFile )
		{
			throw new ArgumentNullException( nameof( configFile ), cMethod );
		}

		// Create a Settings Store using a disk file
		var retValue = new FileSettingsStore();
		await retValue.InitializeAsync( configFile );

		return retValue;
	}

	#endregion

	#region Private Methods

	private void Initialize( FileInfo configFile )
	{
		// Return an uninitialized Setting Store if the file does not exist
		if( null == configFile || !configFile.Exists )
		{
			return;
		}

		// Store the configuration file extension
		fileExtension = IOHelper.GetExtension( configFile.Name ).ToLower();

		Source = IOHelper.CheckIfLocal( configFile.FullName );
		try
		{
			// Initialize the Setting Store
			using( var stream = new MemoryStream() )
			{
				// Copy the stream and place in a stream reader
				configFile.OpenRead().CopyTo( stream );
				stream.Position = 0;
				using var reader = new StreamReader( stream );
				// Initialize the setting store from the stream reader
				var config = reader.ReadToEnd();
				LoadFromStream( ref config );
			}
			Location = configFile.FullName;
			IsInitialized = true;
		}
		catch( Exception )
		{
			// Do nothing - IsInitialized will be False
		}
	}

	/// <summary>Initializes a Setting Store asynchronously using a configuration disk file.</summary>
	/// <param name="configFile">Configuration file containing the settings.</param>
	private async Task InitializeAsync( FileInfo configFile )
	{
		// Return an uninitialized Setting Store if the file does not exist
		if( null == configFile || !configFile.Exists )
		{
			return;
		}

		// Store the configuration file extension
		fileExtension = IOHelper.GetExtension( configFile.Name ).ToLower();

		Source = IOHelper.CheckIfLocal( configFile.FullName );
		try
		{
			// Initialize the Setting Store
			using( var stream = new MemoryStream() )
			{
				// Copy the stream and place in a stream reader
				await configFile.OpenRead().CopyToAsync( stream );
				stream.Position = 0;
				using var reader = new StreamReader( stream );
				// Initialize the setting store from the stream reader
				var config = await reader.ReadToEndAsync();
				LoadFromStream( ref config );
			}
			Location = configFile.FullName;
			IsInitialized = true;
		}
		catch( Exception )
		{
			// Do nothing - IsInitialized will be False
		}
	}

	#endregion
}