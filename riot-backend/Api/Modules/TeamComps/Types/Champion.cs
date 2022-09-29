using System;
using Newtonsoft.Json;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class Champion
{

    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("championId")]
    public string ChampionId { get; set; }
    [JsonProperty("cost")]
    public int Cost { get; set; }
    [JsonProperty("traits")]
    public List<string> Traits { get; set; }
    [JsonProperty("items")]
    public List<short> Items { get; set; }
}