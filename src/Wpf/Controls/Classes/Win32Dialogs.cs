// Ignore Spelling: exe

using Microsoft.Win32;
using System.ComponentModel;

namespace Common.Wpf.Controls.Classes;

/// <summary>Common Windows Dialogs</summary>
// Dialog boxes overview (WPF .NET)
// https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/dialog-boxes-overview
[EditorBrowsable( EditorBrowsableState.Never )]
public static class Win32Dialogs
{
	#region Windows Folder Browser Dialog
	// https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.openfolderdialog

	/// <summary>Displays a standard dialog box that prompts the user to select a folder.</summary>
	/// <param name="folder">Initial directory.</param>
	/// <returns>The selected folder path. If nothing is selected an empty string is returned.</returns>
	public static string ShowSelectFolder( string? folder )
	{
		if( folder is null ) { return string.Empty; }

		// Show folder browser dialog
		var dlg = new OpenFolderDialog
		{
			InitialDirectory = folder
		};

		// Return folder browser dialog box results
		if( dlg.ShowDialog() == true )
		{
			return dlg.FolderName;
		}

		return string.Empty;
	}

	#endregion

	#region Windows File Open Dialog
	// https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog

	private const string cFilter = @"All files (*.*)|*.*|Json files (*.json)|*.json|Xml files (*.xml)|*.xml";
	private const string cExeFilter = @"Executables (*.exe)|*.exe|All files (*.*)|*.*";

	/// <summary>Displays a standard dialog box that prompts the user to open a file.</summary>
	/// <param name="folder">Initial directory.</param>
	/// <param name="file">Initial filename.</param>
	/// <param name="full">True to indicate a full path and file name should be returned.</param>
	/// <param name="exists">True to indicate the file must exist.</param>
	/// <param name="exe">True to indicate an executable is being selected.</param>
	/// <returns>The selected file name. If nothing is selected an empty string is returned.</returns>
	public static string? ShowFileOpen( string? folder = "", string? file = "", bool full = true,
		bool exists = true, bool exe = false )
	{
		if( folder is null || file is null ) { return string.Empty; }

		// Show open file dialog
		var dlg = new OpenFileDialog
		{
			Title = $"Select File",
			InitialDirectory = folder,
			FileName = file,
			DefaultExt = System.IO.Path.GetExtension( file ),
			Filter = exe ? cExeFilter : cFilter,
			CheckPathExists = true,
			CheckFileExists = exists
		};

		// Return open file dialog box results
		var rtn = string.Empty;
        if( dlg.ShowDialog() == true )
        {
			rtn = full ? dlg.FileName : dlg.SafeFileName;
		}

        return rtn;
	}

	#endregion

	#region Windows File Save Dialog
	// https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.savefiledialog

	/// <summary>Displays a standard dialog box that prompts the user to save a file.</summary>
	/// <param name="folder">Initial directory.</param>
	/// <param name="file">Initial filename.</param>
	/// <param name="full">True to indicate a full path and file name should be returned.</param>
	/// <param name="exists">True to indicate the file must exist.</param>
	/// <returns>The selected file name. If nothing is selected an empty string is returned.</returns>
	public static string? ShowFileSave( string? folder = "", string? file = "", bool full = true,
		bool exists = true )
	{
		if( folder is null || file is null ) { return string.Empty; }

		// Show save file dialog
		var dlg = new SaveFileDialog
		{
			Title = $"Select File",
			InitialDirectory = folder,
			FileName = file,
			DefaultExt = System.IO.Path.GetExtension( file ),
			Filter = cFilter,
			CheckPathExists = true,
			CheckFileExists = exists
		};

		// Return save file dialog box results
		var rtn = string.Empty;
		if( dlg.ShowDialog() == true )
		{
			rtn = full ? dlg.FileName : System.IO.Path.GetFileName( dlg.FileName );
		}

		return rtn;
	}

	#endregion
}