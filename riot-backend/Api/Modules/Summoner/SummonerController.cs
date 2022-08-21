namespace riot_backend.Api.Modules.Summoner;

using Microsoft.AspNetCore.Mvc;

public class SummonerController : Controller
{
    private readonly ILogger<SummonerController> _logger;
    private readonly SummonerLoader _loader;

    public SummonerController(ILogger<SummonerController> logger, IHttpClientWrapper http)
    {
        _logger = logger;
        _loader = new SummonerLoader(http);
    }

    [HttpGet("api/summoner/name")]
    public Types.Summoner GetByName(string name)
    {
        return _loader.GetByName(name);
    }

    [HttpGet("api/summoner/puuid")]
    public Types.Summoner GetByPuuid(string puuid)
    {
        return _loader.GetByPuuid(puuid);
    }
}