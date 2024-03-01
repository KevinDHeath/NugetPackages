# Common.Core Change Log
[<img src="https://kevindheath.github.io/codecoverage/core/badge_combined.svg">](https://kevindheath.github.io/codecoverage/core/html/)

- v2.1.3
  - Applied code refactoring based on the Changed Risk Anti-Patterns statistics.
  - Fixed bug in the `Person` model when setting the `BirthDate` property and retrieving it in the `Read` method.
  - `DataServiceBase.PostResource` and `DataServiceBase.PutResource` methods now accept a null `obj` parameter.
- [v2.1.2](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.2.1)
  - See [Refactoring Notes](v2.1.2-Notes.md) for details of the code refactoring based on the January 31, 2024 unit test results.
  - Added an optional `maxDepth` parameter to the `JsonHelper.ReadAppSettings` method with a default value of 2.
  - Added an optional `timeoutSeconds` parameter to the `DataServiceBase` constructor with a default value of 100 seconds.
- [v2.1.1](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.3)
  - Added a `Find` method to the `IDataFactory` interface.
- [v2.1.0](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.2) - **Breaking change**
  - See [v2.1.0 Notes](v2.1.0-Notes.md) for information on migrating databases containing tables for any of the models mentioned below.
  - `Common.Models.Address`, `Common.Interfaces.IPerson` and `Common.Models.Person` have changed to support multi-country address data.
  - `Common.Classes.AddressFactory` is removed, use `Common.Classes.AddressFactoryBase` _(which was added in v2.0.2)_ instead.
  - `Common.Models.USState` is removed, use `Common.Models.Province` instead.
  - `Common.Models.USZipCode` is removed, use `Common.Models.Postcode` instead.
  - `AddressDataBase` changed to support setting of the `DefaultCountry` property in derived class constructor.
- [v2.0.3](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.3) - **Breaking change**
  - `AddressFactoryBase` namespace changed from `Common.Data.Classes` to `Common.Core.Classes`
  - Fixed issue in `ModelDataError` when an unsupported annotation is applied to a property. For example `StringLength` for a `DataTime` property will result in an `InvalidCastException` exception.
- [v2.0.2](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.2)
  - Added the `AddressFactoryBase` class to support multi-country address data.
- v2.0.1
  - Added `Postcode` model to replace `USZipCode`\
  Column order changed in data file\
  ZipCode -> Code _(size changed from 5 to 10)_\
  State -> Province _(size changed from 2 to 10)_\
  County - no longer required\
  City - no longer required
  - Added `Province` model to replace `USState`\
  Alpha -> Code _(size changed from 2 to 10)_\
  Capital - Removed
- v2.0.0 - **Breaking change**
  - GitHub repository name changed from `MyProjects` to `HomeBase`
  - Updated to .NET 8
- v1.0.3
  - Added Converters for Json.
  - Added `Interfaces.IDataFactory`
  - Added `Classes.DataFactoryBase`
  - Renamed `Common.Core.Classes.EditableHelper` to `ReflectionHelper`
- v1.0.2
  - Added the `ModelData` base class and the `ResultsSet` class to provide data set paging information
  - Moved the `Common.Data.Interfaces` to this module so they can be referenced without having to include the large `Common.Data` component which has the JSON data files embedded
  - Moved the `Common.Data.Models` classes for the same reason
  - Updated to target .NET 7
- v1.0.1 - Package created
