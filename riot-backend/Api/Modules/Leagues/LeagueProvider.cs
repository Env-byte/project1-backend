namespace riot_backend.Api.Modules.Leagues;

public class LeagueProvider
{
    private readonly IHttpClientWrapper _http;

    private const string Endpoint = "/tft/league/v1";

    public LeagueProvider(IHttpClientWrapper http)
    {
        _http = http;
    }

    public List<Types.League> GetSummonerLeague(string summonerId)
    {
        var url = Config.Endpoints[0, 1] + Endpoint + "/entries/by-summoner/" + Uri.EscapeDataString(summonerId);
        return _http.Get<List<Types.League>>(url);
    }
}