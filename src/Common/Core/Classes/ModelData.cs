using System.ComponentModel.DataAnnotations;

namespace Common.Core.Classes;

/// <summary>Base class for models with an integer as the primary key.</summary>
public abstract class ModelData : ModelBase
{
	/// <summary>Gets or sets the primary key.</summary>
	[Key]
	[Range( 0, 2147483647 )]
	public abstract int Id { get; set; }

	#region Internal Methods

	internal static void SetSQLColumn( string colName, object? newValue, IList<string> sql )
	{
		string quote = string.Empty;
		if( newValue is not null and string str )
		{
			quote = "'";
			if( str.Contains( '\'' ) ) { newValue = str.Replace( "'", "''" ); } // Check for "'" in string
		}
		if( newValue is not null and char chr )
		{
			quote = "'";
			if( chr == '\'' ) { newValue = "''"; } // Check for "'" in char
		}

		newValue = newValue is not null ? $"{quote}{newValue}{quote}" : "NULL";
		sql.Add( $"[{colName}]={newValue}" );
	}

	#endregion
}