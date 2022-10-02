using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Leagues;

[Route("api/league/")]
public class LeagueController : Controller
{
    private readonly LeagueService _leagueService;

    public LeagueController(LeagueProvider leagueProvider, LeagueRepository leagueRepository)
    {
        _leagueService = new LeagueService(leagueProvider, leagueRepository);
    }

    [HttpGet("summonerId/{summonerId}")]
    public IActionResult GetSummonerId(string summonerId)
    {
        return Ok(_leagueService.GetSummonerId(summonerId));
    }
}