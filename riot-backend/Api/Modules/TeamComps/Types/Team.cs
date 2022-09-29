using System;
namespace riot_backend.Api.Modules.TeamComps.Types;

public class Team
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string TftSetId { get; set; }
    public string Guuid { get; set; }
    public bool IsPublic { get; set; }
    public List<TeamChampion> Champions { get; set; }
}