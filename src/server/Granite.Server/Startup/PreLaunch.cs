using Granite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace WebAPI.Startup;

public static class PreLaunch
{
    public static void RunPreLaunchFunctions(IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env, WebApplication app)
    {
        Log.Information("Pre-launch: Migrating database.");
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<GraniteDatabaseContext>();
        dataContext.Database.Migrate();
    }
}