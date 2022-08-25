namespace riot_backend.Api.Modules.Summoner;

using Microsoft.AspNetCore.Mvc;

[Route("api/summoner/")]
public class SummonerController : Controller
{
    private readonly ILogger<SummonerController> _logger;
    private readonly SummonerLoader _loader;
    private readonly SummonerService _service;

    public SummonerController(ILogger<SummonerController> logger, IHttpClientWrapper http,
        SummonerService summonerService)
    {
        _logger = logger;
        _service = summonerService;
        _loader = new SummonerLoader(http);
    }

    [HttpGet("{name}")]
    public Types.Summoner GetByName(string name)
    {
        return _loader.GetByName(name);
    }

    [HttpGet("{puuid}")]
    public Types.Summoner GetByPuuid(string puuid)
    {
        return _loader.GetByPuuid(puuid);
    }
}