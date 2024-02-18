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
		const char cCharQuote = '\'';
		const string cStrQuote = "'";
		const string cDblQuote = "''";

		string quote = string.Empty;
		bool hasNewValue = newValue is not null;

		if( hasNewValue )
		{
			if( newValue is string str )
			{
				quote = cStrQuote;
				if( str.Contains( cCharQuote ) ) { newValue = str.Replace( cStrQuote, cDblQuote ); }
			}
			else if( newValue is char chr )
			{
				quote = cStrQuote;
				if( chr == cCharQuote ) { newValue = cDblQuote; }
			}
		}

		newValue = hasNewValue ? $"{quote}{newValue}{quote}" : "NULL";
		sql.Add( $"[{colName}]={newValue}" );
	}

	#endregion
}