namespace riot_backend.Api.Modules.Summoner;

public class SummonerLoader : Curl
{
    public Types.Summoner Get(string name)
    {
        var response = CurlGet<Types.Summoner>(name).Result;
        return Types.Summoner.FromJson(response);
    }
}