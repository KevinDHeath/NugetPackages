using System.Security;

namespace Core.Tests;

public class ComplexClass
{
	// Strings
	public SecureString Secure { get; set; }

	public string String;

	// Collections
	public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
	public SortedDictionary<string, User?> Users { get; set; } = new SortedDictionary<string, User?>();

	public IList<string?> Keys { get; set; } = new List<string?>();

	public int Count { get; set; }

	public int[] Array = [1, 2, 3];

	// Fields
	public DateOnly DateOnly = new( 2000, 1, 1 );
	public decimal Decimal = 123.45M;
	public double Double = 123.4567D;
	public float Float = 123.456789F;
	public int Integer = 1;
	public User User = new();

	public ComplexClass()
	{
		Secure = new System.Net.NetworkCredential( "", "password" ).SecurePassword;
		String = "ABC";
		Headers.Add( @"foo", "bar" );
		Users.Add( "A", new User() );
		Users.Add( "B", null );
		Keys.Add( "key" );
		Keys.Add( "null" );
		Count = 1;
	}
}