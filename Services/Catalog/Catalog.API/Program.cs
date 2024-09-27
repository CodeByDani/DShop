using BuildingBlocks;
using BuildingBlocks.Behavior;
using BuildingBlocks.DependencyInjection;
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
            builder.AddServiceDefaults();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"Catalog.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.SchemaFilter<EnumSchemaFilter>();
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            builder.Services.RegisterServices();
            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddCarter();

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
