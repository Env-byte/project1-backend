using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Items.Types;

public class Item
{
    /**
    * Item id.
    */
    [JsonProperty("id")]
    public string id { get; set; }

    /**
     * Item Name
     */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
     * Item Description
     */
    [JsonProperty("description")]
    public string description { get; set; }

    /**
     * Is the item unique 
     */
    [JsonProperty("isUnique")]
    public string isUnique { get; set; }
}