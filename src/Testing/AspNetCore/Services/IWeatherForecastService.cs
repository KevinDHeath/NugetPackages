namespace AspNetCore.Services;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> Get();
}