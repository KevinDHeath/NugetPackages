using Common.Core.Interfaces;

namespace Common.Core.Classes;

/// <summary>Base class for models that allow editing.</summary>
public abstract class ModelEdit : ModelData, ICloneable, IEditable, IEquatable<object>
{
	/// <summary>Creates a new object that is a copy of the current instance.</summary>
	/// <returns>A new object that is a copy of this instance.</returns>
	public abstract object Clone();

	/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
	/// <param name="obj">An object to compare with this object.</param>
	/// <returns>True if the current object is equal to the other parameter; otherwise, False.</returns>
	public abstract new bool Equals( object? obj );

	/// <summary>Updates the current object properties from an object of the same type.</summary>
	/// <param name="obj">An object with the changed values.</param>
	public abstract void Update( object? obj );
}