using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    /// <summary>
    /// This method is used to install all services registered using the <see cref="DependencyInjection.IServiceRegistrar"/>
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="environment">The application environment.</param>
    /// <param name="assemblies">The assemblies in which to search for service registrars.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment,
        params Assembly[] assemblies
    )
    {
        var serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(e => e.IsAssignableTo<IServiceRegistrar>())
            .Select(Activator.CreateInstance)
            .Cast<IServiceRegistrar>();

        foreach (var s in serviceInstallers) s.Register(services, configuration, environment);

        return services;
    }
}