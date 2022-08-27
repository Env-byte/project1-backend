using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Champions;

[Route("api/champion/")]
public class ChampionController : Controller
{
    private readonly ILogger<ChampionController> _logger;
    private readonly ChampionService _championService;

    public ChampionController(ILogger<ChampionController> logger, ChampionService championService)
    {
        _logger = logger;
        _championService = championService;
    }


    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(_championService.GetAll());
    }

    [HttpGet("{championId}")]
    public IActionResult Get(string championId)
    {
        return Ok(_championService.Get(championId));
    }
}