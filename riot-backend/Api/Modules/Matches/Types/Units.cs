namespace riot_backend.Api.Modules.Matches.Types;

public class Units
{
    /**
     * A list of the unit's items. Please refer to the Teamfight Tactics documentation for item ids.
     */
    public int[] items { get; set; }

    /**
     * This field was introduced in patch 9.22 with data_version 2.
     */
    public string characterId { get; set; }

    /**
     * If a unit is chosen as part of the Fates set mechanic,
     * the chosen trait will be indicated by this field. Otherwise this field is excluded from the response.
     */
    public string chosen { get; set; }

    /**
     * Unit name. This field is often left blank.
     */
    public string name { get; set; }

    /**
     * Unit rarity. This doesn't equate to the unit cost.
     */
    public int rarity { get; set; }

    /**
     * Unit tier.
     */
    public int tier { get; set; }

    public static Units FromJson(dynamic json)
    {
        return new Units
        {
            items = json.items,
            characterId = json.character_id,
            chosen = json.chosen,
            name = json.name,
            rarity = json.rarity,
            tier = json.tier
        };
    }
}