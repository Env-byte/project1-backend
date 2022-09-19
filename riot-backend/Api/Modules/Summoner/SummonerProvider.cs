using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly Region _region;
    private const string Endpoint = "/tft/summoner/v1/summoners";

    public SummonerProvider(IHttpClientWrapper http, Region region)
    {
        _http = http;
        _region = region;
    }

    public Types.Summoner GetByName(string name)
    {
        var url = _region.platformRoute + Endpoint + "/by-name/" + Uri.EscapeDataString(name);
        return _http.Get<Types.Summoner>(url);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        var url = _region.platformRoute + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid);
        return _http.Get<Types.Summoner>(url);
    }
}