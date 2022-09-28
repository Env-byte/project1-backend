using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompsController
{
    private readonly TeamCompService _service;

    public TeamCompsController(TeamCompService teamCompsService)
    {
        _service = teamCompsService;
    }

    [HttpGet("team/save/{id}")]
    public IActionResult Save(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        return Ok(_service.GetByName(name));
    }
}