// ReSharper disable once CheckNamespace
namespace System.Reflection;

public static class TypeInfoExtensions
{
    public static bool IsAssignableTo<T>(this TypeInfo typeInfo)
    {
        return typeof(T).IsAssignableFrom(typeInfo) &&
               typeInfo is { IsInterface: false, IsAbstract: false };
    }
}