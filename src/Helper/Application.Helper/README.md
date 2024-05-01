# Application.Helper
[<img src="https://kevindheath.github.io/codecoverage/helper/badge_combined.svg">](https://kevindheath.github.io/codecoverage/helper/html/)

## Change Log
- [v2.0.5](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.5.1)
  - Include documentation files in release build packages.
- [v2.0.4](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.4.1)
  - Added package tags.
- [v2.0.3](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.3.1)
  - `GenericException.FormatException` method for `AggregateException` now logs stack traces from all inner exceptions.
  - Added an optional `configFile` parameter to the `ConsoleApp` constructor. The default is `appsettings.json`
- [v2.0.2](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.2.2)
  - Added an optional `detectDebugMode` parameter to the `ConsoleApp` constructor with a default value of `true`.
  - Added a set-only `IsUnitTest` property with a default value of false.
  - Fixed the formating of an `AggregateException` in the `GenericException.FormatException` method.
- [v2.0.1](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.1)
  - Corrected the Source Link paths by specifying the source repository URL as the root rather than a sub-folder.
- [v2.0.0](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.0) - **Breaking change**
  - GitHub repository name changed from `MyProjects` to `NuGetPackages`.  
- v1.0.3 **Deprecated**
  - Updated project website and source repository links.
- v1.0.2 **Deprecated**
  - Package name prefix changed to kdheath.
  - Remove strong-signing of assemblies.
- v1.0.1 **Deprecated**
  - Support for `App.config` _(XML)_ **and** `appSettings.json` files.
  - Target Framework updated to .NET Standard 2.0.3
- v1.0.0 - Package created.
