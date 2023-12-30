# Wpf.Controls
Provides UI controls for .NET Windows Presentation Foundation applications.

## Change Log
- v2.0.2
  - Fixed FilePathTextBox not using correct relative path when filename is empty. 
- v2.0.1
  - Corrected the Source Link paths by specifying the source repository URL as the root rather than a sub-folder.
- v2.0.0 - **Breaking change**
  - GitHub repository name changed from `MyProjects` to `NuGetPackages`.  
- v1.0.4
  - Target Framework updated to .NET 8.0
  - Replaced use of `FolderBrowserDialog` with new `OpenFolderDialog` so requirement of Windows Forms removed.
- v1.0.3
  - Updated project website and source repository links.
- v1.0.2
  - Package name prefix changed to kdheath.
  - `Common.Wpf` package dependency name changed to `Common.Wpf.Resources`.
- v1.0.1
  - Introduced the IsErrorShown dependency property to replace the need for 'WithErrors' styles.
  - Added `ComboBox` and `DataPicker` controls to replace the styles.
  - Added `NumericSpinner` control to increase and decrease numeric values in a TextBox.
  - Added `FilePathTextBox` control for the selection of a file or folder in a `TextBox`.
- v1.0.0 - Package created.
