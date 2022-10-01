using System;
using Newtonsoft.Json;
using riot_backend.Api.Modules.TeamComps.Types;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class TeamRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("hexes")]
    public List<Hex> Hexes { get; set; }

    [JsonProperty("setId")]
    public string SetId { get; set; }

    [JsonProperty("isPublic")]
    public bool IsPublic { get; set; }

    [JsonProperty("isReadonly")]
    public bool IsReadonly { get; set; }

    [JsonProperty("guuid")]
    public string Guuid { get; set; }

    public static TeamRequest FromTeam(Team team)
    {
        return new TeamRequest()
        {
            Name = team.Name,
            Hexes = team.Champions.Select((champion) => Hex.FromChampion(champion)).ToList(),
            SetId = team.TftSetId,
            IsPublic = team.IsPublic,
            IsReadonly = team.IsReadonly,
            Guuid = team.Guuid
        };
    }
}


