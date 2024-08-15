// ReSharper disable once CheckNamespace
namespace System.Reflection;

public static class MethodInfoExtensions
{
    public static bool HasAttribute<T>(this MethodInfo method) => method
        .GetCustomAttributes(false)
        .Any(attribute => attribute.GetType().FullName == typeof(T).FullName);
}