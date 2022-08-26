namespace riot_backend.Api.Modules.Summoner;

public class SummonerProvider
{
    private readonly IHttpClientWrapper _http;
    private const string Endpoint = "/tft/summoner/v1/summoners";

    public SummonerProvider(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Summoner GetByName(string name)
    {
        var url = Config.Endpoints[0, 1] + Endpoint + "/by-name/" + Uri.EscapeDataString(name);
        return _http.Get<Types.Summoner>(url);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        var url = Config.Endpoints[0, 1] + Endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid);
        return _http.Get<Types.Summoner>(url);
    }
}