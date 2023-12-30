using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Common.Wpf.Commands;

/// <summary>Command to relay functionality to other objects by invoking delegates.</summary>
public class DelegateCommand : INotifyPropertyChanged, ICommand
{
	/// <summary>Gets the count of subscribers.</summary>
	public int SubscriberCount { get { return _canExecuteChanged?.GetInvocationList().Length ?? 0; } }

#pragma warning disable IDE1006 // Naming Styles
	private event EventHandler? _canExecuteChanged;
#pragma warning restore IDE1006
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
	}

	#endregion

	#region INotifyPropertyChanged Implementation

	/// <summary>Method that will handle the PropertyChanged event raised when a property is
	/// changed on a component.</summary>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>Invoked whenever the effective value has been updated.</summary>
	/// <param name="property">Name of the property that has changed.</param>
	public void OnPropertyChanged( [CallerMemberName] string property = "" )
	{
		PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( property ) );
	}

	#endregion

	#region ICommand Implementation

	/// <inheritdoc/>
	public bool CanExecute( object? parameter ) => _canExecute == null || _canExecute( parameter );

	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;
			_canExecuteChanged += value;
			OnPropertyChanged( nameof( SubscriberCount ) );
		}
		remove
		{
			CommandManager.RequerySuggested -= value;
			_canExecuteChanged -= value;
			OnPropertyChanged( nameof( SubscriberCount ) );
		}
	}

	/// <inheritdoc/>
	public void Execute( object? parameter ) => _execute.Invoke( parameter );

	#endregion
}