// Ignore Spelling: Auth IFD

using System;

namespace Helper.Tests.Configuration;

public class WebConnectionStringBuilderTests
{
	private const string cUrl = @"https://contosotest.crm.dynamics.com";
	private const string cAppId = @"51f81489-12ee-4a9e-aaae-a2591f45987d";

	public WebConnectionStringBuilderTests()
	{
		// For code coverage
		WebConnectionStringBuilder builder = new( @"Url=" + cUrl + @"/;" )
		{
			{ "AppId", cAppId }
		};

		_ = builder.Url;
		builder.ClientId = string.Empty;
		builder.Url = cUrl;

		_ = builder.Url;
		_ = builder.AuthenticationType;
		_ = builder.Thumbprint;
		_ = builder.ClientId;
		_ = builder.StoreName;
		_ = builder.ClientSecret;
		_ = builder.UserName;
		_ = builder.Password;
		_ = builder.RedirectUri;
		_ = builder.TokenCacheStorePath;
		_ = builder.LoginPrompt;
		_ = builder.IntegratedSecurity;
		_ = builder.RequireNewInstance;
		_ = builder.SkipDiscovery;
		_ = builder.HomeRealmUri;
		_ = builder.Domain;
	}

	[Fact]
	public void OAuth_authentication_should_be_Certificate()
	{
		// Arrange: Certificate based authentication
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.Certificate,
			Url = cUrl,
			Thumbprint = "{CertThumbPrintId}",
			ClientId = "{AppId}",
			StoreName = "{CertStoreName}"
		};

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=Certificate" );
	}

	[Fact]
	public void OAuth_authentication_should_be_ClientSecret()
	{
		// Arrange: ClientId or Client Secret based authentication
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.ClientSecret,
			Url = cUrl,
			ClientId = "{AppId}",
			ClientSecret = "{ClientSecret}",
		};

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=ClientSecret" );
	}

	[Fact]
	public void OAuth_authentication_should_be_OAuth()
	{
		// Arrange: OAuth using current logged in user with fall back UX to prompt for authentication
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.OAuth,
			UserName = "jsmith@contoso.onmicrosoft.com",
			Password = @"passcode",
			Url = cUrl,
			RedirectUri = @"app://58145B91-0C36-4500-8554-080854F2AC97",
			TokenCacheStorePath = @"c:\MyTokenCache",
			LoginPrompt = WebConnectionStringBuilder.LoginPromptType.Auto
		};
		builder.Add( "AppId", cAppId );

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=OAuth" );
	}

	[Fact]
	public void OAuth_authentication_should_be_Office365()
	{
		// Arrange: Named account using Office365 (deprecated)
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.Office365,
			UserName = "jsmith@contoso.onmicrosoft.com",
			Password = @"passcode",
			Url = cUrl
		};

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=Office365" );
	}

	[Fact]
	public void OnPremises_authentication_should_be_AD()
	{
		// Arrange: Integrated on-premises authentication
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.AD,
			Url = "https://contoso:8080/Test",
			IntegratedSecurity = true,
			RequireNewInstance = true,
			SkipDiscovery = true
		};

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=AD" );
	}

	[Fact]
	public void OnPremises_authentication_should_be_IFD()
	{
		// Arrange: Internet-facing on-premises authentication with delegation to a sub realm
		WebConnectionStringBuilder builder = new()
		{
			AuthenticationType = WebConnectionStringBuilder.AuthType.IFD,
			Url = "https://contoso:8080/Test",
			HomeRealmUri = "https://server-1.server.com/adfs/services/trust/mex/",
			Domain = "CONTOSO",
			UserName = @"jsmith",
			Password = @"passcode"
		};

		// Act
		string result = builder.ConnectionString;

		// Assert
		_ = result.Should().StartWith( "AuthType=IFD" );
	}
}