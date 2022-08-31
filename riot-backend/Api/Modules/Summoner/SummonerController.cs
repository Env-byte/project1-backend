using System.ComponentModel;

namespace riot_backend.Api.Modules.Summoner;

using Microsoft.AspNetCore.Mvc;

[Route("api/summoner/")]
public class SummonerController : Controller
{
    private readonly ILogger<SummonerController> _logger;
    private readonly SummonerService _service;

    public SummonerController(ILogger<SummonerController> logger, SummonerService summonerService)
    {
        _logger = logger;
        _service = summonerService;
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
        try
        {
            return Ok(_service.Refresh(puuid));
        }
        catch (WarningException e)
        {
            return BadRequest(new ErrorWrapper(e.Message, 101));
        }
    }
}