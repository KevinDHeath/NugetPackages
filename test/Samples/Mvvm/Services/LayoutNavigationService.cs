namespace Sample.Mvvm.Services;

public class LayoutNavigationService<TViewModel>( NavigationStore navigationStore,
	Func<TViewModel> createViewModel,
	Func<NavigationBarViewModel> createNavigationBarViewModel ) : INavigationService where TViewModel : ViewModelBase
{
	private readonly NavigationStore _navigationStore = navigationStore;
	private readonly Func<TViewModel> _createViewModel = createViewModel;
	private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel = createNavigationBarViewModel;

	public void Navigate()
	{
		_navigationStore.CurrentViewModel = new LayoutViewModel( _createNavigationBarViewModel(), _createViewModel() );
	}
}