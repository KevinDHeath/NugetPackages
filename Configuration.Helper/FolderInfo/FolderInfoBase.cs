namespace KevinDHeath.Configuration.Helper.FolderInfo;

/// <summary>Base class for Folder Information implementations.</summary>
public abstract class FolderInfoBase : IFolderInfo
{
	#region IFolderInfo Implementation

	#region Properties

	/// <inheritdoc />
	public string Location { get; protected set; }

	/// <inheritdoc />
	public virtual bool Exists { get; protected set; }

	/// <inheritdoc />
	public IOHelper.PathType Source { get; protected set; }

	/// <inheritdoc />
	public SortedList<string, string> FileList { get; }

	/// <inheritdoc />
	public SortedList<string, IFolderInfo> FolderList { get; }

	#endregion

	#region Methods

	/// <inheritdoc />
	public string[] SortFileList()
	{
		if( FileList.Count > 0 )
		{
			var retValue = FileList.Keys.ToArray();
			Array.Sort( retValue, new AlphanumComparator() );
			return retValue;
		}

		return Array.Empty<string>();
	}

	/// <inheritdoc />
	public string[] SortFolderList()
	{
		if( FolderList.Count > 0 )
		{
			var retValue = FolderList.Keys.ToArray();
			Array.Sort( retValue, new AlphanumComparator() );
			return retValue;
		}

		return Array.Empty<string>();
	}

	#endregion

	#endregion

	#region Default Constructor and Variables

	/// <summary>Disk folder name separator.</summary>
	protected static readonly string _separator = Path.DirectorySeparatorChar.ToString();

	/// <summary>Alternate folder name separator.</summary>
	protected static readonly string _separatorAlt = Path.AltDirectorySeparatorChar.ToString();

	/// <summary>Sorting comparer for the collections.</summary>
	private static readonly StringComparer _comparer = StringComparer.OrdinalIgnoreCase;

	/// <summary>Default constructor.</summary>
	protected FolderInfoBase()
	{
		Location = string.Empty;
		FileList = new SortedList<string, string>( _comparer );
		FolderList = new SortedList<string, IFolderInfo>( _comparer );
	}

	#endregion
}