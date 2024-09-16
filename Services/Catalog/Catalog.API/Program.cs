using BuildingBlocks.DependencyInjection;
using Scalar.AspNetCore;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //todo Builder 
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.SchemaFilter<EnumSchemaFilter>());
            builder.Services.AddCarter();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.RegisterServices();
            builder.Services.AddMarten(opt =>
            {
                opt.Connection(builder.Configuration.GetConnectionString("CatalogDb")!);
                opt.Advanced.HiloSequenceDefaults.MaxLo = 1;
            }).UseLightweightSessions();
            //todo App
            var app = builder.Build();
            app.MapDefaultEndpoints();
            app.MapCarter();
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
