using System;
using System.Windows.Input;

namespace Common.Wpf.Commands;

/// <summary>
/// A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
/// </summary>
public class RelayCommand : ICommand
{
	private readonly Action<object?> _execute;
	private readonly Predicate<object?> _canExecute;

	#region Constructor

	/// <summary>Initializes a new instance of the RelayCommand class.</summary>
	/// <param name="canExecute">Method that has a single parameter and does not return a value.</param>
	/// <param name="execute">Method that defines a set of criteria and determines whether the specified object meets those criteria.</param>
	public RelayCommand( Predicate<object?> canExecute, Action<object?> execute )
	{
		_canExecute = canExecute;
		_execute = execute;
	}

	#endregion

	#region ICommand Implementation

	/// <inheritdoc/>
	public bool CanExecute( object? parameter )
	{
		return _canExecute( parameter );
	}

	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged
	{
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}

	/// <inheritdoc/>
	public void Execute( object? parameter )
	{
		_execute( parameter );
	}

	#endregion
}