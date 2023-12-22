using System.ComponentModel.DataAnnotations;
using Common.Core.Classes;
using Common.Core.Models;

namespace TestWPF.Core.Models;

public class User : ModelDataError, ICloneable
{
	#region Properties

	private string? _name;
	[Required( ErrorMessage = "{0} cannot be empty." )]
	[StringLength( 50, ErrorMessage = "{0} cannot be greater than 50 chars." )]
	public string Name
	{
		get => ( _name is not null ) ? _name : string.Empty;
		set
		{
			if( value.Equals( _name ) ) return;
			if( ValidateProperty( value ) ) { _name = value; }
			OnPropertyChanged();
		}
	}

	private int? _age;
	[Required( ErrorMessage = "{0} cannot be empty." )]
	[Range( 0, 140, ErrorMessage = "{0} must be between {1} and {2}" )]
	public int? Age
	{
		get => _age;
		set
		{
			if( value is not null && value.Equals( _age ) ) return;
			if( ValidateProperty( value ) ) { _age = value; }
			OnPropertyChanged();
		}
	}

	private string? _mail;
	public string? Email
	{
		get => _mail;
		set
		{
			if( value is not null && value.Equals( _mail ) ) return;
			if( ValidateProperty( value ) ) { _mail = value; };
			OnPropertyChanged();
		}
	}

	private DateOnly? _dob;
	[Display( Name = "Date of Birth" )]
	[Required( ErrorMessage = "{0} cannot be empty." )]
	public DateOnly? BirthDate
	{
		get => _dob;
		set
		{
			if( value is not null && value.Equals( _dob ) ) return;
			_dob = value == DateOnly.MinValue ? null : value;
			if( ValidateProperty( value ) )
			{
				int? age = CalculateAge( value );
				if( age is not null ) { Age = age; }
			}
			OnPropertyChanged();
		}
	}

	private Genders _gender;
	public Genders Gender
	{
		get => _gender;
		set
		{
			if( value.Equals( _gender ) ) return;
			_gender = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Constructor

	public User()
	{
		Name = string.Empty;
		Age = null;
		BirthDate = null;
	}

	#endregion

	public object Clone()
	{
		return MemberwiseClone();
	}

	public void ApplyChanges( User source )
	{
		if( Name != source.Name ) { Name = source.Name; }
		if( Age != source.Age ) { Age = source.Age; }
		if( Email != source.Email ) { Email = source.Email; }
		if( BirthDate != source.BirthDate ) { BirthDate = source.BirthDate; }
		if( Gender != source.Gender ) {  Gender = source.Gender; }
	}

	internal static IEnumerable<Genders> GetGenders()
	{
		Genders[] values = (Genders[])Enum.GetValues( typeof( Genders ) );
		return values.OrderBy( v => v.ToString() );
	}

	internal static IList<User> GetUsers()
	{
		return new List<User>
		{
			new() { Name = "Donna Doe", Email = "donna.doe@gmail.com", BirthDate = new DateOnly( 2010, 1, 5 ), Gender = Genders.Female },
			new() { Name = "Jane Doe", Email = "jane@doe-family.com", BirthDate = new DateOnly( 1984, 7, 5 ), Gender = Genders.Female },
			new() { Name = "John Doe", Email = "john@doe-family.com", BirthDate = new DateOnly( 1981, 8, 5 ), Gender = Genders.Male },
			new() { Name = "Sammy Doe", Email = "sammy.doe@gmail.com", BirthDate = new DateOnly( 2016, 1, 5 ), Gender = Genders.Male }
		};
	}
}