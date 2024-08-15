using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Granite.Infrastructure.Data;

public class DatabaseServiceRegistrar : IServiceRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        //// Register all repositories as their interfaces
        // services.Scan(scan => scan
        //     .FromAssemblies(typeof(Infrastructure.AssemblyReference).Assembly)
        //     .AddClasses(filter => filter.Where(x => x.IsAssignableTo(typeof(IRepository))), publicOnly: true)
        //     .UsingRegistrationStrategy(RegistrationStrategy.Throw)
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime()); // Set as scoped (each API request creates new scope)

        services.AddDbContext<GraniteDatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("db"));
        });
    }
}