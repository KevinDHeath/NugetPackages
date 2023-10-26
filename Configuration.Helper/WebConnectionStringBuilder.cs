// Ignore Spelling: Auth Thumbprint

using System.Data.Common;

namespace KevinDHeath.Configuration.Helper;

/// <summary>
/// Provides a simple way to create and manage the contents of connection
/// strings used for a Microsoft Dataverse connection. 
/// </summary>
/// <remarks>
/// Use connection strings in XRM tooling to connect to Microsoft Dataverse
/// https://learn.microsoft.com/en-us/power-apps/developer/data-platform/xrm-tooling/use-connection-strings-xrm-tooling-connect
/// </remarks>
public class WebConnectionStringBuilder : DbConnectionStringBuilder
{
	#region Key Aliases

	private static class Alias
	{
		internal static readonly string[] url = { @"Url", "ServiceUri", "Service Uri", "Server" };
		internal static readonly string[] userName = { "UserName", "User Name", "UserId", " User Id" };
		internal static readonly string[] password = { "Password" };
		internal static readonly string[] homeRealmUri = { "HomeRealmUri", "Home Realm Uri" };
		internal static readonly string[] authType = { "AuthType", "AuthenticationType" };
		internal static readonly string[] requireNewInstance = { "RequireNewInstance" };
		internal static readonly string[] clientId = { "ClientId", "AppId", "ApplicationId" };
		internal static readonly string[] clientSecret = { "ClientSecret", "Secret" };
		internal static readonly string[] redirectUri = { "RedirectUri", "ReplyUrl" };
		internal static readonly string[] tokenCacheStorePath = { "TokenCacheStorePath" };
		internal static readonly string[] loginPrompt = { "LoginPrompt" };
		internal static readonly string[] storeName = { "StoreName", "CertificateStoreName" };
		internal static readonly string[] thumbprint = { @"Thumbprint", "CertThumbprint" };
		internal static readonly string[] skipDiscovery = { "SkipDiscovery" };
		internal static readonly string[] integratedSecurity = { "Integrated Security" };
	}

	#endregion

	#region Authentication Types

	/// <summary>Dynamics 365 Web service authentication types.</summary>
	/// <remarks>
	/// Only OAuth, Certificate, ClientSecret and Office365 are permitted values
	/// for Dataverse environments.
	/// Authenticate with Microsoft Dataverse web services
	/// https://docs.microsoft.com/en-us/powerapps/developer/data-platform/authentication
	/// </remarks>
	public enum AuthType
	{
		/// <summary>Open standard authorization protocol for access delegation.</summary>
		OAuth,
		/// <summary>Client secrets to enable server-to-server authentication scenarios.</summary>
		ClientSecret,
		/// <summary>Certificates to enable server-to-server authentication scenarios.</summary>
		Certificate,
		/// <summary>Use of the WS-Trust authentication security protocol is no longer recommended</summary>
		Office365,
		/// <summary>Dynamics 365 On-premises Active Directory authentication.</summary>
		AD,
		/// <summary>Dynamics 365 Internet-facing deployment authentication.</summary>
		IFD
	}

	#endregion

	#region Login Prompt Types

	/// <summary>Dynamics 365 Web service login types.</summary>
	/// <remarks>The item with a value of zero is the default.</remarks>
	public enum LoginPromptType
	{
		/// <summary>Does not prompt the user to specify credentials.</summary>
		Never = 0,
		/// <summary>Always prompts the user to specify credentials.</summary>
		Always,
		/// <summary>Allows the user to select in the login control interface whether to display the prompt or not.</summary>
		Auto
	}

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the URL to the Dataverse environment. The URL can use
	/// HTTP or HTTPS protocol, and the port is optional.
	/// </summary>
	public string Url
	{
		get
		{
			const string lastChar = @"/";
			var value = GetProperty( Alias.url );
			// Make sure the URL ends with a delimiting character 
			if( value.Length > 0 & !value.EndsWith( lastChar ) & !value.EndsWith( @"\" ) )
			{
				return value + lastChar;
			}
			return value;
		}
		set { SetProperty( value, Alias.url ); }
	}

	/// <summary>Gets or sets the user name associated with the credentials.</summary>
	public string UserName
	{
		get { return GetProperty( Alias.userName ); }
		set { SetProperty( value, Alias.userName ); }
	}

	/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
	public string Password
	{
		get { return GetProperty( Alias.password ); }
		set { SetProperty( value, Alias.password ); }
	}

	/// <summary>Gets or sets the Home Realm Uniform Resource Identifier.</summary>
	/// <remarks>
	/// Set this property to a non-null value when a second AD FS instance is configured as an
	/// identity provider to the AD FS instance that Dynamics 365 has been configured with
	/// for claims authentication. The parameter value is the URI of the WS-Trust metadata
	/// endpoint of the second AD FS instance.
	/// </remarks>
	public string HomeRealmUri
	{
		get { return GetProperty( Alias.homeRealmUri ); }
		set { SetProperty( value, Alias.homeRealmUri ); }
	}

	/// <summary>Gets or sets the authentication type to connect to Dataverse environment.</summary>
	public AuthType AuthenticationType
	{
		get { return ConvertToAuthType( GetProperty( Alias.authType ) ); }
		set { SetProperty( value.ToString(), Alias.authType ); }
	}

	/// <summary>
	/// Gets or sets whether to reuse an existing connection if recalled
	/// while the connection is still active.
	/// </summary>
	/// <remarks>The default is false.</remarks>
	public bool RequireNewInstance
	{
		get { return ConvertToBool( GetProperty( Alias.requireNewInstance ) ); }
		set { SetProperty( value.ToString().ToLower(), Alias.requireNewInstance ); }
	}

	/// <summary>
	/// Gets or sets the ClientID assigned when the registered application
	/// in Azure Active Directory or Active Directory Federation Services (AD FS).
	/// </summary>
	public string ClientId
	{
		get { return GetProperty( Alias.clientId ); }
		set { SetProperty( value, Alias.clientId ); }
	}

	/// <summary>Gets or sets the secret when authentication type is set to ClientSecret.</summary>
	public string ClientSecret
	{
		get { return GetProperty( Alias.clientSecret ); }
		set { SetProperty( value, Alias.clientSecret ); }
	}

	/// <summary>
	/// Gets or sets the redirect URI of the application registered in Azure
	/// Active Directory or Active Directory Federation Services (AD FS).
	/// </summary>
	public string RedirectUri
	{
		get { return GetProperty( Alias.redirectUri ); }
		set { SetProperty( value, Alias.redirectUri ); }
	}

	/// <summary>
	/// Gets or sets the full path to the location where the user token cache should be stored.
	/// </summary>
	/// <remarks>
	/// Required only with a web service configured for OAuth authentication.
	/// </remarks>
	public string TokenCacheStorePath
	{
		get { return GetProperty( Alias.tokenCacheStorePath ); }
		set { SetProperty( value, Alias.tokenCacheStorePath ); }
	}

	/// <summary>
	/// Gets or sets whether the user is prompted for credentials if the credentials are not supplied.
	/// </summary>
	/// <remarks>
	/// Required only with a web service configured for OAuth authentication.
	/// </remarks>
	public LoginPromptType LoginPrompt
	{
		get { return ConvertToLoginPromptType( GetProperty( Alias.loginPrompt ) ); }
		set { SetProperty( value.ToString(), Alias.loginPrompt ); }
	}

	/// <summary>
	/// Gets or sets the store name where the certificate identified by
	/// thumb-print can be found. When set, Thumb-print is required.
	/// </summary>
	public string StoreName
	{
		get { return GetProperty( Alias.storeName ); }
		set { SetProperty( value, Alias.storeName ); }
	}

	/// <summary>
	/// Gets or sets the thumb-print of the certificate to be utilized during
	/// an S2S connection. When set, AppID is required and UserID and Password
	/// values are ignored.
	/// </summary>
	public string Thumbprint
	{
		get { return GetProperty( Alias.thumbprint ); }
		set { SetProperty( value, Alias.thumbprint ); }
	}

	/// <summary>
	/// Gets or sets whether to call instance discovery to determine the connection
	/// URI for a given instance.
	/// As of NuGet release Microsoft.CrmSdk.XrmTooling.CoreAssembly Version 9.0.2.7
	/// </summary>
	public bool SkipDiscovery
	{
		get { return ConvertToBool( GetProperty( Alias.skipDiscovery ) ); }
		set { SetProperty( value.ToString().ToLower(), Alias.skipDiscovery ); }
	}

	/// <summary>
	/// Gets or sets whether to use current windows credentials to attempt to create
	/// a token for the instances.
	/// As of NuGet release Microsoft.CrmSdk.XrmTooling.CoreAssembly Version 9.1.0.21
	/// </summary>
	public bool IntegratedSecurity
	{
		get { return ConvertToBool( GetProperty( Alias.integratedSecurity ) ); }
		set { SetProperty( value.ToString().ToLower(), Alias.integratedSecurity ); }
	}

	#endregion

	#region Constructors

	/// <summary>Initializes a new instance of the WebConnectionStringBuilder class.</summary>
	public WebConnectionStringBuilder()
	{ }

	/// <summary>
	/// Initializes a new instance of the WebConnectionStringBuilder class using a supplied connection string. 
	/// </summary>
	/// <param name="connectionString">The basis for the object's internal connection information.
	/// Parsed into name/value pairs.</param>
	public WebConnectionStringBuilder( string connectionString )
	{
		if( !string.IsNullOrWhiteSpace( connectionString ) )
		{
			ConnectionString = connectionString.Trim();
		}
	}

	#endregion

	#region Private Methods

	/// <summary>Get the value for a property key.</summary>
	/// <param name="keyAliases">Collection of keywords.</param>
	/// <returns>An empty string is returned if the key is not found.</returns>
	private string GetProperty( params string[] keyAliases )
	{
		foreach( var alias in keyAliases )
		{
			if( TryGetValue( alias, out object? value ) )
			{
				string? wrk = value.ToString();
				if( wrk is not null ) { return wrk.Trim(); }
			}
		}

		return string.Empty;
	}

	/// <summary>Sets the value for a property key.</summary>
	/// <param name="val">Value to set.</param>
	/// <param name="keyAliases">Collection of keywords.</param>
	private void SetProperty( string val, params string[] keyAliases )
	{
		val = val.Trim();

		// If value is empty string then remove the parameter
		if( val.Length == 0 )
		{
			RemoveValue( keyAliases );
		}
		else
		{
			// Set the new value
			SetValue( val, keyAliases );
		}
	}

	/// <summary>Removes a parameter from the connection string.</summary>
	/// <param name="keyAliases">Collection of keyword aliases.</param>
	/// <returns>
	/// The value of the key before it is removed is returned.
	/// If the key is not found an empty string is returned.
	/// </returns>
	private string RemoveValue( params string[] keyAliases )
	{
		string? retValue = null;
		foreach( var alias in keyAliases )
		{
			if( TryGetValue( alias, out object? value ) )
			{
				if( null == retValue )
				{
					retValue = value.ToString();
				}
				Remove( alias );
			}
		}

		return retValue ?? string.Empty;
	}

	/// <summary>Set the value for a parameter key.</summary>
	/// <param name="newValue">New value</param>
	/// <param name="keyAliases">Collection of keywords.</param>
	private void SetValue( string newValue, params string[] keyAliases )
	{
		foreach( var alias in keyAliases )
		{
			if( TryGetValue( alias, out _ ) )
			{
				this[alias] = newValue;
				return;
			}
		}

		this[keyAliases[0]] = newValue;
	}

	/// <summary>Converts a string to a boolean.</summary>
	/// <param name="valBool">Boolean string.</param>
	/// <param name="dftValue">Default value. False is assumed if a value is not provided.</param>
	/// <returns>The default is returned if the string is not a valid boolean.</returns>
	private static bool ConvertToBool( string valBool, bool dftValue = false )
	{
		return bool.TryParse( valBool, out bool retValue ) ? retValue : dftValue;
	}

	/// <summary>Converts a string to a Dynamics 365 Web service authentication type.</summary>
	/// <param name="authType">Dynamics 365 Web service authentication type string.</param>
	/// <returns>The default value of AD is returned if the string is not an authentication type.</returns>
	private static AuthType ConvertToAuthType( string authType )
	{
		_ = Enum.TryParse( authType.Trim(), out AuthType retValue );
		return retValue;
	}

	/// <summary>Converts a string to a Dynamics 365 Web service login type.</summary>
	/// <param name="loginType">Dynamics 365 Web login type string.</param>
	/// <returns>The default value of Never is returned if the string is not a login type.</returns>
	private static LoginPromptType ConvertToLoginPromptType( string loginType )
	{
		_ = Enum.TryParse( loginType.Trim(), out LoginPromptType retValue );
		return retValue;
	}

	#endregion
}