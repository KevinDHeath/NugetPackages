# Wpf.Controls
[![NuGet](https://img.shields.io/nuget/v/kdheath.Wpf.Controls.svg)](https://www.nuget.org/packages/kdheath.Wpf.Controls)
[![NuGet](https://img.shields.io/nuget/dt/kdheath.Wpf.Controls.svg)](https://www.nuget.org/packages/kdheath.Wpf.Controls)\
Provides UI controls for .NET Windows Presentation Foundation applications.

## Change Log
- 1.0.4
  - Target Framework updated to .NET 8.0
  - Replaced use of `FolderBrowserDialog` with new `OpenFolderDialog` so requirement of Windows Forms removed.
- 1.0.3
  - Updated project website and source repository links.
- 1.0.2
  - Package name prefix changed to kdheath.
  - `Common.Wpf` package dependency name changed to `Common.Wpf.Resources`.
- 1.0.1
  - Introduced the IsErrorShown dependency property to replace the need for 'WithErrors' styles.
  - Added `ComboBox` and `DataPicker` controls to replace the styles.
  - Added `NumericSpinner` control to increase and decrease numeric values in a TextBox.
  - Added `FilePathTextBox` control for the selection of a file or folder in a `TextBox`.
- 1.0.0 - Package created.
