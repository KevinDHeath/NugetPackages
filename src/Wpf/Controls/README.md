# Wpf.Controls
Provides UI controls for .NET Windows Presentation Foundation applications.

## Change Log
- [v2.0.6](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.4.2) - **Breaking change**
  - Switch from using Font Awesome true-type fonts to SVG images. See [Font Awesome Upgrade](v2.0.6-Notes.md) for details of the changes required.
 - [v2.0.5](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.4.1)
   - Added package tags.
- [v2.0.4](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2024.3.1)
  - Minimum version of `kdheath.Wpf.Resources` dependency changed to 2.0.1.
- [v2.0.3](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.3)
  - Added the ability to bind the `PasswordBox.Password` property using a `PasswordBoxExtend` extension class.
- [v2.0.2](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.2)
  - Fixed FilePathTextBox not using correct relative path when filename is empty.
  - ComboBox to have white background whether or not it is editable.
- [v2.0.1](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.1)
  - Corrected the Source Link paths by specifying the source repository URL as the root rather than a sub-folder.
- [v2.0.0](https://github.com/KevinDHeath/NuGetPackages/releases/tag/v2.0.0) - **Breaking change**
  - GitHub repository name changed from `MyProjects` to `NuGetPackages`.  
- v1.0.4 **Deprecated**
  - Target Framework updated to .NET 8.0
  - Replaced use of `FolderBrowserDialog` with new `OpenFolderDialog` so requirement of Windows Forms removed.
- v1.0.3 **Deprecated**
  - Updated project website and source repository links.
- v1.0.2 **Deprecated**
  - Package name prefix changed to kdheath.
  - `Common.Wpf` package dependency name changed to `Common.Wpf.Resources`.
- v1.0.1 **Deprecated**
  - Introduced the IsErrorShown dependency property to replace the need for 'WithErrors' styles.
  - Added `ComboBox` and `DataPicker` controls to replace the styles.
  - Added `NumericSpinner` control to increase and decrease numeric values in a TextBox.
  - Added `FilePathTextBox` control for the selection of a file or folder in a `TextBox`.
- v1.0.0 - Package created.
