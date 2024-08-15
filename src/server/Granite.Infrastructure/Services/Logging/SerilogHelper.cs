using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Granite.Infrastructure.Services.Logging;

public static class SerilogHelper
{
    public static LoggerConfiguration StandardConfiguration(this LoggerConfiguration config, IConfiguration configuration, IHostEnvironment env)
    {
        config.ReadFrom.Configuration(configuration);
        config.Enrich.FromLogContext();
        config.Enrich.WithMachineName();
        config.Enrich.WithEnvironmentName();
        config.Enrich.WithProperty("Application", env.ApplicationName);

        return config;
    }
}