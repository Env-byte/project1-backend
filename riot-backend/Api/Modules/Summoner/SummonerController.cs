using riot_backend.Api.Modules.Matches;

namespace riot_backend.Api.Modules.Summoner;

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

[Route("api/summoner/")]
public class SummonerController : Controller
{
    private readonly ILogger<SummonerController> _logger;
    private readonly SummonerService _service;

    public SummonerController(ILogger<SummonerController> logger,
        SummonerRepository summonerRepository,
        SummonerProvider summonerProvider,
        MatchRepository matchRepository)
    {
        _logger = logger;
        _service = new SummonerService(summonerRepository, summonerProvider, matchRepository);
    }

    [HttpGet("name/{name}")]
    public IActionResult GetByName(string name)
    {
        return Ok(_service.GetByName(name));
    }

    [HttpGet("puuid/{puuid}")]
    public IActionResult GetByPuuid(string puuid)
    {
        return Ok(_service.GetByPuuid(puuid));
    }

    [HttpGet("{puuid}/refresh")]
    public IActionResult Refresh(string puuid)
    {
        return Ok(_service.Refresh(puuid));
    }
}