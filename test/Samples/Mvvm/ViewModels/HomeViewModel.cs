using System.Windows.Input;
using Sample.Mvvm.Commands;

namespace Sample.Mvvm.ViewModels;

public class HomeViewModel( INavigationService loginNavigationService ) : ViewModelBase
{
	public ICommand NavigateLoginCommand { get; } = new NavigateCommand( loginNavigationService );
}