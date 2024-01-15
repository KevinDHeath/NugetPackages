using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Core.Classes;

/// <summary>Initializes a new instance of the InterfaceFactory class.</summary>
/// <param name="concrete">The concrete class type.</param>
/// <param name="interfaceType">The interface type.</param>
/// <remarks>Supports converting Interface types for Json serialization by using a factory pattern.</remarks>
public class InterfaceFactory( Type concrete, Type interfaceType ) : JsonConverterFactory
{
	#region Properties

	/// <summary>Gets the concrete class type.</summary>
	public Type ConcreteType { get; } = concrete;

	/// <summary>Gets the interface type.</summary>
	public Type InterfaceType { get; } = interfaceType;

	#endregion

	#region Overridden Methods

	/// <summary>Determines whether the converter instance can convert the specified object type.</summary>
	/// <param name="typeToConvert">The type of the object to check whether it can be
	/// converted by this converter instance.</param>
	/// <returns>True if the instance can convert the specified object type otherwise False.</returns>
	public override bool CanConvert( Type typeToConvert )
	{
		return typeToConvert == InterfaceType;
	}

	/// <summary>Creates a converter for a specified type.</summary>
	/// <param name="typeToConvert">The type handled by the converter.</param>
	/// <param name="options">The serialization options to use.</param>
	/// <returns>A converter compatible with typeToConvert.</returns>
	public override JsonConverter CreateConverter( Type typeToConvert, JsonSerializerOptions options )
	{
		var converterType = typeof( InterfaceConverter<,> ).MakeGenericType( ConcreteType, InterfaceType );

		return (JsonConverter)Activator.CreateInstance( converterType )!;
	}

	#endregion
}