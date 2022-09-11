using System.Collections.Specialized;

namespace riot_backend.Api;

public class Config
{
    public static readonly NameValueCollection PlatformRoutes = new()
    {
        { "EUW1", "https://euw1.api.riotgames.com" },
        { "NA1", "https://na1.api.riotgames.com" },
        { "EUN1", "https://eun1.api.riotgames.com" }
    };

    public static readonly NameValueCollection RegionalRoutes = new()
    {
        { "EUW1", "https://europe.api.riotgames.com" },
        { "EUN1", "https://europe.api.riotgames.com" },
        { "NA1", "https://americas.api.riotgames.com" }
    };
}