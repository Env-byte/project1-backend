using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Champions.Types;

public class Champion
{
    /**
     * Item id.
    */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
    * Item id.
    */
    [JsonProperty("championId")]
    public string championId { get; set; }

    /**
    * Item id.
    */
    [JsonProperty("cost")]
    public int cost { get; set; }

    /**
    * Item id.
    */
    [JsonProperty("traits")]
    public List<string> traits { get; set; }
}