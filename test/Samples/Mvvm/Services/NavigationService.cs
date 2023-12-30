namespace Sample.Mvvm.Services;

public class NavigationService<TViewModel>( NavigationStore navigationStore,
	Func<TViewModel> createViewModel ) : INavigationService where TViewModel : ViewModelBase
{
	private readonly NavigationStore _navigationStore = navigationStore;
	private readonly Func<TViewModel> _createViewModel = createViewModel;

	public void Navigate()
	{
		_navigationStore.CurrentViewModel = _createViewModel();
	}
}