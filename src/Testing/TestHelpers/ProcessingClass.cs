using System;
using Logging.Helper;

namespace TestHarness;

public class ProcessingClass : LoggerEvent
{
	internal int DoProcessing()
	{
		var retValue = 0; // Assume normal completion

		// This method is inherited from the LoggerEvent class
		RaiseLogEvent( "Raising an error from the processing class", LogSeverity.Error );

		// Produce an exception
		int div = 0; var fatal = 10 / div;
		Console.WriteLine( fatal );

		return retValue;
	}
}