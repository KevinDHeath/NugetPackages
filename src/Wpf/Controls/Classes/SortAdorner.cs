// Ignore Spelling: Adorner

using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Common.Wpf.Controls.Classes;

/// <summary>
/// Triangular sorting direction adorner.
/// </summary>
internal class SortAdorner : Adorner
{
	#region Properties and Constructor

	private static readonly Geometry ascGeometry = Geometry.Parse( "M 0 4 L 3.5 0 L 7 4 Z" );
	private static readonly Geometry descGeometry = Geometry.Parse( "M 0 0 L 3.5 4 L 7 0 Z" );

	/// <summary>Sorting direction.</summary>
	internal ListSortDirection Direction { get; private set; }

	/// <summary>
	/// Initializes a new instance of the SortAdorner class.
	/// </summary>
	/// <param name="element">UI Element.</param>
	/// <param name="direction">Sorting direction.</param>
	internal SortAdorner( UIElement element, ListSortDirection direction ) : base( element )
	{
		Direction = direction;
	}

	#endregion

	#region Overridden Methods

	/// <summary>
	/// When overridden in a derived class, participates in rendering operations that are
	/// directed by the layout system. The rendering instructions for this element are not
	/// used directly when this method is invoked, and are instead preserved for later
	/// asynchronous use by layout and drawing.
	/// </summary>
	/// <param name="drawingContext">The drawing instructions for a specific element.
	/// This context is provided to the layout system.</param>
	protected override void OnRender( DrawingContext drawingContext )
	{
		base.OnRender( drawingContext );

		if( AdornedElement.RenderSize.Width < 20 ) return;

		TranslateTransform transform = new(
			AdornedElement.RenderSize.Width - 15, ( AdornedElement.RenderSize.Height - 5 ) / 2 );
		drawingContext.PushTransform( transform );

		Geometry geometry = ascGeometry;
		if( Direction == ListSortDirection.Descending ) geometry = descGeometry;
		drawingContext.DrawGeometry( Brushes.Black, null, geometry );

		drawingContext.Pop();
	}

	#endregion

	#region Static Methods

	/// <summary>
	/// Gets a grid view column header.
	/// </summary>
	/// <param name="listView">List view control.</param>
	/// <param name="colName">Column header name.</param>
	/// <returns>Null is returned if the column header could not be found.</returns>
	internal static GridViewColumnHeader? GetGridViewColumnHeader( ListView listView, string colName )
	{
		// Get all the normal column headers
		var list = GetVisualChildren<GridViewColumnHeader>( listView )
			.Where( x => x.Role == GridViewColumnHeaderRole.Normal );

		// Returns the specific column header if found, otherwise null is returned
		return list.FirstOrDefault( x => (string)x.Content == colName );
	}

	private static IEnumerable<T> GetVisualChildren<T>( DependencyObject parent ) where T : DependencyObject
	{
		int childrenCount = VisualTreeHelper.GetChildrenCount( parent );
		for( int i = 0; i < childrenCount; i++ )
		{
			DependencyObject child = VisualTreeHelper.GetChild( parent, i );
			if( child is T typ )
				yield return typ;

			foreach( var descendant in GetVisualChildren<T>( child ) )
				yield return descendant;
		}
	}

	#endregion
}