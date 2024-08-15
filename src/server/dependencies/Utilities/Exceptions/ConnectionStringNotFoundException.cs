// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public class ConnectionStringNotFoundException : Exception
{
    private static string _message(string name) => $"The connection string '{name}' could not be found. Please ensure the connection string exists and is properly formatted.";

    public ConnectionStringNotFoundException(string name) : base(_message(name))
    {

    }
}