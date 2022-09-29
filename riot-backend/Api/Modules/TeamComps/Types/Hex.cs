using System;
using Newtonsoft.Json;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class Hex
{
    [JsonProperty("hex")]
    public int Position { get; set; }
    [JsonProperty("champion")]
    public Champion Champion { get; set; }
}


