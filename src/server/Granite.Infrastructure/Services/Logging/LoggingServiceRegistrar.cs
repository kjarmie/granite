using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Granite.Infrastructure.Services.Logging;

public class LoggingServiceRegistrar : IServiceRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddSerilog((context, config) => config.StandardConfiguration(configuration, env));
    }
}