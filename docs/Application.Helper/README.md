## About
The Application.Helper package is a light-weight platform for .NET console applications.

See [Change Log](https://github.com/KevinDHeath/NuGetPackages/tree/main/src/Helper/Application.Helper#change-log) for all release notes.

## Key Features
- Supports .NET Framework, .NET Core, and .NET 5.0+

It provides:
- The title of the program from the assembly attributes.
- The name of the configuration file _(if it can be determined)_.
- The elapsed time of the starting and stopping of the program.
- Methods to suspend and resume elapse time recording are available.
- Indication of whether the program is running in debug mode.
- Indication of whether help was requested from the command line arguments.

## Main Types
- `ConsoleApp` - Helper class for Console Applications.
- `GenericException` - Represents errors that occur during application execution.

See [.NET Helper Packages](https://kevindheath.github.io/nuget/html/N_Application_Helper.htm) for technical documentation.

## How to Use
To provide help information about the program:
- Create a new class and use the `ConsoleApp` class as the base class.
- Override the `ShowProgramInfo()` method. This is called during the start process and is triggered by the user providing a question mark (?) anywhere in the first argument.

To use in a C# program:
```c#
using Application.Helper;

private static readonly ConsoleApp sApp = new();
```
The main entry point method would look like:

```c#
static void Main( string[] args )
{
  var result = false;
  try
  {
    sApp.StartApp( args );
    Console.WriteLine( sApp.FormatTitleLine( " Starting " + sApp.Title + " " ) );
    result = Processing();
  }
  catch( Exception ex )
  {
    // Do something to log the error
  }
  finally
  {
    sApp.StopApp( result );
  }
  Environment.Exit( Environment.ExitCode );
}
```

The output looks like this:

```dos
Start..: 20:16:31.8915290

------------------- Starting Test Harness [Version 1.0.1.1] --------------------

Runtime: 00:00:00.2836824
Result.: Success

Press any key to continue . . .
```

## Feedback
This is provided as open source under the MIT license.\
Bug reports and contributions are welcome [at the GitHub repository](https://github.com/KevinDHeath/NuGetPackages).