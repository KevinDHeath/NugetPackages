namespace Sample.Mvvm.Services;

public class ParameterNavigationService<TParameter, TViewModel>( NavigationStore navigationStore,
	Func<TParameter, TViewModel> createViewModel ) where TViewModel : ViewModelBase
{
	private readonly NavigationStore _navigationStore = navigationStore;
	private readonly Func<TParameter, TViewModel> _createViewModel = createViewModel;

	public void Navigate( TParameter parameter )
	{
		_navigationStore.CurrentViewModel = _createViewModel( parameter );
	}
}