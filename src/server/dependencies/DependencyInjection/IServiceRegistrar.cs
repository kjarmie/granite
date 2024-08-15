using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Defines a collection of services that are registered with the MSDI container. Allows for grouping of related service registrations.
/// </summary>
public interface IServiceRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment env);
}