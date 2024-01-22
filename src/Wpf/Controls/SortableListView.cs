using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Common.Wpf.Controls.Classes;

namespace Common.Wpf.Controls;

// https://wpf-tutorial.com/listview-control/listview-how-to-column-sorting/
// https://stackoverflow.com/questions/30787068/wpf-listview-sorting-on-column-click
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadata

/// <summary>Control to implement the sorting and filtering of a ListView.</summary>
/// <remarks>
/// 1. Add a namespace attribute to the root element of the markup file it is to be used in:<br/>
/// <code language="XAML">xmlns:cc="clr-namespace:Common.Wpf.Controls;assembly=Common.Wpf.Controls"</code>
/// 2. Define the control in XAML setting the <b>DefaultColumn</b> as a one-based column index:<br/>
/// <code language="XAML">&lt;cc:SortableListView DefaultColumn="2"... /&gt;</code>
/// 3. To implement filtering make sure the <b>FilterPredicate</b> property is set on the control.<br/>
/// <code language="XAML">&lt;cc:SortableListView FilterPredicate="{Binding MyFilter}"... /&gt;</code>
/// 4. In the <b>ViewModel</b> create the callback function to handle the filtering requirements: 
/// <code>
/// private string _filterText = string.Empty;
/// 
/// public Func&lt;object bool&gt; MyFilter
/// {
///   get
///   {
///     return (item) =>
///     {
///       var person = item as Person;
///       return person.Name.Contains( _filterText ) ||
///         person.Occupation.Contains( _filterText );
///     };
///   }
/// }</code>
/// To perform the filtering make sure the <b>_filterText</b> value is set with the criteria then
/// execute the <b>ApplyFilter()</b> method on the SortableListView control.
/// </remarks>
public class SortableListView : ListView
{
	#region Properties

	/// <summary>Identifies the DefaultColumn dependency property.</summary>
	public static readonly DependencyProperty DefaultColumnProperty = DependencyProperty.Register(
		name: nameof( DefaultColumn ), propertyType: typeof( int ), ownerType: typeof( SortableListView ),
		typeMetadata: new PropertyMetadata( defaultValue: cDftColIndex ) );

	/// <summary>Gets or sets the default one-based column index.<br/>
	/// The default value is <c>1</c>.</summary>
	public int DefaultColumn
	{
		get => (int)GetValue( DefaultColumnProperty );
		set => SetValue( DefaultColumnProperty, value );
	}

	/// <summary>Identifies the FilterPredicate dependency property.</summary>
	public static readonly DependencyProperty FilterPredicateProperty = DependencyProperty.Register(
		name: nameof( FilterPredicate ), propertyType: typeof( Func<object, bool> ),
		ownerType: typeof( SortableListView ), typeMetadata: new PropertyMetadata( null ) );

	/// <summary>Gets or sets the default filter predicate.</summary>
	public Func<object, bool> FilterPredicate
	{
		get
		{
			var val = (Func<object, bool>)GetValue( FilterPredicateProperty );
			if( val is not null ) { return val; } else { return ( obj ) => obj is not null; }
		}
		set => SetValue( FilterPredicateProperty, value );
	}

	#endregion

	#region Constructors and Variables

	private const int cDftColIndex = 1;
	private GridViewColumnHeader? _firstHeader;
	private ListSortDirection _lastDirection = ListSortDirection.Ascending;
	private GridViewColumnHeader? _lastHeader;
	private SortAdorner? _lastAdorner;

	static SortableListView()
	{
		DefaultStyleKeyProperty.OverrideMetadata( typeof( SortableListView ),
			new FrameworkPropertyMetadata( typeof( SortableListView ) ) );
	}

	#endregion

	#region Public Methods

	/// <summary>Sorts the list view on a one-based column index.</summary>
	/// <param name="columnIndex">Column index to use as the default sort column.
	/// The default value is <c>1</c>.</param>
	/// <remarks>This method should be called when the list view is first initialized.</remarks>
	public void Sort( int columnIndex = cDftColIndex )
	{
		string columnName = GetColumnName( columnIndex );
		GridViewColumnHeader? header = GetColumnHeader( columnName );
		Sort( header );
	}

	/// <summary>Sorts the list view based on a grid view column header.</summary>
	/// <param name="header">Grid view column header to use.</param>
	/// <remarks>This method should be called when a grid view column header is clicked.</remarks>
	public void Sort( GridViewColumnHeader? header )
	{
		SortList( header );
	}

	/// <summary>Resets the list view.</summary>
	/// <remarks>This method should be called if the contents of the list view change.</remarks>
	public void Reset()
	{
		if( _lastHeader != null )
		{
			AdornerLayer.GetAdornerLayer( _lastHeader ).Remove( _lastAdorner );
			_lastHeader = null;
		}

		SortList( _firstHeader );
	}

	/// <summary>Applies the filter predicate to the collection view.</summary>
	public void ApplyFilter()
	{
		ICollectionView? dftView = CollectionViewSource.GetDefaultView( ItemsSource );
		if( dftView is not null )
		{
			dftView.Filter = ( item ) => FilterPredicate( item );
		}
	}

	#endregion

	#region Private Methods

	private string GetColumnName( int columnIndex = cDftColIndex )
	{
		if( columnIndex < cDftColIndex ) { return string.Empty; }

		if( View is GridView gv && gv.Columns.Count >= columnIndex )
		{
			object hdr = gv.Columns[columnIndex - 1].Header;
			if( hdr is string rtn ) { return rtn; }
		}

		return string.Empty;
	}

	private static string? GetPropertyName( GridViewColumnHeader? header )
	{
		Binding? binding = header?.Column.DisplayMemberBinding as Binding;

		return binding is not null ? binding.Path.Path :
			header is not null && header.Content is string title ? title : null;
	}

	private GridViewColumnHeader? GetColumnHeader( string propertyName )
	{
		return SortAdorner.GetGridViewColumnHeader( this, propertyName );
	}

	private void SortList( GridViewColumnHeader? header )
	{
		string? propertyName = GetPropertyName( header );
		if( header is null || propertyName is null ) { return; }

		// Store the very first header used
		_firstHeader ??= header;

		// Set the direction
		ListSortDirection direction = header != _lastHeader
			? ListSortDirection.Ascending
			: _lastDirection == ListSortDirection.Ascending ?
				ListSortDirection.Descending : ListSortDirection.Ascending;

		// Clear the previous direction indicator
		if( _lastHeader != null )
		{
			AdornerLayer.GetAdornerLayer( _lastHeader ).Remove( _lastAdorner );
		}

		// Sort the list
		ICollectionView? data = CollectionViewSource.GetDefaultView( ItemsSource );
		if( data is not null )
		{
			data.SortDescriptions.Clear();
			data.SortDescriptions.Add( new SortDescription( propertyName, direction ) );
			data.Refresh();
		}

		// Store the previous values
		_lastDirection = direction;
		_lastHeader = header;
		_lastAdorner = new SortAdorner( header, direction );
		AdornerLayer.GetAdornerLayer( header ).Add( _lastAdorner );
	}

	#endregion
}