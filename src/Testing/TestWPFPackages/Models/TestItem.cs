using Common.Core.Classes;

namespace TestWPFPackages.Core.Models;

public class TestItem : ModelBase
{
	#region Properties

	private string? _name;
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

	#endregion
}