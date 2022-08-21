using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Traits.Types;

public class TraitSet
{
    /**
     * Trait style. the colour of the badge when activated
     */
    [JsonProperty("style")]
    public string style { get; set; }

    /**
     * Trait min number of unique units to activate
     */
    [JsonProperty("min")]
    public string min { get; set; }

    /**
     * Trait max number of unique units before next level
     */
    [JsonProperty("max")]
    public string max { get; set; }
}