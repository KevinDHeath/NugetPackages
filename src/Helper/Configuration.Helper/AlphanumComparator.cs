// Ignore Spelling: Alphanum

using System.Collections;
using System.Text;

namespace Configuration.Helper;

/// <summary>Exposes a method that compares two objects.</summary>
public class AlphanumComparator : IComparer
{
	#region Private Enumeration and Methods

	private enum ChunkType { Alphanumeric, Numeric };

	private const int clt = -1;
	private const int ceq = 0;
	private const int cgt = 1;

	private static bool InChunk( char ch, char otherCh )
	{
		ChunkType type = ChunkType.Alphanumeric;

		if( char.IsDigit( otherCh ) ) { type = ChunkType.Numeric; }

		return ( type != ChunkType.Alphanumeric || !char.IsDigit( ch ) )
			&& ( type != ChunkType.Numeric || char.IsDigit( ch ) );
	}

	private static StringBuilder GetStringBuilder( ref string str, ref int marker )
	{
		char thisCh = str[marker];
		StringBuilder rtn = new();

		while( ( marker < str.Length ) && ( rtn.Length == 0 || InChunk( thisCh, rtn[0] ) ) )
		{
			rtn.Append( thisCh );
			marker++;

			if( marker < str.Length ) { thisCh = str[marker]; }
		}

		return rtn;
	}

	private static bool IsNumeric( char x, char y )
	{
		return char.IsDigit( x ) && char.IsDigit( y );
	}

	private static int SortNumeric( string x, string y )
	{
		int xNumber = Convert.ToInt32( x );
		int yNumber = Convert.ToInt32( y );

		if( xNumber < yNumber ) { return clt; }
		else if( xNumber > yNumber ) { return cgt; }
		return ceq;
	}

	private static bool MoreToCheck( int xPos, string s1, int yPos, string s2 )
	{
		return ( xPos < s1.Length ) || ( yPos < s2.Length );
	}

	#endregion

	#region IComparer Implementation

	/// <summary>
	/// Compares two objects and returns a value indicating whether one is less than,
	/// equal to, or greater than the other.
	/// </summary>
	/// <param name="x">The first object to compare.</param>
	/// <param name="y">The second object to compare.</param>
	/// <returns>A signed integer that indicates the relative values of x and y:
	/// - If less than 0, x is less than y.
	/// - If 0, x equals y.
	/// - If greater than 0, x is greater than y.
	/// </returns>
	public int Compare( object? x, object? y )
	{
		if( x is not string s1 || y is not string s2 ) { return ceq; }

		int xPosition = 0;
		int yPosition = 0;

		while( MoreToCheck( xPosition, s1, yPosition, s2 ) )
		{
			if( xPosition >= s1.Length ) { return clt; }
			if( yPosition >= s2.Length ) { return cgt; }

			StringBuilder xChunk = GetStringBuilder( ref s1, ref xPosition );
			StringBuilder yChunk = GetStringBuilder( ref s2, ref yPosition );

			string xStr = xChunk.ToString();
			string yStr = yChunk.ToString();

			// If both chunks contain numeric characters, sort them numerically
			int result = IsNumeric( xChunk[0], yChunk[0] )
				? SortNumeric( xStr, yStr )
				: string.Compare( xStr, yStr, StringComparison.Ordinal );

			if( result != ceq ) { return result; }
		}

		return ceq;
	}

	#endregion
}