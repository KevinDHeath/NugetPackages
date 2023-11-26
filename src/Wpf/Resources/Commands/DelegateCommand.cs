using System.Windows.Input;

namespace Common.Wpf.Commands;

/// <summary>Command to relay functionality to other objects by invoking delegates.</summary>
public class DelegateCommand : ICommand
{
	private readonly Action<object?> _execute;
	private readonly Predicate<object?>? _canExecute;

	#region Constructor

	/// <summary>Initializes a new instance of the DelegateCommand class.</summary>
	/// <param name="execute">Method that has a single parameter and does not return a value.</param>
	/// <param name="canExecute">Method that defines a set of criteria and determines whether the
	/// specified object meets those criteria.<br/>
	/// If no value is passed the CanExecute method will always return true.</param>
	public DelegateCommand( Action<object?> execute, Predicate<object?>? canExecute = null )
	{
		_execute = execute;
		_canExecute = canExecute;

		// https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager.requerysuggested
		CommandManager.RequerySuggested += CommandManager_RequerySuggested;
	}

	#endregion

	#region ICommand Implementation

	/// <inheritdoc/>
	public bool CanExecute( object? parameter ) => _canExecute == null || _canExecute( parameter );

	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged;

	/// <inheritdoc/>
	public void Execute( object? parameter ) => _execute( parameter );

	#endregion

	/// <summary>Occurs when conditions that might change the ability of a command to execute.</summary>
	public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke( this, EventArgs.Empty );

	private void CommandManager_RequerySuggested( object? sender, EventArgs e ) => RaiseCanExecuteChanged();
}