// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public static class IConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        var conn = configuration.GetConnectionString(name);

        if (conn is null)
            throw new ConnectionStringNotFoundException(name);

        return conn;
    }
}