## About
The Configuration.Helper package is a light-weight platform for .NET to support application settings in old-style App.config files, and as of version 1.0.1, the newer style of JSON files.

See [Change Log](https://github.com/KevinDHeath/NuGetPackages/tree/main/src/Helper/Configuration.Helper#configurationhelper) for all release notes.

## Main Types
- `ConfigFileHelper` - Helper class for Configuration file access.
- `IOHelper` - Helper class for `System.IO` operations.
- `WebConnectionStringBuilder` - Provides a simple way to create and manage the contents of connection strings used for a Microsoft Dataverse connection.

See [.NET Helper Packages](https://kevindheath.github.io/nuget/html/N_Configuration_Helper.htm) for technical documentation.

## How to Use
- The naming of the standard sections **`AppSettings`** and **`ConnectionStrings`** along with any custom sections are case _insensitive_.
- All the values must be defined as strings.

A JSON style configuration file would look like:
```json
{
  "appSettings": {
    "FavoriteMovie": "Dune",
    "FavoriteDirector": "James Cameron",
    "FavoriteActor": "Johnny Depp"
  },
  "connectionStrings": {
    "Movies": "DataSource=localhost;Initial Catalog=MovieCatalog;Integrated Security=True"
  },
  "Custom": {
    "Password": "abc123"
  }
}
```
\
The older style XML application settings file would look like:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="FavouriteMovie" value="Dune" />
    <add key="FavouriteDirector" value="James Cameron" />
    <add key="FavouriteActor" value="Johnny Depp" />
   </appSettings>

  <connectionStrings>
    <clear />
    <add name="Movies" connectionString="DataSource=localhost;Initial Catalog=MovieCatalog;Integrated Security=True" />
  </connectionStrings>

  <Custom>
    <add key="Password" value="abc123" />
  </Custom>
</configuration>
```
\
To access the settings from C# code:
```c#
using Configuration.Helper;

// Load the configuration settings
var config = ConfigFileHelper.GetConfiguration( "appSettings.json" );

// Get a setting value from the AppSettings section
var director = config.GetSetting( "favoritedirector" );

// Get a setting value from the ConnectionStrings section
var connect = config.ConnectionStrings.GetSetting( "movies" );

// Get a setting value from the Custom section
var password = config.GetSection( "custom" ).GetSetting( "password" );

// Add or update a setting value in the AppSettings section
var ok = config.AddSetting( "FavouriteActress", "Julia Roberts" );
ok = config.AddSetting( @"favouriteactress", "Angelina Jolie" );
```

## Feedback
This is provided as open source under the MIT license.\
Bug reports and contributions are welcome [at the GitHub repository](https://github.com/KevinDHeath/NuGetPackages).
