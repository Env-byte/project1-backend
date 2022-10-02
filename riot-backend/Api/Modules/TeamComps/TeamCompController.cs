using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.TeamComps.Models;
using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.TeamComps;

[Route("api/team/")]
public class TeamCompsController : Controller
{
    private readonly TeamCompService _service;

    public TeamCompsController(TeamCompRepository teamCompRepository, Header header)
    {
        _service = new TeamCompService(teamCompRepository, header);
    }

    [HttpPut("/{guuid}/update")]
    public IActionResult Update(string guuid, [FromBody] TeamRequest teamRequest)
    {
        if (string.IsNullOrEmpty(guuid))
        {
            throw new BadRequestException($"'{nameof(guuid)}' cannot be null or empty.");
        }

        if (teamRequest is not null) return Ok(_service.Update(guuid, teamRequest));

        throw new BadRequestException($"'{nameof(teamRequest)}' cannot be null or empty.");
    }

    [HttpPut("/{guuid}/update/options")]
    public IActionResult UpdateOptions(string guuid, [FromBody] OptionsRequest optionsRequest)
    {
        if (string.IsNullOrEmpty(guuid))
        {
            throw new BadRequestException($"'{nameof(guuid)}' cannot be null or empty.");
        }

        if (optionsRequest is null)
        {
            throw new BadRequestException($"'{nameof(optionsRequest)}' cannot be null or empty.");
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