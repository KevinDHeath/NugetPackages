# Helper Packages
The Helper Packages documentation for .NET applications can be found at [Helper Packages](https://kevindheath.github.io/nuget/html/R_Project_NuGetPackages.htm)

> Note: .NET Standard 2.0 is the highest version of .NET Standard that is supported by the .NET Framework.\
> See [The future of .NET Standard](https://devblogs.microsoft.com/dotnet/the-future-of-net-standard/) for more information.

## Application.Helper
[![NuGet](https://img.shields.io/nuget/v/kdheath.Application.Helper.svg)](https://www.nuget.org/packages/kdheath.Application.Helper)
[![NuGet](https://img.shields.io/nuget/dt/kdheath.Application.Helper.svg)](https://www.nuget.org/packages/kdheath.Application.Helper)

Framework: .NET Standard 2.0.3

Dependencies:
- None
 
## Configuration.Helper
[![NuGet](https://img.shields.io/nuget/v/kdheath.Configuration.Helper.svg)](https://www.nuget.org/packages/kdheath.Configuration.Helper)
[![NuGet](https://img.shields.io/nuget/dt/kdheath.Configuration.Helper.svg)](https://www.nuget.org/packages/kdheath.Configuration.Helper)

Framework: .NET 6.0 _and_ .NET 8.0

Dependencies:
- None

## Json.Converters
Framework: .NET 6.0 _and_ .NET 8.0

> Note: This is not yet available on NuGet.

Dependencies:
- None

References:
- [JSON serialization and de-serialization in .NET](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)\
[Compare Newtonsoft.Json to System.Text.Json, and migrate to System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/migrate-from-newtonsoft)
- [De-serializing generic interfaces with System.Text.Json](https://www.mrlacey.com/2019/10/deserializing-generic-interfaces-with.html)

## Logging.Helper
[![NuGet](https://img.shields.io/nuget/v/kdheath.Logging.Helper.svg)](https://www.nuget.org/packages/kdheath.Logging.Helper)
[![NuGet](https://img.shields.io/nuget/dt/kdheath.Logging.Helper.svg)](https://www.nuget.org/packages/kdheath.Logging.Helper)

Framework: .NET Standard 2.0.3

Dependencies:
- [NLog](https://www.nuget.org/packages/NLog)
- [kdheath.Application.Helper](https://www.nuget.org/packages/kdheath.Application.Helper)