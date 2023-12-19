using System.ComponentModel.DataAnnotations;
using Common.Core.Classes;

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
			_name = value;
			ValidateProperty( value );
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
			_age = value;
			ValidateProperty( value );
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
			_mail = value;
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
			ValidateProperty( value );
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

	#region Public Methods

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

	#endregion

	#region Populate Lists

	internal static IList<User> GetUsers()
	{
		var users = new List<User>();

		var user = new User() { Name = "Donna Doe", Age = 13, Email = "donna.doe@gmail.com", BirthDate = new DateOnly( 2010, 1, 5 ), Gender = Genders.Female };
		AddToList( user, users );
		user = new User() { Name = "Jane Doe", Age = 39, Email = "jane@doe-family.com", BirthDate = new DateOnly( 1984, 7, 5 ), Gender = Genders.Female };
		AddToList( user, users );
		user = new User() { Name = "John Doe", Age = 42, Email = "john@doe-family.com", BirthDate = new DateOnly( 1981, 8, 5 ), Gender = Genders.Male };
		AddToList( user, users );
		user = new User() { Name = "Sammy Doe", Age = 7, Email = "sammy.doe@gmail.com", BirthDate = new DateOnly( 2016, 1, 5 ), Gender = Genders.Male };
		AddToList( user, users );

		return users;
	}

	private static void AddToList( User user, List<User> users )
	{
		user.Age = CalculateAge( user.BirthDate );
		users.Add( user );
	}

	internal static async Task<IList<User>> GetUsersAsync()
	{
		IList<User> rtn = new List<User>();
		await Task.Run( () => { rtn = GetUsers(); } );
		return rtn;
	}

	internal static IEnumerable<Genders> GetGenders()
	{
		Genders[] values = (Genders[])Enum.GetValues( typeof( Genders ) );
		return values.OrderBy( v => v.ToString() );
	}

	#endregion
}