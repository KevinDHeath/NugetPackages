namespace Sample.Mvvm.Commands;

public class NavigateCommand( INavigationService navigationService ) : CommandBase
{
	private readonly INavigationService _navigationService = navigationService;

	public override void Execute( object? parameter )
	{
		_navigationService.Navigate();
	}
}