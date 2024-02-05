using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Common.Core.Classes;

/// <summary>Base class for services consuming data from a RESTful API.</summary>
public class DataServiceBase
{
	private readonly HttpClient _httpClient;
	private readonly string _mediaType = "application/json";

	#region Constructor and Initialization

	/// <summary>Initializes a new instance of the DataServiceBase class.</summary>
	/// <param name="baseUri">Base Uniform Resource Identifier.</param>
	/// <param name="timeoutSeconds">The number of seconds to wait before a request times out.</param>
	public DataServiceBase( string baseUri, int timeoutSeconds = 100 )
	{
		_httpClient = CreateSharedClient( baseUri, timeoutSeconds );
		_httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( _mediaType ) );
	}

	#endregion

	/// <summary>Deletes a resource.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="uri">Uniform Resource Identifier.</param>
	/// <param name="options">Json serializer options.</param>
	/// <returns>The deleted resource, or <see langword="null"/> if there were errors while processing the JSON.</returns>
	/// <exception cref="AggregateException">Represents one or more errors that occur during execution.</exception>
	public T? DeleteResource<T>( string uri, JsonSerializerOptions? options = null ) where T : class
	{
		CheckUri( ref uri );
		try
		{
			HttpRequestMessage rq = new( HttpMethod.Delete, uri );
			string json = GetResponseAsync( rq ).Result;
			return !string.IsNullOrEmpty( json ) ?
				JsonHelper.DeserializeJson<T>( ref json, options ) : null;
		}
		catch( AggregateException ) { throw; }
	}

	/// <summary>Gets a resource.</summary>
	/// <param name="uri">Uniform Resource Identifier.</param>
	/// <returns>Resource as a JSON string if there were no errors while processing the request.</returns>
	/// <exception cref="AggregateException">Represents one or more errors that occur during execution.</exception>
	public string? GetResource( string uri )
	{
		CheckUri( ref uri );
		try
		{
			HttpRequestMessage rq = new( HttpMethod.Get, uri );
			return GetResponseAsync( rq ).Result;
		}
		catch( AggregateException ) { throw; }
	}

	/// <summary>Creates a resource.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="uri">Uniform Resource Identifier.</param>
	/// <param name="obj">Resource to create.</param>
	/// <param name="options">Json serializer options.</param>
	/// <returns>The created resource, or <see langword="null"/> if there were errors while processing the JSON.</returns>
	/// <exception cref="AggregateException">Represents one or more errors that occur during execution.</exception>
	public T? PostResource<T>( string uri, T obj, JsonSerializerOptions? options = null ) where T : class
	{
		string? json = JsonHelper.Serialize( obj );
		if( json is null ) { return null; }

		CheckUri( ref uri );
		try
		{
			HttpRequestMessage rq = new( HttpMethod.Post, uri )
			{
				Content = new StringContent( json, Encoding.UTF8, _mediaType )
			};
			json = GetResponseAsync( rq ).Result;
			return !string.IsNullOrEmpty( json ) ?
				JsonHelper.DeserializeJson<T>( ref json, options ) : null;
		}
		catch( AggregateException ) { throw; }
	}

	/// <summary>Updates a resource.</summary>
	/// <typeparam name="T">Generic class or interface.</typeparam>
	/// <param name="uri">Uniform Resource Identifier.</param>
	/// <param name="obj">Resource to update.</param>
	/// <param name="options">Json serializer options.</param>
	/// <returns>The updated resource, or <see langword="null"/> if there were errors while processing the JSON.</returns>
	/// <exception cref="AggregateException">Represents one or more errors that occur during execution.</exception>
	public T? PutResource<T>( string uri, T obj, JsonSerializerOptions? options = null ) where T : class
	{
		string? json = JsonHelper.Serialize( obj, options );
		if( json is null ) { return null; }

		CheckUri( ref uri );
		try
		{
			HttpRequestMessage rq = new( HttpMethod.Put, uri )
			{
				Content = new StringContent( json, Encoding.UTF8, _mediaType )
			};
			json = GetResponseAsync( rq ).Result;
			return !string.IsNullOrEmpty( json ) ?
				JsonHelper.DeserializeJson<T>( ref json, options ) : null;
		}
		catch( AggregateException ) { throw; }
	}

	#region Private Methods

	private void CheckUri( ref string uri )
	{
		if( !uri.StartsWith( Uri.UriSchemeHttp, StringComparison.InvariantCultureIgnoreCase ) )
		{
			uri = _httpClient.BaseAddress?.OriginalString + uri;
		}
	}

	private static HttpClient CreateSharedClient( string siteUri, int timeout )
	{
		SocketsHttpHandler handler = new()
		{
			PooledConnectionLifetime = TimeSpan.FromMinutes( 15 ) // Recreate every 15 minutes
		};

		if( siteUri.Contains( '\\' ) ) { siteUri = siteUri.Replace( '\\', '/' ); }
		if( !siteUri.EndsWith( '/' ) ) { siteUri += "/"; }
		TimeSpan timespan = TimeSpan.FromSeconds( timeout );

		return new HttpClient( handler ) { BaseAddress = new Uri( siteUri ), Timeout = timespan };
	}

	// https://devblogs.microsoft.com/dotnet/configureawait-faq/
	private async Task<string> GetResponseAsync( HttpRequestMessage request )
	{
		HttpResponseMessage response = await _httpClient.SendAsync( request ).ConfigureAwait( false );
		_ = response.EnsureSuccessStatusCode();

		using StreamReader reader = new( response.Content.ReadAsStream() );
		return await reader.ReadToEndAsync().ConfigureAwait( false );
	}

	#endregion
}