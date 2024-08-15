// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.Configuration;

public class ConfigurationSectionNotFoundException : Exception
{
    private static string _message(string sectionName) => $"The configuration section '{sectionName}' could not be found. Please ensure the section exists and is properly formatted.";

    public ConfigurationSectionNotFoundException(string sectionName) : base(_message(sectionName))
    {

    }
}