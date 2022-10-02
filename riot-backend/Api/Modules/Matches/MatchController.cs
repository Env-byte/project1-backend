using System.Text.RegularExpressions;

namespace riot_backend.Api.Modules.Matches;

using Microsoft.AspNetCore.Mvc;

[Route("api/match/")]
public class MatchesController : Controller
{
    private readonly ILogger<MatchesController> _logger;
    private readonly MatchService _matchService;

    public MatchesController(ILogger<MatchesController> logger, MatchRepository matchRepository,
        MatchProvider matchProvider)
    {
        _logger = logger;
        _matchService = new MatchService(matchRepository, matchProvider);
    }

    [HttpGet("summonerPuuid/{puuid}")]
    public IActionResult GetMatchPuuid(string puuid)
    {
        return Ok(_matchService.GetMatchPuuids(puuid));
    }

    [HttpGet("{puuid}")]
    public IActionResult GetMatch(string puuid)
    {
        return Ok(_matchService.GetMatch(puuid));
    }
}