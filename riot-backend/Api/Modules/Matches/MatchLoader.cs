namespace riot_backend.Api.Modules.Matches;

public class MatchLoader
{
    private readonly IHttpClientWrapper _http;

    private readonly string _endpoint = "/tft/match/v1/matches";

    public MatchLoader(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Match GetMatch(string matchId)
    {
        var url = Config.TFTEndpoints[0, 1] + _endpoint + "/" + Uri.EscapeDataString(matchId);
        return _http.Get<Types.Match>(url);
    }

    public List<string> GetMatches(string puuid)
    {
        var url = Config.TFTEndpoints[0, 1] + _endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid) +
                  "/ids?start=0&count=20";
        return _http.Get<List<string>>(url);
    }
}