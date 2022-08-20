namespace riot_backend.Api.Modules.Matches;

using Microsoft.AspNetCore.Mvc;

public class MatchesController : Controller
{
    private readonly ILogger<MatchesController> _logger;
    private readonly MatchLoader _loader;

    public MatchesController(ILogger<MatchesController> logger, IHttpClientWrapper http)
    {
        _logger = logger;
        _loader = new MatchLoader(http);
    }

    [HttpGet("api/matches/name")]
    public Types.Match GetMatch(string name)
    {
        return _loader.GetMatch(name);
    }

    [HttpGet("api/matches/puuid")]
    public List<string> GetMatches(string puuid)
    {
        return _loader.GetMatches(puuid);
    }
}