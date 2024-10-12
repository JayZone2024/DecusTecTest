using FloodRiskPremiumCalculator.Bff.Api.Repositories;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

namespace FloodRiskPremiumCalculator.Bff.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddMemoryCache()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services
            .AddSingleton<IStateRatingRepository, StateRatingRepository>()
            .AddSingleton<IPremiumCalculator, PremiumCalculator>();

        // Build Application Pipeline
        var app = builder.Build();

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