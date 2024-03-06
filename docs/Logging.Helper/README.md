## About
The Logging.Helper package is a light-weight platform for .NET with rich log routing and management capabilities provided by NLog.

See [Change Log](https://github.com/KevinDHeath/NuGetPackages/tree/main/src/Helper/Logging.Helper#logginghelper) for all release notes.

## Key Features
- Supports .NET Framework, .NET Core, and .NET 5.0+
- By default, logging will only be output to the console window.

## Main Types
- `Logger` - Class that provides common logging properties and methods.
- `LoggerEvent` - Base class to handle logging using an event handler.
- `LoggerEventArgs` - Common event arguments to use when logging a message.

See [.NET Helper Packages](https://kevindheath.github.io/nuget/html/N_Logging_Helper.htm) for technical documentation.

## How to Use
To use in a C# program:
```c#
using Logging.Helper;

internal static readonly Logger sLogger = new( typeof( Program ) );
```
\
Logging can be performed using:
```c#
sLogger.Debug( "Logging a debug message" );
sLogger.Info( "Logging a information message" );
sLogger.Warn( "Logging a warning message" );
sLogger.Error( "Logging an error message" );
sLogger.Error( "With exception", ex );
```
**Note:** `sLogger.Log` can also be used to log an informational message.

To allow a processing class to log using the logger created in the main program it must inherit from the `Logging.Helper.LoggerEvent` class.

The processing class would look something like this:
```c#
using System;
using Logging.Helper;

public class ProcessingClass : LoggerEvent
{
  internal int DoProcessing()
  {
    var retValue = 0; // Assume normal completion
    try
    {
      // This method is inherited from the LoggerEvent class
      RaiseLogEvent( "Information message about the processing..." );
    }
    catch( Exception ex )
    {
      RaiseLogEvent( ex.ToString(), LogSeverity.Fatal );
      retValue = -1; // Abnormal completion
    }
    return retValue;
  }
}
```
The raise log handler must be set for the `ProcessingClass` object so that it will use the internal logger defined in the main program.

```c#
// Create an object from a class that inherits LoggerEvent
var logicClass = new ProcessingClass();
logicClass.RaiseLogHandler += Logging.OnRaiseLog;

// Do Processing and set the Exit code
logicClass.DoProcessing();
```
### Advanced Logging
To enable advanced NLog logging create a `nlog.config` _(all lowercase)_ file in the root of your application project and set the file `Property: Copy if newer.`

- For detailed information about NLog configurations [Configuration File](https://github.com/NLog/NLog/wiki/Configuration-file).
- For tutorials and documentation see the [NLog Wiki](https://github.com/nlog/nlog/wiki) site.

Example XML for a stand-alone NLog.config:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="File" name="logfile" fileName="c:\temp\console-example.log"
            layout="${longdate} ${uppercase:${level:padding=-5}} ${message}"/>
    <target xsi:type="Console" name="logconsole"
            layout="${longdate} ${uppercase:${level:padding=-5}} ${message}"/>
  </targets>
  <rules>
    <logger name="*" minlevel="Debug" writeTo="logconsole" />
    <logger name="*" minlevel="Debug" writeTo="logfile" />
  </rules>
</nlog>
```

## Feedback
This is provided as open source under the MIT license.\
Bug reports and contributions are welcome [at the GitHub repository](https://github.com/KevinDHeath/NuGetPackages).