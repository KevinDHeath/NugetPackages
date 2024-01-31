namespace Sample.Mvvm.ViewModels;

public sealed class CustomTestViewModel : ViewModelBase
{
	public string Name { get; set; }

	public CustomTestViewModel( AccountStore accountStore )
	{
		string name = accountStore.CurrentAccount?.User is not null ?
			accountStore.CurrentAccount.User : "World";

		Name = $"Hello {name}! ";
	}
}