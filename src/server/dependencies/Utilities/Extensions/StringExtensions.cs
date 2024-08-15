using System.Text.RegularExpressions;

namespace Common.Utilities;

public static class StringExtensions
{
    /// <summary>
    /// Converts a string to lowercase kebab-case (separated by hyphens).
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The converted string in kebab-case.</returns>
    /// <remarks>
    /// This method converts the input string to lowercase and inserts hyphens (-) between words in camelCase.
    /// Words are determined by splitting on uppercase letters that are not the first letter of the string.
    /// The original casing of the first letter is preserved.
    ///
    /// Empty or null strings are returned without modification.
    /// </remarks>
    public static string ToKebabCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var segments = str.Split("/");

        var s = segments.Select(e =>
        {
            var str1 = Regex.Replace(e, "[A-Z][a-z]+", m => $"-{m.ToString().ToLower()}");

            // find and replace all parts that are all capital letter e.g. NET
            var str2 = Regex.Replace(str1, "[A-Z]+", m => $"-{m.ToString().ToLower()}");

            return str2.TrimStart('-');
        }).ToList();

        var output = string.Join('/', s);
        return output;
    }
}