using System.Text;

namespace Configuration.Helper;

/// <summary>Helper class for System.IO operations.</summary>
public static class IOHelper
{
	#region Directory Functions

	/// <summary>Gets the directory information for a directory name.</summary>
	/// <param name="directoryName">Name of the directory.</param>
	/// <returns><see langword="null"/> is returned if the directory name is not valid.</returns>
	public static DirectoryInfo? GetDirectoryInfo( string directoryName )
	{
		if( !string.IsNullOrWhiteSpace( directoryName ) && directoryName.Length > 0 )
		{
			try
			{
				return new DirectoryInfo( directoryName );
			}
			catch( Exception )
			{
				// Directory name is not valid;
			}
		}
		return null;
	}

	/// <summary>Checks whether a directory exists.</summary>
	/// <param name="directoryName">Name of the file to check.</param>
	/// <param name="throwNotFound">Throw an exception if the directory does not exist.</param>
	/// <returns><see langword="true"/> if the file exists, otherwise <see langword="false"/> is returned.</returns>
	/// <exception cref="DirectoryNotFoundException">Thrown if the directory is not found and throwNotFound is True.</exception>
	public static bool DoesDirectoryExist( string directoryName, bool throwNotFound = false )
	{
		// Get the file information and check existence
		var di = GetDirectoryInfo( directoryName );
		var retValue = null != di && di.Exists;

		if( retValue || !throwNotFound )
		{
			return retValue;
		}

		// Set the file name to use and throw exception
		directoryName = string.IsNullOrWhiteSpace( directoryName ) ? string.Empty : directoryName.Trim();
		directoryName = null != di ? di.FullName : directoryName;
		throw new DirectoryNotFoundException( directoryName );
	}

	/// <summary>Returns the directory name for the specified path string.</summary>
	/// <param name="path">The path of a file or directory.</param>
	/// <returns>Directory information for path, or <see langword="null"/> if path denotes a root directory.<br/>
	/// Returns an empty string if path does not contain directory information.</returns>
	public static string GetDirectoryName( string path )
	{
		string? wrk = null;
		if ( path is not null ) wrk = Path.GetDirectoryName( path );
		return string.IsNullOrWhiteSpace( wrk ) ? string.Empty : wrk;
	}

	#endregion

	#region File Functions

	/// <summary>Gets the file information for a filename.</summary>
	/// <param name="fileName">Name of the file.</param>
	/// <returns><see langword="null"/> is returned if the file name is not valid.</returns>
	public static FileInfo? GetFileInfo( string fileName )
	{
		if( string.IsNullOrWhiteSpace( fileName ) || fileName.Length <= 0 ) return null;

		try
		{
			return new FileInfo( fileName.Trim() );
		}
		catch( Exception )
		{
			// File name is not valid;
		}
		return null;
	}

	/// <summary>Checks whether a file exists.</summary>
	/// <param name="fileName">Name of the file to check.</param>
	/// <param name="throwNotFound">Throw an exception if the file does not exist.</param>
	/// <returns><see langword="true"/> if the file exists, otherwise <see langword="false"/> is returned.</returns>
	/// <exception cref="FileNotFoundException">Thrown if the file is not found and throwNotFound is True.</exception>
	public static bool DoesFileExist( string fileName, bool throwNotFound = false )
	{
		// Get the file information and check existence
		var fi = GetFileInfo( fileName );
		var retValue = null != fi && fi.Exists;

		if( retValue || !throwNotFound )
		{
			return retValue;
		}

		// Set the file name to use and throw exception
		fileName = string.IsNullOrWhiteSpace( fileName ) ? string.Empty : fileName.Trim();
		fileName = null != fi ? fi.FullName : fileName;
		throw new FileNotFoundException( fileName );
	}

	/// <summary>Returns the file name and extension of the specified path string.</summary>
	/// <param name="path">The path string from which to obtain the file name and extension.</param>
	/// <returns>The characters after the last directory character in path. If the last character of path
	/// is a directory or volume separator character, this method returns an empty string.</returns>
	public static string GetFileName( string path )
	{
		return string.IsNullOrWhiteSpace( path ) ? string.Empty : Path.GetFileName( path );
	}

	/// <summary>Returns the file name of the specified path string without the extension.</summary>
	/// <param name="path">The path of the file. </param>
	/// <returns>The string returned by GetFileName, minus the last period (.) and all characters following it.</returns>
	public static string GetFileNameWithoutExtension( string path )
	{
		return string.IsNullOrWhiteSpace( path ) ? string.Empty : Path.GetFileNameWithoutExtension( path );
	}

	/// <summary>Returns a file list from the directory matching the given search pattern.</summary>
	/// <param name="path">The path of the directory.</param>
	/// <param name="searchPattern">The search string to match against the names of files.<br />
	/// This parameter can contain a combination of valid literal path and wild-card (* and ?) characters,
	/// but doesn't support regular expressions. The default pattern is "*", which returns all files.</param>
	/// <returns>A collection of type string.</returns>
	public static IList<string> GetFileNames( string path, string searchPattern )
	{
		searchPattern = string.IsNullOrWhiteSpace( searchPattern ) ? @"*.*" : searchPattern.Trim();
		var dir = GetDirectoryInfo( path );

		var retValue = new List<string>();
		if( dir is null ) return retValue;
		foreach( var file in dir.GetFiles( searchPattern ) )
		{
			retValue.Add( file.FullName );
		}

		retValue.TrimExcess();
		return retValue;
	}

	/// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
	/// <param name="path">The file to open for reading.</param>
	/// <returns>A string containing all lines in the file.</returns>
	public static string ReadAllText( string path )
	{
		if( string.IsNullOrWhiteSpace( path ) )
		{
			return string.Empty;
		}
		try
		{
			return File.ReadAllText( path );
		}
		catch( Exception )
		{
			return string.Empty;
		}
	}

	/// <summary>
	/// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
	/// </summary>
	/// <param name="path">The file to open for reading.</param>
	/// <param name="encoding">The encoding applied to the contents of the file.</param>
	/// <returns>A string containing all lines in the file.</returns>
	public static string ReadAllText( string path, Encoding encoding )
	{
		if( string.IsNullOrWhiteSpace( path ) )
		{
			return string.Empty;
		}
		try
		{
			return File.ReadAllText( path, encoding );
		}
		catch( Exception )
		{
			return string.Empty;
		}
	}

	#endregion

	#region Path Functions

	/// <summary>Returns the absolute path for the specified path string.</summary>
	/// <param name="path">The file or directory for which to obtain absolute path information.</param>
	/// <returns>The fully qualified location of path, such as "C:\MyFile.txt".</returns>
	public static string GetFullPath( string path )
	{
		return string.IsNullOrWhiteSpace( path ) ? string.Empty : Path.GetFullPath( path );
	}

	/// <summary>Returns the extension of the specified path string.</summary>
	/// <param name="path">The path string from which to get the extension.</param>
	/// <returns>The extension of the specified path (including the period "."), or empty string.<br />
	/// If path does not have extension information, an empty string is returned.</returns>
	public static string GetExtension( string path )
	{
		return string.IsNullOrWhiteSpace( path ) ? string.Empty : Path.GetExtension( path );
	}

	/// <summary>Combines two strings into a path.</summary>
	/// <param name="path1">The first path to combine.</param>
	/// <param name="path2">The second path to combine.</param>
	/// <returns>The combined paths.
	/// If one of the specified paths is a zero-length string, this method returns the other path.
	/// If path2 contains an absolute path, this method returns path2.</returns>
	public static string Combine( string path1, string path2 )
	{
		if( string.IsNullOrWhiteSpace( path1 ) )
		{
			path1 = string.Empty;
		}

		if( string.IsNullOrWhiteSpace( path2 ) )
		{
			path2 = string.Empty;
		}

		return Path.Combine( path1, path2 );
	}

	/// <summary>Returns a directory path without any trailing directory separator characters.</summary>
	/// <param name="path">Path to check.</param>
	/// <returns>The path without any trailing separator character.</returns>
	public static string RemoveLastDirSeparator( string path )
	{
		if( string.IsNullOrWhiteSpace( path ) )
		{
			return path;
		}

		if( path.EndsWith( Path.DirectorySeparatorChar.ToString() ) ||
		    path.EndsWith( Path.AltDirectorySeparatorChar.ToString() ) )
		{
			return path[..^1];
		}

		return path;
	}

	/// <summary>Enumeration of folder or file path types.</summary>
	public enum PathType
	{
		/// <summary>Unknown path type.</summary>
		Unknown,
		/// <summary>Local disk folder or file.</summary>
		Local,
		/// <summary>Network share disk folder or file.</summary>
		Network,
		/// <summary>Cloud location of folder or file.</summary>
		Cloud
	}

	/// <summary>Checks for a local path.</summary>
	/// <param name="path">Path to check.</param>
	/// <returns>The path type.</returns>
	public static PathType CheckIfLocal( string path )
	{
		path = string.IsNullOrWhiteSpace( path ) ? string.Empty : path.Trim();
		if( path.Length == 0 ) return PathType.Unknown;

		// Check for relative local path
		if( path.StartsWith( '.' ) ) return PathType.Local;

		// Check for absolute local path
		if( path.Length > 1 & path.Substring( 1, 1 ) == ":" ) return PathType.Local;

		// Check for network path
		if( path.StartsWith( @"\\" ) ) return PathType.Network;

		// Assume cloud location
		return PathType.Cloud;
	}

	#endregion
}