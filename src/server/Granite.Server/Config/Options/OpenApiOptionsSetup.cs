using Microsoft.Extensions.Options;

namespace Granite.Server.Config.Options;

public class OpenApiOptionsSetup: IConfigureNamedOptions<OpenApiOptionsSetup>
{
    private readonly IConfiguration _config;

    public OpenApiOptionsSetup(IConfiguration configuration)
    {
        _config = configuration;
    }

    public void Configure(OpenApiOptionsSetup options)
    {

    }

    public void Configure(string? name, OpenApiOptionsSetup options)
    {
        _config.GetSection(name).Bind(options);
    }
}