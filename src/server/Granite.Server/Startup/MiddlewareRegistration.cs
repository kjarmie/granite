using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Web.Middleware;
using WebExtensions.Middleware;

namespace Granite.Server.Startup;

public static class MiddlewareRegistration
{
    /// <summary>
    ///     This method is used to register the middleware in the <see cref="Microsoft.AspNetCore.Builder.WebApplication" />
    ///     pipeline.
    /// </summary>
    /// <param name="app">The <see cref="Microsoft.AspNetCore.Builder.WebApplication" /> to add middleware to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="environment">The application environment.</param>
    /// <returns></returns>
    public static void ConfigurePipeline(this WebApplication app, IConfiguration configuration,
        IHostEnvironment environment)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        if (environment.IsDevelopment())
        {
            app.MapSwagger();
            app.UseSwaggerUI();
        }
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}