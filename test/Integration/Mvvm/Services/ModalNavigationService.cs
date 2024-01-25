namespace Sample.Mvvm.Services;

public class ModalNavigationService<TViewModel>( ModalNavigationStore navigationStore,
	Func<TViewModel> createViewModel ) : INavigationService
	where TViewModel : ViewModelBase
{
	private readonly ModalNavigationStore _navigationStore = navigationStore;
	private readonly Func<TViewModel> _createViewModel = createViewModel;

	public void Navigate()
	{
		_navigationStore.CurrentViewModel = _createViewModel();
	}
}