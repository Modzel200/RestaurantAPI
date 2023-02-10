using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<RestaurantDbContext>();
            builder.Services.AddScoped<RestaurantSeeder>();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
            seeder.Seed();
            // Configure the HTTP request pipeline.
            
            if (app.Environment.IsDevelopment())
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
}