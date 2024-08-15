

namespace WebAPI.Startup.Services;

public class RoutingServiceRegistrar : IServiceRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddControllers();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });
    }
}