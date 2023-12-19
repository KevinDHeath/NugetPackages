using System.Windows.Input;
using Common.Core.Classes;

namespace TestWPFPackages.Core.Commands;

/// <summary>Initializes a new instance of the DelegateCommand class.</summary>
/// <param name="commandTask">Method that has a single parameter and does not return a value.</param>
/// <param name="canExecute">Method that defines a set of criteria and determines whether the
/// specified object meets those criteria.<br/>
/// If no value is passed the CanExecute method will always return true.</param>
public class RelayCommand( Action<object?> commandTask, Predicate<object?>? canExecute = null ) : ModelBase, ICommand
{
	#region For CanExecute test

	public int SubscriberCount { get { return _canExecuteChanged?.GetInvocationList().Length ?? 0; } }

	//public void RaiseCanExecuteChanged() => _canExecuteChanged?.Invoke( this, EventArgs.Empty );

#pragma warning disable IDE1006 // Naming Styles
	private event EventHandler? _canExecuteChanged;
#pragma warning restore IDE1006

	#endregion

	#region Constructor and Variables

	private readonly Action<object?> _commandTask = commandTask;
	private readonly Predicate<object?>? _canExecute = canExecute;

	#endregion

	#region ICommand Implementation

	/// <inheritdoc/>
	public bool CanExecute( object? parameter ) => _canExecute == null || _canExecute( parameter );

	/// <inheritdoc/>
	public void Execute( object? parameter ) => _commandTask.Invoke( parameter );

	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;

			// For CanExecute test
			_canExecuteChanged += value;
			OnPropertyChanged( nameof( SubscriberCount ) );
		}
		remove
		{
			CommandManager.RequerySuggested -= value;

			// For CanExecute test
			_canExecuteChanged -= value;
			OnPropertyChanged( nameof( SubscriberCount ) );
		}
	}

	#endregion
}