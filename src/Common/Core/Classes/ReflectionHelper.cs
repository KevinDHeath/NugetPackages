using System.Collections;
using System.Reflection;
using System.Security;

namespace Common.Core.Classes;

/// <summary>Class to provide methods that use reflection.</summary>
public static class ReflectionHelper
{
	private static readonly Type _stringType = typeof( string );
	private static readonly Type _secureType = typeof( SecureString );

	#region Apply Changes

	/// <summary>Applies changes to the target object properties from the source object.</summary>
	/// <typeparam name="T">Class type being used.</typeparam>
	/// <param name="source">Source object.</param>
	/// <param name="target">Target object.</param>
	public static void ApplyChanges<T>( T? source, T? target ) where T : class
	{
		if( source is null || target is null ) { return; }

		foreach( PropertyInfo prop in GetProperties( source.GetType() ) )
		{
			if( !prop.CanWrite ) {  continue; }

			object? sourceVal = prop.GetValue( source );
			object? targetVal = prop.GetValue( target );

			if( IsClassOrInterface( prop ) )
			{
				if( sourceVal is null && targetVal is not null ) { target = source; }
				else
				{
					//if( sourceVal is not null && targetVal is null ) { target = ; }
					ApplyChanges( sourceVal, targetVal );
				}
			}
			else
			{
				prop.SetValue( target, sourceVal );
			}
		}
	}

	#endregion

	#region Check For Changes

	/// <summary>Compare values to the target object properties from the source object.</summary>
	/// <typeparam name="T">Class type being used.</typeparam>
	/// <param name="source">Source object.</param>
	/// <param name="target">Target object.</param>
	/// <returns><see langword="true"/> if the source object properties are equal to the target object.</returns>
	public static bool IsEqual<T>( T? source, T? target ) where T : class
	{
		if( source is null || target is null ) { return false; }

		foreach( PropertyInfo prop in GetProperties( source.GetType() ) )
		{
			if( !prop.CanWrite ) continue;
			object? sourceVal = prop.GetValue( source );
			object? targetVal = prop.GetValue( target );

			if( IsClassOrInterface( prop ) )
			{
				return IsEqualClass( sourceVal, targetVal );
			}
			else
			{
				if( sourceVal is not null && !sourceVal.Equals( targetVal ) ) { return false; }
			}
		}

		return true;
	}

	private static bool IsEqualClass( object? sourceVal, object? targetVal )
	{
		if( sourceVal is null & targetVal is not null ||
			sourceVal is not null & targetVal is null ) { return false; }
		if( !IsEqual( sourceVal, targetVal ) ) { return false; }
		return true;
	}

	#endregion

	#region Create Deep Copy

	/// <summary>Creates a deep copy of an object.</summary>
	/// <param name="obj">Object to copy.</param>
	/// <returns><see langword="null"/> is returned if the object could not be copied.</returns>
	/// <remarks>
	/// When a deep copy operation is performed, the cloned object can be modified
	/// without affecting the original object.
	/// <br/><br/>The <i>Object.MemberwiseClone</i> method creates a shallow copy by creating a new object,
	/// and then copying the non-static fields of the current object to the new object.
	/// If a field is a value type, a bit-by-bit copy of the field is performed.
	/// If a field is a reference type, the reference is copied but the referred object
	/// is not; therefore, the original object and its clone refer to the same object.
	/// <br/><br/>This method is meant for simple Model classes, more complex classes are
	/// supported but because it is written using reflection it can be somewhat slow and cause
	/// performance issues.
	/// </remarks>
	public static object? CreateDeepCopy( object? obj )
	{
		// https://stackoverflow.com/questions/3647048/create-a-deep-copy-in-c-sharp
		// https://codereview.stackexchange.com/questions/147856/generic-null-empty-check-for-each-property-of-a-class

		if( obj == null ) return null;
		Type typ = obj.GetType();

		if( typ == _stringType ) { return obj.ToString(); } // Handle strings differently
		else if( typ == _secureType ) // Handle secure strings differently
		{
			return new System.Net.NetworkCredential( "", new System.Net.NetworkCredential( "",
				(SecureString)obj ).Password ).SecurePassword;
		}
		else if( typ.IsArray ) { return ProcessArray( obj ); }
		else if( obj is IEnumerable or ICollection ) { return ProcessCollection( typ, obj ); }

		// Try and create an instance of the object type
		object? objCopy = null;
		try { objCopy = Activator.CreateInstance( type: typ, nonPublic: true ); } catch( Exception ) { }
		if( objCopy is null ) { return objCopy; }

		if( typ.IsClass ) { CopyProperties( typ, obj, objCopy ); } // For a class use the properties
		else { CopyFields( typ, obj, objCopy ); } // For anything else use the fields

		return objCopy;
	}

	private static void CopyProperties( Type typ, object? obj, object? objCopy )
	{
		foreach( PropertyInfo prop in GetProperties( typ ) )
		{
			if( !prop.CanWrite ) { continue; }

			if( !prop.PropertyType.IsPrimitive && !prop.PropertyType.IsValueType &&
				prop.PropertyType != _stringType )
			{
				object? fieldCopy = CreateDeepCopy( prop.GetValue( obj ) );
				prop.SetValue( objCopy, fieldCopy );
			}
			else
			{
				prop.SetValue( objCopy, prop.GetValue( obj ) );
			}
		}
	}

	private static void CopyFields( Type typ, object? obj, object? objCopy )
	{
		FieldInfo[] fields = GetFields( typ );
		foreach( FieldInfo field in fields )
		{
			field.SetValue( objCopy, field.GetValue( obj ) );
		}
	}

	#endregion

	#region Private Functions

	private static bool IsClassOrInterface( PropertyInfo prop )
	{
		// String is classified as a class as is SecureString
		return (prop.PropertyType.IsClass || prop.PropertyType.IsInterface ) && !IsStringType( prop );
	}

	private static bool IsStringType( PropertyInfo prop )
	{
		// String is classified as a class as is SecureString
		return prop.PropertyType == _stringType || prop.PropertyType == _secureType;
	}

	private static PropertyInfo[] GetProperties<T>( T type ) where T : Type
	{
		// https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo

		// Ignore static properties
		return type.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
	}

	private static FieldInfo[] GetFields<T>( T type ) where T : Type
	{
		// https://learn.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo

		// Ignore static fields
		return type.GetFields( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
	}

	private static object? ProcessCollection( Type typ, object obj )
	{
		bool isList = IsList( typ );
		bool isDictionary = IsDictionary( typ );
		if( !isList && !isDictionary ) return Activator.CreateInstance( typ );

		if( isList )
		{
			return ProcessList( typ, obj );
		}

		return ProcessDictionary( typ, obj );
	}

	private static Array? ProcessArray( object obj )
	{
		Array org = (Array)obj;
		Array? ret = null;

		Type? elm = obj.GetType().GetElementType();
		if( elm is not null )
		{
			ret = Array.CreateInstance( elm, org.Length );
			Array cpy = ret;

			for( int i = 0; i < org.Length; i++ )
			{
				cpy.SetValue( CreateDeepCopy( org.GetValue( i ) ), i );
			}
		}

		return ret;
	}

	private static IDictionary? ProcessDictionary( Type typ, object obj )
	{
		IDictionary? org = (IDictionary)obj;
		IDictionary? ret = (IDictionary)Activator.CreateInstance( typ )!;
		if( org is not null && ret is not null )
		{
			foreach( object orgKey in org.Keys )
			{
				var orgVal = org[orgKey];
				var cpyKey = CreateDeepCopy( orgKey );
				var cpyVal = CreateDeepCopy( orgVal );

				if( cpyKey is not null )
				{
					ret.Add( cpyKey, cpyVal );
				}
			}
		}

		return ret;

	}

	private static IList? ProcessList( Type typ, object obj )
	{
		IList? org = (IList)obj;
		IList? ret = (IList)Activator.CreateInstance( typ )!;
		if( org is not null && ret is not null )
		{
			foreach( object? val in org )
			{
				ret.Add( CreateDeepCopy( val ) );
			}
		}

		return ret;
	}

	private static bool IsList( Type type )
	{
		if( typeof( IList ).IsAssignableFrom( type ) ) return true;

		foreach( Type it in type.GetInterfaces() )
		{
			if( it.IsGenericType && typeof( IList<> ) == it.GetGenericTypeDefinition() ) return true;
		}

		return false;
	}

	private static bool IsDictionary( Type type )
	{
		if( typeof( IDictionary ).IsAssignableFrom( type ) ) return true;

		foreach( Type it in type.GetInterfaces() )
		{
			if( it.IsGenericType && typeof( IDictionary<,> ) == it.GetGenericTypeDefinition() ) return true;
		}

		return false;
	}

	#endregion

	/// <summary>Adds the current path to a file name.</summary>
	/// <param name="fileName">The file name to use.</param>
	/// <returns>The full file name, or if no file name is supplied, the executing path is returned.</returns>
	public static string AddCurrentPath( string fileName = "" )
	{
		string path = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location )!;
		if( !string.IsNullOrWhiteSpace( fileName ) )
		{
			path = Path.Combine( path, Path.GetFileName( fileName ) );
		}
		return path;
	}
}