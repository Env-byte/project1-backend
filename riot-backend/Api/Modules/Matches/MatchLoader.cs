namespace riot_backend.Api.Modules.Matches;

public class MatchLoader
{
    private readonly IHttpClientWrapper _http;

    public MatchLoader(IHttpClientWrapper http)
    {
        _http = http;
    }

    public Types.Match Get(string name)
    {
        var response = _http.Get<Types.Match>(name).Result;
        return response;
    }
}