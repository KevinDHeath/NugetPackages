# Wpf.Resources
Provides UI resources for .NET Windows Presentation Foundation applications.

## Change Log
- v2.0.3
  - Fixed `DateOnly` rule so it doesn't wipe out invalid values. 
  - Added ability for `DelegateCommand` to monitor changes to a specific object and properties.
  - Added styles `commonPasswordBoxStyle` and `commonPasswordBoxWithErrorsStyle` for the `PasswordBox` control.
- v2.0.2
  - Added `SubscriberCount` property to the `DelegateCommand` class.
  - Added `DateOnlyToString` converter. 
  - Added `DoubleToString` converter.
- v2.0.1
  - Corrected the Source Link paths by specifying the source repository URL as the root rather than a sub-folder.
- v2.0.0 - **Breaking change**
  - GitHub repository name changed from `MyProjects` to `NuGetPackages`.  
- 1.0.4
  - Added support for .NET 8.0
- 1.0.3
  - Updated project website and source repository links.
- 1.0.2
  - Package name prefix changed to kdheath.
  - Name changed from `Common.Wpf` with no changes to any of the namespace names.
  - Removed the `DateOnlyToString`, `DateTimeToString`, and `UIntegerToString` converters.
- 1.0.1
  - Removed the `ComboBox.xaml` and `DatePicker.xaml` styles as they have been replaced with custom controls.
- 1.0.0 - Package created.
