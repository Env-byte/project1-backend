
using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.Leagues;

public class LeagueProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly Region _region;

    private const string Endpoint = "/tft/league/v1";

    public LeagueProvider(IHttpClientWrapper http, Region region)
    {
        _http = http;
        _region = region;
    }

    public List<Types.League> GetSummonerLeague(string summonerId)
    {
        var url = _region.platformRoute + Endpoint + "/entries/by-summoner/" + Uri.EscapeDataString(summonerId);
        return _http.Get<List<Types.League>>(url);
    }
}