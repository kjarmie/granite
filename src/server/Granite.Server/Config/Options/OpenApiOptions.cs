using Microsoft.OpenApi.Models;

namespace Granite.Server.Config.Options;

public class OpenApiOptions
{
    public List<OpenApiServer> Servers { get; set; } = [];
}