using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Leagues;

[Route("api/league/")]
public class LeagueController : Controller
{
    private readonly LeagueService _leagueService;

    public LeagueController(LeagueService leagueService)
    {
        _leagueService = leagueService;
    }
    
    [HttpGet("summonerId/{summonerId}")]
    public IActionResult GetSummonerId(string summonerId)
    {
        return Ok(_leagueService.GetSummonerId(summonerId));
    }
}