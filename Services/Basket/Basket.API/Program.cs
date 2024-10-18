using BuildingBlocks;
using BuildingBlocks.Behavior;
using BuildingBlocks.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace Basket.API;

public class Program
{
    public static void Main(string[] args)
    {
        var assembly = typeof(Program).Assembly;
        //todo Builder Basket
        var builder = WebApplication.CreateBuilder(args);
        var databaseConnection = builder.Configuration.GetConnectionString("Basketdb")!;

        builder.AddMongoDBClient("MongoConnection");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"Basket.API.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.SchemaFilter<EnumSchemaFilter>();
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        builder.Services.RegisterServices();
        builder.Services.AddHealthChecks();

        //todo App Basket
        var app = builder.Build();
        app.MapDefaultEndpoints();
        app.MapCarter();
        //app.UseHealthChecks("/health", new HealthCheckOptions
        //{
        //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //});
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "openapi/{documentName}.json";
            });
            app.MapScalarApiReference(option =>
                option.WithTheme(ScalarTheme.Solarized)
                    .WithTitle("DShopBasketAPI")
                    .WithForceThemeMode(ThemeMode.Dark));

        }

        app.Run();
    }
}
