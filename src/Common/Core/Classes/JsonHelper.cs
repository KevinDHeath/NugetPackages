using System.Security;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Common.Core.Classes;

/// <summary>Class to provide Json file loading and saving.</summary>
public static class JsonHelper
{
	/// <summary>Reads a Json file and populates an object.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="fileName">Json file name.</param>
	/// <param name="options">Optional Json serializer options.</param>
	/// <returns><see langword="null"/> is returned if the object could not be populated.</returns>
	/// <exception cref="ArgumentException">Thrown when one of the arguments provided to a method is not valid.</exception>
	/// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
	/// <exception cref="NotSupportedException">Thrown when an invoked method is not supported, or when there
	/// is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
	/// <exception cref="OutOfMemoryException">Thrown when there is not enough memory to continue.</exception>
	/// <exception cref="SecurityException">Thrown when a security error is detected.</exception>
	/// <exception cref="UnauthorizedAccessException">Thrown when the operating system denies access because of an I/O error or a specific type of security error.</exception>
	public static T? DeserializeFile<T>( string fileName, JsonSerializerOptions? options = null ) where T : class
	{
		T? rtn = null;
		string? json = ReadJsonFromFile( fileName );
		options ??= DefaultSerializerOptions();
		return json is null ? rtn : DeserializeJson<T>( ref json, options );
	}

	/// <summary>Reads a Json string and populates an object.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="json">Json string.</param>
	/// <param name="options">Optional Json serializer options.</param>
	/// <returns><see langword="null"/> is returned if the object could not be populated.</returns>
	public static T? DeserializeJson<T>( ref string json, JsonSerializerOptions? options = null ) where T : class
	{
		T? rtn = null;
		try
		{
			options ??= DefaultSerializerOptions();
			rtn = JsonSerializer.Deserialize<T>( json, options );
		}
		catch( Exception ) { }

		return rtn;
	}

	/// <summary>Populates a list of objects from a Json string.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="json">Json string.</param>
	/// <param name="options">Optional Json serializer options</param>
	/// <returns>An empty list is returned if the string could not be converted.</returns>
	public static List<T> DeserializeList<T>( ref string? json, JsonSerializerOptions? options = null )
	{
		List<T> rtn = [];
		if( json is not null )
		{
			options ??= DefaultSerializerOptions();
			try
			{
				List<T>? obj = JsonSerializer.Deserialize<List<T>>( json, options );
				if( obj is not null ) { rtn = obj; }
			}
			catch( Exception ) { }
		}

		return rtn;
	}

	/// <summary>Reads the Json from a file.</summary>
	/// <param name="fileName">Json file name.</param>
	/// <returns><see langword="null"/> is returned if the file could not be accessed.</returns>
	/// <exception cref="ArgumentException">Thrown when one of the arguments provided to a method is not valid.</exception>
	/// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
	/// <exception cref="NotSupportedException">Thrown when an invoked method is not supported, or when there
	/// is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
	/// <exception cref="OutOfMemoryException">Thrown when there is not enough memory to continue.</exception>
	/// <exception cref="SecurityException">Thrown when a security error is detected.</exception>
	/// <exception cref="UnauthorizedAccessException">Thrown when the operating system denies access because of an I/O error or a specific type of security error.</exception>
	public static string? ReadJsonFromFile( string fileName )
	{
		if( string.IsNullOrWhiteSpace( fileName ) ) { return null; }
		string? json = null;
		FileInfo fi = new( fileName );
		if( fi.Exists )
		{
			using StreamReader sr = new( fi.FullName );
			json = sr.ReadToEnd();
		}

		return json;
	}

	/// <summary>Returns a collection of settings from a Json application settings file.</summary>
	/// <param name="fileName">Json application settings file name.</param>
	/// <param name="section">Application settings section to return <i>(case-sensitive)</i>.</param>
	/// <param name="maxDepth">Maximum depth allowed when parsing JSON data.</param>
	/// <returns>An empty collection is returned if the settings section could not be found.</returns>
	/// <remarks>If no section is provided it is assumed that the settings are in the root.
	/// <br/>Otherwise it is assumed that the section name is case-sensitive and only 3 levels deep.</remarks>
	public static Dictionary<string, string?> ReadAppSettings( ref string fileName, ref string? section, int maxDepth = 2 )
	{
		Dictionary<string, string?> rtn = [];
		try
		{
			if( string.IsNullOrEmpty( Path.GetDirectoryName( fileName ) ) )
			{
				fileName = ReflectionHelper.AddCurrentPath( fileName );
			}
			string? json = ReadJsonFromFile( fileName );
			if( string.IsNullOrWhiteSpace( json ) ) { return rtn; }
			JsonDocumentOptions options = new()
			{
				AllowTrailingCommas = true,
				MaxDepth = maxDepth,
				CommentHandling = JsonCommentHandling.Skip
			};
			using JsonDocument document = JsonDocument.Parse( json, options );
			JsonElement coll = document.RootElement;
			if( !string.IsNullOrEmpty( section ) ) { coll = coll.GetProperty( section ); }
			foreach( JsonProperty prop in coll.EnumerateObject() )
			{
				if( prop.Value.ValueKind is not JsonValueKind.Object )
				{
					rtn.Add( prop.Name, prop.Value.ToString() );
				}
			}
		}
		catch( Exception ) { }

		return rtn;
	}

	/// <summary>Writes a Json file of the provided object type.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="obj">Object to save.</param>
	/// <param name="fileName">Json file name.</param>
	/// <param name="options">Optional Json serializer options.</param>
	/// <returns><see langword="true"/> if the object was saved.</returns>
	/// <exception cref="ArgumentException">Thrown when one of the arguments provided to a method is not valid.</exception>
	/// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
	/// <exception cref="NotSupportedException">Thrown when an invoked method is not supported, or when there
	/// is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
	/// <exception cref="SecurityException">Thrown when a security error is detected.</exception>
	/// <exception cref="UnauthorizedAccessException">Thrown when the operating system denies access because of an I/O error or a specific type of security error.</exception>
	public static bool Serialize<T>( T? obj, string fileName, JsonSerializerOptions? options = null ) where T : class
	{
		if( obj is null || string.IsNullOrWhiteSpace( fileName ) ) { return false; }
		options ??= DefaultSerializerOptions();
		string json = JsonSerializer.Serialize( obj, options );
		File.WriteAllText( fileName, json );
		return true;
	}

	/// <summary>Returns a Json string of the provided object type.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="obj">Object to serialize.</param>
	/// <param name="options">Optional Json serializer options.</param>
	/// <returns><see langword="null"/> is returned if the serialization fails.</returns>
	/// <exception cref="NotSupportedException">Thrown when an invoked method is not supported, or when there
	/// is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
	public static string? Serialize<T>( T? obj, JsonSerializerOptions? options = null ) where T : class
	{
		if( obj is null ) { return null; }
		options ??= DefaultSerializerOptions();
		return JsonSerializer.Serialize( obj, options );
	}

	/// <summary>Gets a default set of Json Serializer options.</summary>
	/// <returns>
	/// <code>
	/// AllowTrailingCommas = true
	/// IgnoreReadOnlyProperties = true
	/// MaxDepth = 6
	/// PropertyNameCaseInsensitive = true
	/// </code>
	/// </returns>
	public static JsonSerializerOptions DefaultSerializerOptions()
	{
		return new JsonSerializerOptions
		{
			AllowTrailingCommas = true,
			IgnoreReadOnlyProperties = true,
			MaxDepth = 6,
			PropertyNameCaseInsensitive = true,
			Encoder = JavaScriptEncoder.Create( UnicodeRanges.BasicLatin ),
		};
	}
}