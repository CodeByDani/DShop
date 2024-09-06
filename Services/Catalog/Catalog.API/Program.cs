namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //todo Builder 
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCarter();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //todo App
            var app = builder.Build();
            app.MapCarter();
            app.Run();
        }
    }
}
