// Ignore Spelling: Alphanum

using System.Collections;
using System.Text;

namespace Configuration.Helper;

/// <summary>Exposes a method that compares two objects.</summary>
public class AlphanumComparator : IComparer
{
	#region Private Enumeration and Methods

	private enum ChunkType { Alphanumeric, Numeric };

	private static bool InChunk( char ch, char otherCh )
	{
		ChunkType type = ChunkType.Alphanumeric;

		if( char.IsDigit( otherCh ) ) { type = ChunkType.Numeric; }

		return ( type != ChunkType.Alphanumeric || !char.IsDigit( ch ) )
			&& ( type != ChunkType.Numeric || char.IsDigit( ch ) );
	}

	#endregion

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
		if( x is not string s1 || y is not string s2 ) { return 0; }

		int thisMarker = 0;
		int thatMarker = 0;

		while( ( thisMarker < s1.Length ) || ( thatMarker < s2.Length ) )
		{
			if( thisMarker >= s1.Length ) { return -1; }
			if( thatMarker >= s2.Length ) { return 1; }

			StringBuilder thisChunk = GetStringBuilder( ref s1, ref thisMarker );
			StringBuilder thatChunk = GetStringBuilder( ref s2, ref thatMarker );

			int result = 0;

			// If both chunks contain numeric characters, sort them numerically
			if( char.IsDigit( thisChunk[0] ) && char.IsDigit( thatChunk[0] ) )
			{
				int thisNumericChunk = Convert.ToInt32( thisChunk.ToString() );
				int thatNumericChunk = Convert.ToInt32( thatChunk.ToString() );

				if( thisNumericChunk < thatNumericChunk ) { result = -1; }
				else if( thisNumericChunk > thatNumericChunk ) { result = 1; }
			}
			else
			{
				result = string.Compare( thisChunk.ToString(), thatChunk.ToString(), StringComparison.Ordinal );
			}

			if( result != 0 ) { return result; }
		}

		return 0;
	}

	#endregion
}