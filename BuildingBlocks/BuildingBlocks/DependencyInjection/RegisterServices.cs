using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DependencyInjection;

public static class ServiceCollectionConfigurationHandler
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        TypeAdapterConfig.GlobalSettings.Scan(assemblies);

        services.Scan(selector =>
            selector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<IScopeLifetime>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.Scan(selector =>
            selector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<ITransientLifetime>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        services.Scan(selector =>
            selector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<ISingletonLifetime>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        return services;
    }

}