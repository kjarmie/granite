using Microsoft.Extensions.Options;

namespace Granite.Server.Config.Options;

public class JwtOptionsSetup : IConfigureNamedOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _config;

    public JwtOptionsSetup(IConfiguration config)
    {
        _config = config;
    }

    public void Configure(JwtOptions options)
    {

    }

    public void Configure(string? name, JwtOptions options)
    {
        _config.GetSection(SectionName).Bind(options);
    }
}