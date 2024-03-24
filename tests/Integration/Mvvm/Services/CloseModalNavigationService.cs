namespace Sample.Mvvm.Services;

public class CloseModalNavigationService( ModalNavigationStore navigationStore ) : INavigationService
{
	private readonly ModalNavigationStore _navigationStore = navigationStore;

	public void Navigate()
	{
		_navigationStore.Close();
	}
}