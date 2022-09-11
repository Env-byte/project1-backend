using riot_backend.Services;

namespace riot_backend.Api.Modules.Leagues;

public class LeagueProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly RegionService _regionService;

    private const string Endpoint = "/tft/league/v1";

    public LeagueProvider(IHttpClientWrapper http, RegionService regionService)
    {
        _http = http;
        _regionService = regionService;
    }

    public List<Types.League> GetSummonerLeague(string summonerId)
    {
        var url = _regionService.platformRoute + Endpoint + "/entries/by-summoner/" + Uri.EscapeDataString(summonerId);
        return _http.Get<List<Types.League>>(url);
    }
}