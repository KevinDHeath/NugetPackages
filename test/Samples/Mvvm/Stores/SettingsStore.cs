using Common.Core.Classes;
using Sample.Mvvm.Models;

namespace Sample.Mvvm.Stores;

public class SettingsStore
{
	public Settings Settings { get; private set; }

	private static string _settingsFile = string.Empty;

	public SettingsStore( string settingsFile )
	{
		_settingsFile = settingsFile;
		Settings = Load( _settingsFile ) ?? new Settings() { FontSize = 14 };
	}

	private static Settings? Load( string settingsFile )
	{
		if( string.IsNullOrWhiteSpace( settingsFile ) ) { return null; }
		_settingsFile = ReflectionHelper.AddCurrentPath( settingsFile );
		return JsonHelper.DeserializeFile<Settings>( _settingsFile );
	}

	public static bool Save( Settings settings )
	{
		if( string.IsNullOrWhiteSpace( _settingsFile ) ) { return false; }
		System.Text.Json.JsonSerializerOptions options = JsonHelper.DefaultSerializerOptions();
		options.WriteIndented = true;
		return JsonHelper.Serialize( settings, _settingsFile, options );
	}
}