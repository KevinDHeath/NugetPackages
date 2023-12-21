using System;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using Common.Wpf.Controls.Classes;

namespace Common.Wpf.Controls;

#region Dialog Types

/// <summary>Dialog type enumeration for the FilePathTextBox control.</summary>
public enum DialogTypes
{
	/// <summary>Open file dialog.</summary>
	File,
	/// <summary>Folder browser dialog.</summary>
	Folder
}

#endregion

/// <summary>Control to implement the selection of a file or folder for a TextBox.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control specifying the type of dialog. The default is File:<br/>
/// <code language="XAML">&lt;cc:FilePathTextBox DialogType="Folder"... /&gt;</code>
/// </remarks>
public class FilePathTextBox : TextBox
{
	#region Properties

	/// <summary>Identifies the DialogType dependency property.</summary>
	public static readonly DependencyProperty DialogTypeProperty =
		DependencyProperty.Register( nameof( DialogType ), typeof( DialogTypes ), typeof( FilePathTextBox ),
			new PropertyMetadata( DialogTypes.File ) );

	/// <summary>Gets or sets the dialog type that will be shown to the user.<br/>
	/// The default is File.</summary>
	/// <remarks>File names must have an extension.</remarks>
	public DialogTypes DialogType
	{
		get { return (DialogTypes)GetValue( DialogTypeProperty ); }
		set { SetValue( DialogTypeProperty, value ); }
	}

	/// <summary>Identifies the RelativePath dependency property.</summary>
	public static readonly DependencyProperty RelativePathProperty =
		DependencyProperty.Register( nameof( RelativePath ), typeof( string ), typeof( FilePathTextBox ),
			new PropertyMetadata( string.Empty ) );

	/// <summary>Gets or sets a relative path to a file.</summary>
	public string RelativePath
	{
		get { return (string)GetValue( RelativePathProperty ); }
		set { SetValue( RelativePathProperty, value ); }
	}

	/// <summary>Identifies the IsErrorShown dependency property.</summary>
	public readonly static DependencyProperty IsErrorShownProperty = DependencyProperty.Register(
		name: nameof( IsErrorShown ), propertyType: typeof( bool ), ownerType: typeof( FilePathTextBox ),
		typeMetadata: new PropertyMetadata( defaultValue: false ) );

	/// <summary>Gets or sets whether error messages are shown to the user below the control.
	/// The default is false.</summary>
	public bool IsErrorShown
	{
		get { return (bool)GetValue( IsErrorShownProperty ); }
		set
		{
			SetValue( IsErrorShownProperty, value );
		}
	}

	#endregion

	#region Constructors and Variables

	private Button? _btnOpen = null;

	static FilePathTextBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( FilePathTextBox ),
			new FrameworkPropertyMetadata( typeof( FilePathTextBox ) ) );
	}

	#endregion

	#region Overridden Methods

	/// <summary>When overridden in a derived class, this is invoked whenever application code
	/// or internal processes call ApplyTemplate().</summary>
	public override void OnApplyTemplate()
	{
		_btnOpen = GetTemplateChild( "btnOpen" ) as Button;
		if( _btnOpen is not null )
		{
			_btnOpen.Click += BtnOpen_OnClick;
		}

		base.OnApplyTemplate();
	}

	#endregion

	#region Private Methods

	private void BtnOpen_OnClick( object sender, RoutedEventArgs e )
	{
		string? result;
		string folder;
		bool relative = RelativePath.Length > 0;
		string fullName = relative ? Path.Combine( RelativePath, Text ) : Text;
		fullName = fullName.Trim();

		switch( DialogType )
		{
			case DialogTypes.Folder:
				if( TryGetFullPath( fullName, out folder ) )
				{
					result = Win32Dialogs.ShowSelectFolder( folder );
					if( !string.IsNullOrWhiteSpace( result ) )
					{
						if( relative ) { result = GetRelativePath( result ); }
						if( result != Text ) { Text = result; }
					}
				}
				break;

			case DialogTypes.File:
				bool isExe = Path.GetExtension( Text )?.ToLower() == @".exe";
				if( string.IsNullOrWhiteSpace( Text ) & relative ) { fullName += "\\"; }

				if( TryGetFile( fullName, out folder, out string file ) )
				{
					result = Win32Dialogs.ShowFileOpen( folder, file, full:folder.Length > 0, exe:isExe );
					if( !string.IsNullOrWhiteSpace( result ) )
					{
						if( relative ) { result = GetRelativePath( result ); }
						if( result != Text ) { Text = result; }
					}
				}
				break;
		}
	}

	private string GetRelativePath( string result )
	{
		return result.Replace( RelativePath + "\\", string.Empty );
	}

	private static bool TryGetFullPath( string path, out string outPath )
	{
		outPath = string.Empty;
		if( string.IsNullOrWhiteSpace( path ) ) { return true; }

		bool retValue = false;
		try
		{
			if( path.IndexOfAny( Path.GetInvalidPathChars() ) < 0 )
			{
				if( Uri.TryCreate( path, UriKind.Absolute, out Uri? uri ) )
				{
					if( uri.IsFile )
					{
						outPath = uri.LocalPath;
						retValue = true;
					}
				}
			}
		}
		catch( ArgumentException ) { }
		catch( SecurityException ) { }
		catch( NotSupportedException ) { }
		catch( PathTooLongException ) { }

		return retValue;
	}

	private static bool TryGetFile( string file, out string outPath, out string outFile )
	{
		outPath = string.Empty;
		outFile = string.Empty;
		if( string.IsNullOrWhiteSpace( file ) ) { return true; }

		bool retValue = false;
		try
		{
			string? wrk;
			// Try getting the path
			if( Path.IsPathRooted( file ) )
			{
				wrk = Path.GetDirectoryName( file );
				if( wrk is not null && TryGetFullPath( wrk, out string path ) ) { outPath = path; }
			}

			// Get the file name
			wrk = Path.GetFileName( file );
			if( wrk is not null )
			{
				if( wrk.IndexOfAny( Path.GetInvalidFileNameChars() ) < 0 )
				{
					if( Uri.TryCreate( wrk, UriKind.Absolute, out Uri? uri ) )
					{
						if( uri.IsFile )
						{
							outFile = uri.LocalPath;
							retValue = true;
						}
					}
				}

				outFile = wrk;
				retValue = true;
            }
		}
		catch( ArgumentException ) { }
		catch( SecurityException ) { }
		catch( NotSupportedException ) { }
		catch( PathTooLongException ) { }

		return retValue;
	}

	#endregion
}