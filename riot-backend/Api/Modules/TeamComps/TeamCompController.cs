
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

    [HttpPut("/{guuid}/update")]
    public IActionResult Update(string guuid, [FromBody] TeamRequest teamRequest)
    {
        if (string.IsNullOrEmpty(guuid))
        {
            throw new ArgumentException($"'{nameof(guuid)}' cannot be null or empty.", nameof(guuid));
        }

        if (teamRequest is null)
        {
            throw new ArgumentNullException(nameof(teamRequest));
        }

        return Ok(_service.Update(guuid, teamRequest));
    }

    [HttpPut("/{guuid}/update/options")]
    public IActionResult UpdateOptions(string guuid, [FromBody] OptionsRequest optionsRequest)
    {
        if (string.IsNullOrEmpty(guuid))
        {
            throw new ArgumentException($"'{nameof(guuid)}' cannot be null or empty.", nameof(guuid));
        }

        if (optionsRequest is null)
        {
            throw new ArgumentNullException(nameof(optionsRequest));
        }

        return Ok(_service.UpdateOptions(guuid, optionsRequest));
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
        return Ok(TeamRequest.FromTeam(_service.Get(guuid)));
    }

    [HttpGet("list/{start}")]
    public IActionResult List(int start = 0)
    {
        return Ok(_service.GetPublic(start));
    }

    [HttpGet("list/user")]
    public IActionResult ListUser()
    {
        return Ok(_service.ListUser().Select((team) => TeamRequest.FromTeam(team)));
    }
}