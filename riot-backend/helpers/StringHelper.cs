namespace riot_backend.helpers;

public static class StringHelper
{
    public static string EscapeJoin(this IEnumerable<string> enumerable, string delim = ",")
    {
        return string.Join(delim, enumerable.Select(item => "'" + item + "'").ToList());
    }
}