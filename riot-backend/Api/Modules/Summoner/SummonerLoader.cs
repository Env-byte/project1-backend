namespace riot_backend.Api.Modules.Summoner;

public class SummonerLoader
{
    private readonly IHttpClientWrapper _http;
    private readonly string _endpoint = "/tft/summoner/v1/summoners";

    public SummonerLoader(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Summoner GetByName(string name)
    {
        var url = _endpoint + "/by-name/" + Uri.EscapeDataString(name);
        return _http.Get<Types.Summoner>(url);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        var url = _endpoint + "/by-puuid/" + Uri.EscapeDataString(puuid);
        return _http.Get<Types.Summoner>(url);
    }
}