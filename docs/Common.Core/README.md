## About
The Common.Core package provides classes and interfaces for .NET components.

See [Change Log](https://github.com/KevinDHeath/NuGetPackages/blob/main/src/Common/Core/README.md) for all release notes.

## Key Features
It provides:
- Classes
- Converters
- Interfaces
- Models

## Main Types
- `Classes.JsonHelper` - Provides methods for serialization of data files and custom object types.
- `Classes.ModelBase` - Base class for models that require the `INotifyPropertyChanged` interface.
- `Classes.ModelDataError` - Base class for models _(and view models)_ that require the `INotifyPropertyChanged` and `INotifyDataErrorInfo` interfaces.
- `Converters.JsonBoolean` - Additionally converts `1/0`. `y/n`, or `yes/no` string values to a boolean.
- `Converters.StringConverter` - Provides Try/Parse static methods to convert strings to other data types.

See [.NET Helper Packages](https://kevindheath.github.io/nuget/html/N_Common_Core_Classes.htm) for technical documentation.

## Feedback
This is provided as open source under the MIT license.\
Bug reports and contributions are welcome in the [GitHub repository](https://github.com/KevinDHeath/NuGetPackages/issues).