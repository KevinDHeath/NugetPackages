using System.ComponentModel.DataAnnotations;

namespace Core.Tests.Classes;

public class ModelDataErrorTests
{
	private readonly ModelDataError _validator;

	[Required( ErrorMessage = "Name cannot be empty." )]
	[Display( Name = "Full Name" )]
	public string Name { get; set; }

	[Required( ErrorMessage = "Location cannot be empty." )]
	public string Location { get; set; }

	public ModelDataErrorTests()
	{
		_validator = new ModelDataError( this );
		Name = string.Empty;
		Location = string.Empty;
	}

	[Fact]
	public void HasErrors_should_be_false()
	{
		// Arrange
		Name = "X";
		Location = "X";

		// Act
		_validator.ValidateAllProperties();

		// Assert
		_ = _validator.HasErrors.Should().BeFalse();
	}

	[Fact]
	public void HasErrors_should_be_true()
	{
		// Arrange
		Name = string.Empty;
		Location = string.Empty;

		// Act
		_validator.ValidateAllProperties();
		_ = _validator.GetErrors( null );
		_ = _validator.GetErrors( nameof( Location ) );
		_ = _validator.GetErrors( "X" );

		// Assert
		_ = _validator.HasErrors.Should().BeTrue();
	}

	[Fact]
	public void ClearErrors_should_be_true()
	{
		// Arrange
		Name = string.Empty;
		Location = string.Empty;

		// Act
		_validator.ValidateAllProperties();
		bool errors = _validator.HasErrors;
		_validator.ClearErrors( null );

		// Assert
		_ = errors.Should().BeTrue();
	}

	[Fact]
	public void ValidateProperty_should_be_false()
	{
		// Arrange
		TestError testError = new();
		DateTime value = DateTime.MinValue;

		// Act
		bool result = testError.ValidateProperty( value, nameof( TestError.Created ) );

		// Assert
		_ = result.Should().BeFalse();
	}
}

public class TestError : ModelDataError
{
	[MaxLength( 10 )]
	public DateTime Created { get; set; } = DateTime.Now;
}