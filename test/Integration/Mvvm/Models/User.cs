using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.Core.Classes;

namespace Sample.Mvvm.Models;

public class User : ModelBase, ICloneable
{
	#region Properties

	[MaxLength( 50 )]
	public string Name
	{
		get => ( _name is not null ) ? _name : string.Empty;
		set
		{
			if( value.Equals( _name ) ) return;
			_name = value;
			OnPropertyChanged();
		}
	}

	[MaxLength( 50 )]
	public string Email
	{
		get => _mail;
		set
		{
			if( value.Equals( _mail ) ) return;
			_mail = value;
			OnPropertyChanged();
		}
	}

	[JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
	public DateOnly? BirthDate
	{
		get => _dob;
		set
		{
			if( value is not null && value.Equals( _dob ) ) { return; }
			_dob = value == DateOnly.MinValue ? null : value;
			OnPropertyChanged();
			OnPropertyChanged( nameof( Age ) );
		}
	}

	[JsonIgnore()]
	public int? Age => CalculateAge( BirthDate );

	public TestTypes Tester
	{
		get => _tester;
		set
		{
			if( value.Equals( _tester ) ) { return; }
			_tester = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region Constructor

	private string _name;
	private string _mail;
	private DateOnly? _dob;
	private TestTypes _tester;

	public User()
	{
		_name = string.Empty;
		_mail = string.Empty;
	}

	#endregion

	public object Clone() => MemberwiseClone();

	public bool HasChanges( User source )
	{
		if( Name != source.Name ) { return true; }
		if( Email != source.Email ) { return true; }
		if( BirthDate != source.BirthDate ) { return true; }
		if( Tester != source.Tester ) { return true; }
		return false;
	}

	public void ApplyChanges( User source )
	{
		if( Name != source.Name ) { Name = source.Name; }
		if( Email != source.Email ) { Email = source.Email; }
		if( BirthDate != source.BirthDate ) { BirthDate = source.BirthDate; }
		if( Tester != source.Tester ) { Tester = source.Tester; }
	}
}