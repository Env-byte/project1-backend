using System;
using Newtonsoft.Json;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class TeamRequest
{

    [JsonProperty("hexes")]
    public List<Hex> Hexes { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("setId")]
    public string SetId { get; set; }
    [JsonProperty("isPublic")]
    public bool IsPublic { get; set; }
    [JsonProperty("guuid")]
    public string Guuid { get; set; }
}


