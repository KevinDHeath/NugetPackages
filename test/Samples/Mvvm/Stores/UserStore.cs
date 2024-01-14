using Sample.Mvvm.Models;

namespace Sample.Mvvm.Stores;

public class UserStore( SettingsStore settingsStore )
{
	private readonly List<User> _users = settingsStore.Settings.Users;

	internal IList<User> Users { get { return _users; } }

	internal User? CurrentUser {  get; set; }

	public event Action<User>? UserAdded;

	public void AddUser( User user )
	{
		CurrentUser = user;
		_users.Add( user );
		UserAdded?.Invoke( user );
	}

	internal bool DoesEmailExist( string email ) => Users.Any( x => x.Email is not null &&
		x.Email.Equals( email, StringComparison.OrdinalIgnoreCase ) );
}