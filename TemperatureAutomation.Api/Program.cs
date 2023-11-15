
namespace TemperatureAutomation.Api
{
    using BrewFather;
    using TemperatureMonitor;
    using Models;
    using SmartPlug;
    using System.Runtime;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.

            builder.Services.AddTransient<BrewFatherApi>();
            builder.Services.AddTransient<BrewFatherService>();
            builder.Services.AddTransient<SmartPlugService>();

            builder.Services.AddSingleton<TemperatureMonitorService>();
            builder.Services.Configure<BrewFatherConfig>(builder.Configuration.GetSection("BrewFather"));
            builder.Services.Configure<ShellyConfig>(builder.Configuration.GetSection("Shelly"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }

          //  app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
