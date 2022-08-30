namespace riot_backend.Api.Modules.Matches;

public class MatchProvider
{
    private readonly IHttpClientWrapper _http;

    private const string Endpoint = "/tft/match/v1/matches";

    public MatchProvider(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Match GetMatch(string matchId)
    {
        var url = Config.TftEndpoints[0, 1] + Endpoint + "/" + Uri.EscapeDataString(matchId);
        return _http.Get<Types.Match>(url);
    }

    public List<string> GetMatchPuuids(string puuid)
    {
        var url = Config.TftEndpoints[0, 1] + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid) +
                  "/ids?start=0&count=10";
        return _http.Get<List<string>>(url);
    }
}