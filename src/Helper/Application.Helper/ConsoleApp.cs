using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Application.Helper
{
	/// <summary>Helper class for Console Applications.</summary>
	public class ConsoleApp
	{
		#region Properties

		/// <summary>Gets the name of the current application configuration file.</summary>
		public string ConfigFile { get; }

		/// <summary>Gets the application title.</summary>
		public string Title { get; private set; }

		/// <summary>Indicates whether execution help has been requested.</summary>
		public bool HelpRequest { get; private set; }

		/// <summary>Indicates whether a debugger is attached to the process.</summary>
		public bool DebugMode { get; }

		/// <summary>Gets the total elapsed time measured.</summary>
		public TimeSpan ElapsedTime => _stopWatch.Elapsed;

		/// <summary>Sets whether this is running as a unit test.</summary>
		public bool IsUnitTest { private get; set; } = false;

		#endregion

		#region Constructor

		/// <summary>Stop watch to calculate elapse time.</summary>
		private readonly Stopwatch _stopWatch = new Stopwatch();

		private bool _started;

		/// <summary>Creates a new instance of the ConsoleApp class.</summary>
		/// <param name="detectDebugMode">Detect if a debugger is attached or not.<br/>
		/// The default is <see langword="true"/></param>
		/// <exception cref="ArgumentException">Thrown when one of the arguments provided to a method is not valid.</exception>
		/// <exception cref="TypeLoadException">Thrown when type-loading failures occur.</exception>
		public ConsoleApp( bool detectDebugMode = true )
		{
			ConfigFile = GetAppConfigFile();
			Title = FormatTitle( Assembly.GetEntryAssembly() );
			DebugMode = !detectDebugMode || Debugger.IsAttached;
		}

		#endregion

		#region Private Methods

		/// <summary>Gets the name of the executable configuration file name.</summary>
		/// <returns>An empty string is returned if the configuration file could not be determined.</returns>
		private static string GetAppConfigFile()
		{
			string retValue = "appsettings.json";
			if( File.Exists( retValue ) ) return retValue;

			// Get the name of the executable
			retValue = Path.GetFileName( Assembly.GetEntryAssembly().Location );

			// Check for XML config file
			retValue = Path.ChangeExtension( retValue, ".config" );
			if( File.Exists( retValue ) ) return retValue;

			// Check for JSON config file
			retValue = Path.ChangeExtension( retValue, ".json" );
			if( File.Exists( retValue ) ) return retValue;

			return string.Empty;
		}

		private static T GetAssemblyAttribute<T>( ICustomAttributeProvider assembly ) where T : Attribute
		{
			object[] attributes = assembly.GetCustomAttributes( typeof( T ), false );
			return attributes.Length == 0 ? null : attributes.OfType<T>().SingleOrDefault();
		}

		private static void SetStartTime( ConsoleApp app )
		{
			if( !app.DebugMode ) { return; }

			// Log the start time
			Console.WriteLine( @"Start..: " + DateTime.Now.ToString( @"HH:mm:ss.fffffff" ) );
			Console.WriteLine();
		}

		private static void SetEndTime( ConsoleApp app, bool result, bool unitTest )
		{
			if( app._stopWatch.IsRunning ) { app.SuspendElapsed(); }

			if( !app.DebugMode ) return;

			// Log the completion and elapsed times
			string exitCode = Environment.ExitCode > 0 ? " with exit code " + Environment.ExitCode : string.Empty;
			Console.WriteLine();
			Console.WriteLine( @"Runtime: " + app.ElapsedTime );
			Console.WriteLine( @"Result.: " + ( result ? "Success" : "Failed" ) + exitCode );
			if( !Environment.UserInteractive ) return;

			Console.WriteLine( string.Empty );
			Console.Write( @"Press any key to continue . . . " );
			if( !unitTest ) { _ = Console.Read(); }
		}

		private static string FormatTitle( Assembly assembly )
		{
			AssemblyTitleAttribute titleAttr = GetAssemblyAttribute<AssemblyTitleAttribute>( assembly );
			string titleStr = titleAttr?.Title.Trim() ?? string.Empty;

			// Get the file version
			string versionStr = null;
			AssemblyFileVersionAttribute versionAttr = GetAssemblyAttribute<AssemblyFileVersionAttribute>( assembly );
			if( null != versionAttr ) { versionStr = versionAttr.Version.Trim(); }

			// If no file version get it from the full name
			if( null == versionStr ) { versionStr = assembly.GetName().Version.ToString(); }

			return versionStr.Length > 0 ? (titleStr + $" [Version {versionStr}]").Trim() : titleStr;
		}

		private void ShowProgramInfo( ICustomAttributeProvider assembly )
		{
			// Add the description to the title
			AssemblyDescriptionAttribute descAttr = GetAssemblyAttribute<AssemblyDescriptionAttribute>( assembly );
			string descStr = descAttr?.Description.Trim() ?? string.Empty;
			string titleStr = descStr.Length > 0 ? ( Title + $" {descStr}" ).Trim() : Title;

			// Display the title line
			if( titleStr.Length > 0 )
			{
				Console.WriteLine( titleStr );
			}

			// Display the copyright line
			AssemblyCopyrightAttribute copyAttr = GetAssemblyAttribute<AssemblyCopyrightAttribute>( assembly );
			if( copyAttr != null && copyAttr.Copyright.Length > 0 )
			{
				Console.WriteLine( copyAttr.Copyright );
			}

			Console.WriteLine();
		}

		#endregion

		#region Public Methods

		/// <summary>Start the console application.</summary>
		/// <returns><see langword="true"/> if the application has already been started.</returns>
		public bool StartApp()
		{
			// Check whether the application has already been started
			if( _started ) { return true; }
			_started = true;

			// Show the application start time and start the stopwatch
			SetStartTime( this );
			ResumeElapsed();

			return false;
		}

		/// <summary>Start the console application.</summary>
		/// <param name="args">Collection of command-line arguments.</param>
		public void StartApp( IReadOnlyList<string> args )
		{
			// Check whether the application has already been started
			if( StartApp() ) { return; }

			// Check for command line help request
			args = args ?? new List<string>();
			if( args.Count > 0 && args[0].Contains( "?" ) )
			{
				ShowProgramInfo();

				// Set the flag to indicate help has been requested
				HelpRequest = true;
			}
		}

		/// <summary>Output the program information to the Console.</summary>
		/// <remarks>This method can be overridden in a derived class to provide specific
		/// information about the program when the command line help request is made using
		/// ? anywhere in the first argument.</remarks>
		/// <exception cref="TypeLoadException">Thrown when type-loading failures occur.</exception>
		public virtual void ShowProgramInfo()
		{
			ShowProgramInfo( Assembly.GetEntryAssembly() );
		}

		/// <summary>Stops the application and sets the exit code.</summary>
		/// <param name="result">Execution result, <see langword="false"/> sets the exit code to 1.</param>
		public void StopApp( bool result = true )
		{
			// Check whether the application has already been stopped
			if( !_started ) { return; }
			_started = false;

			// Set the return code if it is not already set
			if( 0 == Environment.ExitCode )
			{
				Environment.ExitCode = result ? 0 : 1;
			}

			// Set the application stop variables
			SetEndTime( this, result, IsUnitTest );
		}

		/// <summary>Formats a title line to be logged.</summary>
		/// <param name="text">Program title.</param>
		/// <param name="maxWidth">Maximum line width, default is 80 characters.</param>
		/// <returns>String containing the maximum width title.</returns>
		public string FormatTitleLine( string text, int maxWidth = 80 )
		{
			text = text ?? string.Empty;
			if( text.Length >= maxWidth ) { return text; }

			// Center the title and pad width '-' characters
			int filler = ( maxWidth - text.Length ) / 2;
			string padding = new string( '-', filler );
			if( text.Length + filler * 2 != maxWidth ) { text += "-"; }

			return padding + text + padding;
		}

		/// <summary>Resumes measuring elapsed time for an interval.</summary>
		public void ResumeElapsed()
		{
			if( !_stopWatch.IsRunning ) { _stopWatch.Start(); }
		}

		/// <summary>Suspends measuring elapsed time for an interval.</summary>
		public void SuspendElapsed()
		{
			if( _stopWatch.IsRunning ) { _stopWatch.Stop(); }
		}

		#endregion
	}
}