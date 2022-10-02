using System.Text.RegularExpressions;

namespace riot_backend.helpers;

public static class StringHelper
{
    public static string EscapeJoin(this IEnumerable<string> enumerable, string delim = ",")
    {
        return string.Join(delim, enumerable.Select(item => "'" + item + "'").ToList());
    }

    public static string SplitOnCapitals(this string text)
    {
        var regex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);


        return regex.Replace(text, " ");
    }
}