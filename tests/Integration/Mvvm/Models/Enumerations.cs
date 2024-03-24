using System.Text.Json.Serialization;

namespace Sample.Mvvm.Models;

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
	Unit = 0,
	Functional,
	Integration,
	Regression,
	Acceptance,
	Globalization,
	Compatibility
};