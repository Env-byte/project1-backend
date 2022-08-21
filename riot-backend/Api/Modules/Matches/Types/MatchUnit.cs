using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class MatchUnit
{
    /**
     * A list of the unit's items. Please refer to the Teamfight Tactics documentation for item ids.
     */
    [JsonProperty("items")]
    public List<int> items { get; set; }

    /**
     * This field was introduced in patch 9.22 with data_version 2.
     */

    [JsonProperty("character_id")]
    public string characterId { get; set; }

    /**
     * If a unit is chosen as part of the Fates set mechanic,
     * the chosen trait will be indicated by this field. Otherwise this field is excluded from the response.
     */
    [JsonProperty("chosen")]
    public string chosen { get; set; }

    /**
     * Unit name. This field is often left blank.
     */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
     * Unit rarity. This doesn't equate to the unit cost.
     */
    [JsonProperty("rarity")]
    public int rarity { get; set; }

    /**
     * Unit tier.
     */
    [JsonProperty("tier")]
    public int tier { get; set; }

   
}