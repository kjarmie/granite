using Granite.Infrastructure.Services.Logging;
using Serilog;
using Granite.Server.Startup;
using WebAPI.Startup;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var config = builder.Configuration;
    var env = builder.Environment;
    var version = typeof(Granite.Server.AssemblyReference).Assembly.GetName().Version;

    // This logger uses the same configuration as the logger that gets injected as a service. We do not want to tie our
    // application to Serilog, so we do not introduce that dependency into the lower layers, but we need access to the
    // logging methods without injecting any services, since this is where we register the services
    Log.Logger = new LoggerConfiguration()
        .StandardConfiguration(config, env)
        .CreateLogger();

    Log.Information("Initialising Granite Server");
    Log.Information("Granite Server environment: {environment}.", builder.Environment.EnvironmentName);
    Log.Information("Granite Server version: {version}.", version);

    Log.Information("Registering services.");
    builder.Host.UseSerilog((context, logConfig) =>
    {
        logConfig.StandardConfiguration(config, env);
    });

    services.RegisterServices(config, env, [
        typeof(Granite.Server.AssemblyReference).Assembly,
        typeof(Granite.Infrastructure.AssemblyReference).Assembly,
        typeof(Granite.Application.AssemblyReference).Assembly,
        typeof(Granite.Domain.AssemblyReference).Assembly
    ]);

    Log.Information("Building Granite Server.");

    var app = builder.Build();

    Log.Information("Running pre-launch functions.");

    // PreLaunch.RunPreLaunchFunctions(services, config, env, app);

    app.ConfigurePipeline(config, env);

    Log.Information("Running Granite Server");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
