
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.TeamComps.Models;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompsController : Controller
{
    private readonly TeamCompService _service;

    public TeamCompsController(TeamCompService teamCompsService)
    {
        _service = teamCompsService;
    }

    [HttpPut("team/update/{id}")]
    public IActionResult Save(string id, [FromBody] TeamRequest teamRequest)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        if (teamRequest is null)
        {
            throw new ArgumentNullException(nameof(teamRequest));
        }

        teamRequest.Guuid = _service.Save(id, teamRequest);

        return Ok();
    }

    [HttpPost("team/create")]
    public IActionResult Create([FromBody] TeamRequest teamRequest)
    {
        return Ok(_service.Create(teamRequest));
    }
}