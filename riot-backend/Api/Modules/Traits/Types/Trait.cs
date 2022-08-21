using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Traits.Types;

public class Trait
{
    /**
     * Trait key. This is the trait unique identifier
     */
    [JsonProperty("key")]
    public string key { get; set; }

    /**
     * Trait name.
     */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
     * Trait Description.
     */
    [JsonProperty("description")]
    public string description { get; set; }

    /**
     * Trait type. class or origin
     */
    [JsonProperty("type")]
    public string type { get; set; }

    /**
     * Trait sets. class or origin
     */
    [JsonProperty("sets")]
    public List<TraitSet> sets { get; set; }
}