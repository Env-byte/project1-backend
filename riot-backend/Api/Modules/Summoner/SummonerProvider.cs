using riot_backend.Services;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerProvider
{
    private readonly IHttpClientWrapper _http;
    private readonly RegionService _regionService;
    private const string Endpoint = "/tft/summoner/v1/summoners";

    public SummonerProvider(IHttpClientWrapper http, RegionService regionService)
    {
        _http = http;
        _regionService = regionService;
    }

    public Types.Summoner GetByName(string name)
    {
        var url = _regionService.platformRoute + Endpoint + "/by-name/" + Uri.EscapeDataString(name);
        return _http.Get<Types.Summoner>(url);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        var url = _regionService.platformRoute + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid);
        return _http.Get<Types.Summoner>(url);
    }
}