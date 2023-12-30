namespace Sample.Mvvm.ViewModels;

public class PersonViewModel( string name ) : ViewModelBase
{
	public string Name { get; } = name;
}