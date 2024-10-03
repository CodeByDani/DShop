using BuildingBlocks;
using BuildingBlocks.Behavior;
using BuildingBlocks.DependencyInjection;
using Catalog.API.Features.Category.Common;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            //todo Builder 
            var builder = WebApplication.CreateBuilder(args);
            //builder.AddServiceDefaults();
            var databaseConnection = builder.Configuration.GetConnectionString("CatalogDb")!;

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"Catalog.API.xml";
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

            builder.Services.AddHealthChecks()
                .AddNpgSql(databaseConnection);

            builder.Services.AddCarter();

            builder.Services.AddMarten(opt =>
            {
                opt.Connection(databaseConnection);
                opt.Advanced.HiloSequenceDefaults.MaxLo = 1;
            })
                .UseLightweightSessions()
                .InitializeWith<InitialDataCategory>();


            //todo App
            var app = builder.Build();
            app.MapDefaultEndpoints();
            app.MapCarter();
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "openapi/{documentName}.json";
                });
                app.MapScalarApiReference(option =>
                    option.WithTheme(ScalarTheme.DeepSpace)
                        .WithTitle("DShopAPI")
                        .WithForceThemeMode(ThemeMode.Dark));

            }
            app.Run();
        }
    }
}
