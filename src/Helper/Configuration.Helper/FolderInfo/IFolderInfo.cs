namespace Configuration.Helper;

/// <summary>Interface for a FolderInfo implementation class.</summary>
public interface IFolderInfo
{
	#region Properties

	/// <summary>Gets the relative location of the folder to the root.</summary>
	string Location { get; }

	/// <summary>Indicates whether the folder exists.</summary>
	bool Exists { get; }

	/// <summary>Gets the folder information source.</summary>
	IOHelper.PathType Source { get;  }

	/// <summary>Gets a collection of full path and file names in the folder, keyed by file name.</summary>
	SortedList<string, string> FileList { get; }

	/// <summary>Gets a collections of sub-folders in the folder, keyed by folder name.</summary>
	SortedList<string, IFolderInfo> FolderList { get; }

	#endregion

	#region Methods

	/// <summary>Sorts the file list using an alphanumeric comparer.</summary>
	/// <returns>Array of file list key values.</returns>
	string[] SortFileList();

	/// <summary>Sorts the folder list using an alphanumeric comparer.</summary>
	/// <returns>Array of folder list key values.</returns>
	string[] SortFolderList();

	#endregion
}