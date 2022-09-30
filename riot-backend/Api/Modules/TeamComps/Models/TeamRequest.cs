using System;
using Newtonsoft.Json;
using riot_backend.Api.Modules.TeamComps.Types;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class OptionsRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("isPublic")]
    public bool IsPublic { get; set; }
}