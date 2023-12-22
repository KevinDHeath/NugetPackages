using System.Text.Json.Serialization;

namespace TestWPF.Core.Models;

public class Enumerations
{
	public static IEnumerable<TestTypes> GetTestTypes()
	{
		TestTypes[] values = (TestTypes[])Enum.GetValues( typeof( TestTypes ) );
		return values.OrderBy( v => v.ToString() );
	}
}

[JsonConverter( typeof( JsonStringEnumConverter<TestTypes> ) )]
public enum TestTypes
{
	Unit,
	Functional,
	Integration,
	Regression,
	Acceptance,
	Globalization,
	Compatibility
};