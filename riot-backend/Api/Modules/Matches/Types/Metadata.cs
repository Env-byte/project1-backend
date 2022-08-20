using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class Metadata
{
    /**
     * Match data version.
     */

    [JsonProperty("data_version")]
    public string dataVersion { get; set; }

    /**
     * Match id.
     */
    [JsonProperty("match_id")]
    public string matchId { get; set; }

    /**
     * A list of participant PUUIDs.
     */
    [JsonProperty("participants")]
    public List<string> participants { get; set; }
    
}