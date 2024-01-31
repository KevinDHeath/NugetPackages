# Package Testing
An _integration test_ differs from a unit test in that it exercises two or more software components' ability to function together, also known as their "integration." These tests operate on a broader spectrum of the system under test, whereas unit tests focus on individual components. Often, integration tests do include infrastructure concerns.

There are 3 types of integration testing projects:
- MVVM - Core component to provide data and services for user interface applications.
- Console - Used to test the functionality of the `Helper` packages.
- WPF - UI used to test the functionality of the `WPF` packages.

## WPF User Interface
This is based on the 'Navigation MVVM' tutorial by _[Singleton Sean](https://github.com/SingletonSean)_ and demonstrates how to combine navigation operations via the composite design pattern.
- [WPF Navigation](https://www.youtube.com/watch?v=N26C_Cq-gAY&list=PLA8ZIAm2I03ggP55JbLOrXl6puKw4rEb2) <sub><sup>_(YouTube)_</sup></sub>
- [WPF Tutorials](https://github.com/SingletonSean/wpf-tutorials/tree/master/NavigationMVVM) <sub><sup>_(GitHub)_</sup></sub>
- [WPF UI Workshops](https://github.com/SingletonSean/wpf-ui-workshops/tree/master) <sub><sup>_(GitHub)_</sup></sub>

Dependencies:
- [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection)
- [kdheath.Common.Core](https://www.nuget.org/packages/kdheath.Common.Core)

