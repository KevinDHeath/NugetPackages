global using System.Text.Json;
global using System.Text.Json.Serialization;
global using FluentAssertions;
global using Xunit;

global using Common.Core.Classes;
global using Common.Core.Converters;
global using Common.Core.Interfaces;
global using Common.Core.Models;

using System.Diagnostics.CodeAnalysis;
[assembly: SuppressMessage( "Naming", "VSSpell001:Spell Check", Justification = "<Pending>", Scope = "member", Target = "~M:Core.Tests.Classes.DataFactoryBaseTests.ReturnItems_should_eq_list_count" )]

namespace Core.Tests;

public class Global
{
	internal const string cDataFolder = @"Testdata\";
	internal const string cGlobalData = "data.json";
	internal const string cSettings = cDataFolder + DataFactoryBase.cConfigFile;

	#region Properties

	public bool? Boolean { get; set; }

	public DateOnly? DateOnly { get; set; }

	public decimal? Decimal { get; set; }

	public int? Integer { get; set; }

	#endregion

	#region Methods

	internal static string GetFileContents( string? filename )
	{
		if( !string.IsNullOrWhiteSpace( filename ) )
		{
			if( !filename.StartsWith( cDataFolder ) ) { filename = cDataFolder + filename; }
			if( File.Exists( filename ) ) { return File.ReadAllText( filename ); }
		}
		return string.Empty;
	}

	internal static List<T> GetJsonList<T>( string? filename, JsonSerializerOptions? options = null )
	{
		string? json = GetFileContents( filename );
		if( json.Length > 0 )
		{
			List<T>? rtn = JsonHelper.DeserializeJson<List<T>>( ref json );
			if( rtn is not null ) { return rtn; }
		}
		return [];
	}

	#endregion
}