## About
The Wpf.Resources package contains Windows Desktop resources which can be includes in .NET WPF projects.

See [Change Log](https://github.com/KevinDHeath/NuGetPackages/tree/main/src/Wpf/Resources#change-log) for all release notes.

## Key Features
Custom styles for `Buttons`, `CheckBoxes`, `ListViews`, `TabControls`, and `TextBoxes`.

## Main Types
- `RelayCommand` - Classes to relay functionality to other objects by invoking delegates.
- `ConverterBase` - Base class for `IValueConverter` classes.
- `RuleBase` - Base class for validation rules.

See [WPF Packages](https://kevindheath.github.io/shfb/html/9488fab8-02de-4046-a582-c44f4c2a945f.htm) for technical documentation.

## How to Use
To include the resources in a WPF project:
- Add a reference to the `Common.Wpf.Resources` package.
- Include the required resource dictionaries in the `App.xaml` file.

```xml
<ResourceDictionary>
  <ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/Common.Wpf.Resources;component/Themes/Common.xaml"/>
  </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

## Feedback
This is provided as open source under the MIT license.\
Bug reports and contributions are welcome [at the GitHub repository](https://github.com/KevinDHeath/NuGetPackages).