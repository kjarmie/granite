// ReSharper disable once CheckNamespace
namespace System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static void Iterate<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var e in enumerable)
        {
            action(e);
        }
    }
}