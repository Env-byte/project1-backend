using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.Matches;

public class MatchProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly Header _region;

    private const string Endpoint = "/tft/match/v1/matches";

    public MatchProvider(IHttpClientWrapper http, Header region)
    {
        _http = http;
        _region = region;
    }

    public Types.Match GetMatch(string matchId)
    {
        var url = _region.RegionalRoute + Endpoint + "/" + Uri.EscapeDataString(matchId);
        return _http.Get<Types.Match>(url);
    }

    public List<string> GetMatchPuuids(string puuid)
    {
        var url = _region.RegionalRoute + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid) +
                  "/ids?start=0&count=10";
        return _http.Get<List<string>>(url);
    }
}