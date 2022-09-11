using riot_backend.Services;

namespace riot_backend.Api.Modules.Matches;

public class MatchProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly RegionService _regionService;

    private const string Endpoint = "/tft/match/v1/matches";

    public MatchProvider(IHttpClientWrapper http, RegionService regionService)
    {
        _http = http;
        _regionService = regionService;
    }

    public Types.Match GetMatch(string matchId)
    {
        var url = _regionService.regionalRoute + Endpoint + "/" + Uri.EscapeDataString(matchId);
        return _http.Get<Types.Match>(url);
    }

    public List<string> GetMatchPuuids(string puuid)
    {
        var url = _regionService.regionalRoute + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid) +
                  "/ids?start=0&count=10";
        return _http.Get<List<string>>(url);
    }
}