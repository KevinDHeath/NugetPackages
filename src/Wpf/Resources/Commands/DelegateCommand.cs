using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

// I hate RelayCommand's CommandManager.RequerySuggested and maybe you should too
// https://jebarson.dev/2021/05/07/i-hate-relaycommands-commandmanager-requerysuggested-and-may-be-you-should-too

namespace Common.Wpf.Commands;

/// <summary>Command to relay functionality to other objects by invoking delegates.</summary>
public class DelegateCommand : INotifyPropertyChanged, ICommand, IDisposable
{
	/// <summary>Gets the count of subscribers.</summary>
	public int SubscriberCount { get { return Subscribers?.GetInvocationList().Length ?? 0; } }

#pragma warning disable IDE1006 // Naming Styles
	private event EventHandler? _canExecuteChanged;
#pragma warning restore IDE1006
	private readonly Action<object?> _execute;
	private readonly Predicate<object?>? _canExecute;
	private INotifyPropertyChanged? _listenObject;
	private readonly IEnumerable<string>? _listenProperties;
	private event EventHandler? Subscribers;

	#region Constructors

	/// <summary>Initializes a new instance of the DelegateCommand class for a specific object .</summary>
	/// <param name="execute">Method that has a single parameter and does not return a value.</param>
	/// <param name="canExecute">Method that defines a set of criteria and determines whether the
	/// specified object meets those criteria.<br/>
	/// If <see langword="null"/> is passed the CanExecute method will always return true.</param>
	/// <param name="listenObject">The object to listen for.</param>
	/// <param name="listenProperties">The properties to listen for. <see langword="null"/> for all properties.</param>
	public DelegateCommand( Action<object?> execute, Predicate<object?>? canExecute,
		INotifyPropertyChanged? listenObject, IEnumerable<string>? listenProperties = null )
	{
		_execute = execute;
		_canExecute = canExecute;
		_listenObject = listenObject;

		if( _listenObject is not null )
		{
			_listenObject.PropertyChanged += ObjectPropertyChanged;
			_listenProperties = listenProperties;
		}
	}

	/// <summary>Initializes a new instance of the DelegateCommand class using an Action and Predicate.</summary>
	/// <param name="execute">Method that has a single parameter and does not return a value.</param>
	/// <param name="canExecute">Method that defines a set of criteria and determines whether the
	/// specified object meets those criteria.<br/>
	/// If <see langword="null"/> is passed the CanExecute method will always return true.</param>
	public DelegateCommand( Action<object?> execute, Predicate<object?>? canExecute ) :
		this( execute, canExecute, null, null )
	{ }

	/// <summary>Initializes a new instance of the DelegateCommand class.</summary>
	/// <param name="execute">Method that has a single parameter and does not return a value.</param>
	public DelegateCommand( Action<object?> execute ) : this( execute, null )
	{ }

	#endregion

	private bool propertyChange = false;

	/// <summary>Listens to any property changed on the object that is passed.</summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The PropertyChangedEventArgs instance containing the event data.</param>
	private void ObjectPropertyChanged( object? sender, PropertyChangedEventArgs e )
	{
		if( _listenProperties is null || ( _listenProperties is not null && _listenProperties.Contains( e.PropertyName ) ) )
		{
			// Raise on all the property changes
			propertyChange = true;
			RaiseCanExecuteChanged();
		}
	}

	/// <summary>Raises the can execute changed.</summary>
	private void RaiseCanExecuteChanged()
	{
		Subscribers?.Invoke( this, EventArgs.Empty );
		_canExecuteChanged?.Invoke( this, EventArgs.Empty );
	}

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
	public bool CanExecute( object? parameter ) => _canExecute is null || _canExecute( parameter );

	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged
	{
		add
		{
			_canExecuteChanged += value;
			if( propertyChange )
			{
				Subscribers += value;
				OnPropertyChanged( nameof( SubscriberCount ) );
				propertyChange = false;
			}
		}

		remove
		{
			_canExecuteChanged -= value;
			Subscribers -= value;
			OnPropertyChanged( nameof( SubscriberCount ) );
		}
	}

	/// <inheritdoc/>
	public void Execute( object? parameter ) => _execute( parameter );

	#endregion

	#region IDisposable Implementation

	private bool _isDisposed;

	/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
	public void Dispose()
	{
		Dispose( true );
		GC.SuppressFinalize( this );
	}

	/// <summary>Releases unmanaged and - optionally - managed resources.</summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	protected virtual void Dispose( bool disposing )
	{
		if( _isDisposed )
		{
			return;
		}

		if( disposing )
		{
			if( _listenObject is not null )
			{
				_listenObject.PropertyChanged -= ObjectPropertyChanged;
			}

			_listenObject = null;
		}

		_isDisposed = true;
	}

	#endregion
}