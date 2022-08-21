using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class MatchTrait
{
    /**
     * Trait name.
     */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
     * Number of units with this trait.
     */
    [JsonProperty("num_units")]
    public int numUnits { get; set; }

    /**
     * Current style for this trait. (0 = No style, 1 = Bronze, 2 = Silver, 3 = Gold, 4 = Chromatic)
     */
    [JsonProperty("style")]
    public int style { get; set; }

    /**
     * Current active tier for the trait.
     */
    [JsonProperty("tier_current")]
    public int tierCurrent { get; set; }

    /**
     * Total tiers for the trait.
     */
    [JsonProperty("tier_total")]
    public int tierTotal { get; set; }

}