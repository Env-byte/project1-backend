using System.Text.RegularExpressions;

namespace riot_backend.Api.Modules.Matches;

using Microsoft.AspNetCore.Mvc;

[Route("api/match/")]
public class MatchesController : Controller
{
    private readonly ILogger<MatchesController> _logger;
    private readonly MatchService _matchService;

    public MatchesController(ILogger<MatchesController> logger, MatchService matchService)
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpGet("{summonerPuuid}")]
    public IActionResult GetMatchPuuid(string summonerPuuid)
    {
        return Ok(_matchService.GetMatches(_matchService.GetMatchPuuids(summonerPuuid)));
    }

    [HttpGet("{matchPuuid}")]
    public IActionResult GetMatch(string matchPuuid)
    {
        return Ok(_matchService.GetMatch(matchPuuid));
    }

    [HttpGet("{summonerName}")]
    public IActionResult GetMatchesByName(string summonerName)
    {
        return Ok(_matchService.GetMatchesByName(summonerName));
    }
}