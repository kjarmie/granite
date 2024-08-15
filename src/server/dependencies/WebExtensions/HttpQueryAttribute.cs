using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// Custom implementation of the proposed HTTP Query method, an idempotent method for querying data from a REST api.
///
/// It provides all the features of the HTTP Get method, but with the addition of a body to allow for more complex
/// querying, for example pagination, filtering, etc. which would be too large or unwieldy for url or query parameters.
///
/// Get requests work well as human-readable or browser url strings, but applications that query data
/// may have more specific needs that do not fall in line with the HTTP specification for Get
/// </summary>
public class HttpQueryAttribute : HttpMethodAttribute
{
    private static readonly IEnumerable<string> SupportedMethods = new[] { "QUERY" };

    /// <summary>
    /// Creates a new <see cref="Microsoft.AspNetCore.Mvc.HttpQueryAttribute"/>.
    /// </summary>
    public HttpQueryAttribute() : base(SupportedMethods)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/> with the given route template.
    /// </summary>
    /// <param name="template">The route template. May not be null.</param>
    public HttpQueryAttribute([StringSyntax("Route")] string template) : base(SupportedMethods, template)
    {
        ArgumentNullException.ThrowIfNull(template);
    }
}