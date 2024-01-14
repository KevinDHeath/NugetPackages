namespace Sample.Mvvm.Models;

public class Account
{
	public string? Email { get; set; }

	public string? User { get; set; }

	public User Login { get; set; } = new User();
}