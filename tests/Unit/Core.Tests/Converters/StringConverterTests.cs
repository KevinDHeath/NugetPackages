namespace Core.Tests.Converters;

public class StringConverterTests
{
	[Theory]
	[InlineData( "", false )]
	[InlineData( "0", false )]
	[InlineData( "N", false )]
	[InlineData( "NO", false )]
	[InlineData( "1", true )]
	[InlineData( "Y", true )]
	[InlineData( "Yes", true )]
	public void Bool( string value, bool result )
	{
		// Act
		_ = StringConverter.TryParse( ref value, out bool rtn );

		// Assert
		_ = rtn.Should().Be( result );
	}

	[Fact]
	public void TryParse_bool_should_be_true()
	{
		// Arrange
		string val = "YES";

		// Act
		bool result = StringConverter.TryParse( ref val, out bool _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_byte_should_be_true()
	{
		// Arrange
		string val = "100";

		// Act
		bool result = StringConverter.TryParse( ref val, out byte _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_DateOnly_should_be_true()
	{
		// Arrange
		string val = "2002-01-24";

		// Act
		bool result = StringConverter.TryParse( ref val, out DateOnly _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_DateTime_should_be_true()
	{
		// Arrange
		string val = "2002-01-24T04:00:00";

		// Act
		bool result = StringConverter.TryParse( ref val, out DateTime _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_DateTimeOffset_should_be_true()
	{
		// Arrange
		string val = "2002-01-24T04:00:00";

		// Act
		bool result = StringConverter.TryParse( ref val, out DateTimeOffset _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_decimal_should_be_true()
	{
		// Arrange
		string val = "-1";

		// Act
		bool result = StringConverter.TryParse( ref val, out decimal _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_double_should_be_false()
	{
		// Arrange
		string? val = string.Empty;

		// Act
		bool result = StringConverter.TryParse( ref val, out double _ );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void TryParse_double_should_be_true()
	{
		// Arrange
		string val = "-1";

		// Act
		bool result = StringConverter.TryParse( ref val, out double _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_float_should_be_false()
	{
		// Arrange
		string? val = string.Empty;

		// Act
		bool result = StringConverter.TryParse( ref val, out float _ );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void TryParse_float_should_be_true()
	{
		// Arrange
		string val = "-1";

		// Act
		bool result = StringConverter.TryParse( ref val, out float _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_Guid_should_be_false()
	{
		// Arrange
		string? val = string.Empty;

		// Act
		bool result = StringConverter.TryParse( ref val, out Guid _ );

		// Assert
		_ = result.Should().BeFalse();
	}

	[Fact]
	public void TryParse_Guid_should_be_true()
	{
		// Arrange
		string val = "106c9880-5af7-4166-b5e2-1acbefbb927d";

		// Act
		bool result = StringConverter.TryParse( ref val, out Guid _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_int_should_be_true()
	{
		// Arrange
		string val = "-1";

		// Act
		bool result = StringConverter.TryParse( ref val, out int _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_long_should_be_true()
	{
		// Arrange
		string val = "-1";

		// Act
		bool result = StringConverter.TryParse( ref val, out long _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_sbyte_should_be_true()
	{
		// Arrange
		string val = "12";

		// Act
		bool result = StringConverter.TryParse( ref val, out sbyte _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_short_should_be_true()
	{
		// Arrange
		string val = "12";

		// Act
		bool result = StringConverter.TryParse( ref val, out short _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_TimeOnly_should_be_true()
	{
		// Arrange
		string val = "01:00:00";

		// Act
		bool result = StringConverter.TryParse( ref val, out TimeOnly _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_TimeSpan_should_be_true()
	{
		// Arrange
		string val = "01:00:00-";

		// Act
		bool result = StringConverter.TryParse( ref val, out TimeSpan _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_uint_should_be_true()
	{
		// Arrange
		string val = "1";

		// Act
		bool result = StringConverter.TryParse( ref val, out uint _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_ulong_should_be_true()
	{
		// Arrange
		string val = "1";

		// Act
		bool result = StringConverter.TryParse( ref val, out ulong _ );

		// Assert
		_ = result.Should().BeTrue();
	}

	[Fact]
	public void TryParse_ushort_should_be_true()
	{
		// Arrange
		string val = "1";

		// Act
		bool result = StringConverter.TryParse( ref val, out ushort _ );

		// Assert
		_ = result.Should().BeTrue();
	}
}