namespace Sample.Mvvm.ViewModels;

public class LayoutViewModel( NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel ) : ViewModelBase
{
	public NavigationBarViewModel NavigationBarViewModel  => _navigationBarViewModel;

	public ViewModelBase ContentViewModel => _contentViewModel;

	private readonly NavigationBarViewModel _navigationBarViewModel = navigationBarViewModel;
	private readonly ViewModelBase _contentViewModel = contentViewModel;

	public override void Dispose()
	{
		NavigationBarViewModel.Dispose();
		ContentViewModel.Dispose();

		base.Dispose();
	}
}