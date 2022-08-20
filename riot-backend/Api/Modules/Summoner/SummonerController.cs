namespace riot_backend.Api.Modules.Summoner;

using Microsoft.AspNetCore.Mvc;

public class SummonerController : Controller
{
    private readonly ILogger<SummonerController> _logger;

    public SummonerController(ILogger<SummonerController> logger)
    {
        _logger = logger;
    }

    [HttpGet("api/Summoner")]
    public Types.Summoner Get(string name)
    {
        var summonerLoader = new SummonerLoader();
        return summonerLoader.Get(name);
    }
}