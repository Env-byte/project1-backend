using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Champions;

[Route("api/champion")]
public class ChampionController : Controller
{
    private readonly ILogger<ChampionController> _logger;
    private readonly ChampionLoader _loader;

    public ChampionController(ILogger<ChampionController> logger)
    {
        _logger = logger;
        _loader = new ChampionLoader();
    }


    [HttpGet("")]
    public List<Types.Champion> GetAll()
    {
        return _loader.GetAll();
    }

    [HttpGet("name")]
    public Types.Champion Get(string championId)
    {
        return _loader.Get(championId);
    }
}