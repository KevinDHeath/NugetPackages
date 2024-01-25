namespace Sample.AspNet.Services;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> Get();
}