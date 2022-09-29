
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.TeamComps.Models;

namespace riot_backend.Api.Modules.TeamComps;
[Route("api/team/")]
public class TeamCompsController : Controller
{
    private readonly TeamCompService _service;

    public TeamCompsController(TeamCompService teamCompsService)
    {
        _service = teamCompsService;
    }

    [HttpPut("update/{guuid}")]
    public IActionResult Save(string guuid, [FromBody] TeamRequest teamRequest)
    {
        if (string.IsNullOrEmpty(guuid))
        {
            throw new ArgumentException($"'{nameof(guuid)}' cannot be null or empty.", nameof(guuid));
        }

        if (teamRequest is null)
        {
            throw new ArgumentNullException(nameof(teamRequest));
        }

        return Ok(_service.Save(guuid, teamRequest));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] TeamRequest teamRequest)
    {
        teamRequest.Guuid = _service.Create(teamRequest);

        return Ok(teamRequest);
    }

    [HttpGet("{guuid}")]
    public IActionResult Get(string guuid)
    {
        return Ok(_service.Get(guuid));
    }

    [HttpGet("list/{start}")]
    public IActionResult Get(int start = 0)
    {
        return Ok(_service.GetPublic(start));
    }
}