using System.ComponentModel.DataAnnotations;

namespace Sample.Mvvm.Validations;

internal class UserEmailAttribute : ValidationAttribute
{
	/// <inheritdoc/>
	protected override ValidationResult? IsValid( object? value, ValidationContext context )
	{
		if( context.ObjectInstance is not null && context.MemberName is not null &&
			value is not null && value is string val )
		{
			if( val.Length > 0 )
			{
				if( context.ObjectInstance is UserViewModel uvm )
				{
					if( uvm._isNew && uvm._userStore is not null && uvm._userStore.DoesEmailExist( val ) )
					{
						return new( "Email not allowed.", new string[] { context.MemberName } );
					}
				}

			}
		}

		return ValidationResult.Success;
	}
}