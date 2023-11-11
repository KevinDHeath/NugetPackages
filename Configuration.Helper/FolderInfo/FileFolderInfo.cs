namespace Configuration.Helper.FolderInfo;

/// <summary>Folder Information factory implementation using a Windows file system.</summary>
public sealed class FileFolderInfo : FolderInfoBase
{
	#region Factory Pattern

	/// <inheritdoc />
	private FileFolderInfo()
	{ }

	/// <summary>Factory method to create an IFolderInfo object from a DirectoryInfo object.</summary>
	/// <param name="dirInfo">Root folder as a DirectoryInfo object.</param>
	/// <param name="includeFolders">Include sub-folder contents. The default is false.</param>
	/// <param name="filter">A string array that contains zero or more file name patterns.</param>
	/// <returns>A FileFolderInfo object.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the required parameter is null.</exception>
	public static IFolderInfo Create( DirectoryInfo dirInfo,
		bool includeFolders = false, params string[] filter )
	{
		// Check the required parameter
		const string cMethod = @"FileFolderInfo.Create";
		if( null == dirInfo )
		{
			throw new ArgumentNullException( nameof( dirInfo ), cMethod );
		}

		return dirInfo.Exists ? Initialize( dirInfo, string.Empty, includeFolders, filter ) : new FileFolderInfo();
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Recursively called method to initialize a FileFolderInfo object for a DirectoryInfo object.
	/// </summary>
	/// <param name="dirInfo">Directory information object.</param>
	/// <param name="root">The root folder name.</param>
	/// <param name="includeFolders">Include sub-folder contents.</param>
	/// <param name="filter">A string array that contains zero or more file name patterns.</param>
	/// <returns>A FileFolderInfo object.</returns>
	private static IFolderInfo Initialize( DirectoryInfo dirInfo, string root, bool includeFolders, params string[] filter )
	{
		var retValue = new FileFolderInfo { Location = FormatLocation( dirInfo, ref root ) };

		// Set a default filter if not supplied
		if( filter.Length == 0 )
		{
			filter = new[] { "*.*" };
		}

		// Populate the file list using the filter
		foreach( var ext in filter )
		{
			foreach( var fi in dirInfo.GetFiles( ext, SearchOption.TopDirectoryOnly ) )
			{
				if( !fi.Name.ToLower().Contains( @".vshost." ) ) // Ignore Visual Studio debugging files
				{
					retValue.FileList.Add( fi.Name, fi.FullName );
				}
			}
		}

		if( includeFolders )
		{
			// Process the sub-folders
			foreach( var di in dirInfo.GetDirectories() )
			{
				retValue.FolderList.Add( di.Name, Initialize( di, root, true ) );
			}
		}

		retValue.Exists = dirInfo.Exists;
		retValue.Source = IOHelper.CheckIfLocal( dirInfo.FullName );
		return retValue;
	}

	/// <summary>Formats the folder location.</summary>
	/// <param name="dirInfo">Directory information object.</param>
	/// <param name="root">The root folder name.</param>
	/// <returns>A string containing the formatted folder location.</returns>
	private static string FormatLocation( FileSystemInfo dirInfo, ref string root )
	{
		string retValue;
		if( root.Length > 0 )
		{
			retValue = dirInfo.FullName.Replace( root, string.Empty );

			// Remove leading separator
			if( retValue.StartsWith( _separator ) )
			{
				retValue = retValue[1..];
			}
		}
		else
		{
			retValue = dirInfo.FullName;
			root = retValue;
		}

		return retValue + _separator;
	}

	#endregion
}