namespace Sample.Mvvm.Commands;

public class LogoutCommand( AccountStore accountStore ) : CommandBase
{
	private readonly AccountStore _accountStore = accountStore;

	public override void Execute( object? parameter )
	{
		_accountStore.Logout();
	}
}