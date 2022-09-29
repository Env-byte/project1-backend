using System;
using Newtonsoft.Json;
using riot_backend.Api.Modules.TeamComps.Types;

namespace riot_backend.Api.Modules.TeamComps.Models;
public class Hex
{
    [JsonProperty("position")]
    public int Position { get; set; }
    [JsonProperty("champion")]
    public Champion Champion { get; set; }

    public static Hex FromChampion(TeamChampion champion)
    {
        return new Hex()
        {
            Position = champion.Hex,
            Champion = new Champion()
            {
                ChampionId = champion.CharacterId,
                Items = champion.ItemIds
            }
        };
    }
}


