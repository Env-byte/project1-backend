namespace riot_backend.Api.Modules.Summoner;

public class SummonerLoader
{
    private readonly IHttpClientWrapper _http;
    private const string _endpoint = "/tft/summoner/v1/summoners";

    public SummonerLoader(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Summoner GetByName(string name)
    {
        var url = Config.Endpoints[0, 1] + _endpoint + "/by-name/" + Uri.EscapeDataString(name);
        return _http.Get<Types.Summoner>(url);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        var url = Config.Endpoints[0, 1] + _endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid);
        return _http.Get<Types.Summoner>(url);
    }
}