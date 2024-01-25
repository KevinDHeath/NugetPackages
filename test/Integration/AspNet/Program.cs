// The Repository Pattern explained for EVERYONE (with Code Examples)
// https://www.youtube.com/watch?v=Wiy54682d1w

namespace Sample.AspNet;

public class Program
{
	public static void Main( string[] args )
	{
		var builder = WebApplication.CreateBuilder( args );

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddScoped<Services.IWeatherForecastService, Services.WeatherForecastServiceEx>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if( app.Environment.IsDevelopment() )
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}