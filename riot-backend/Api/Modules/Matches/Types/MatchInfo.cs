using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class MatchInfo
{
    /**
     * Unix timestamp.
     */
    [JsonProperty("game_datetime")]
    public long gameDatetime { get; set; }

    /**
     * Game length in seconds.
     */
    [JsonProperty("game_length")]
    public float gameLength { get; set; }

    /**
     * Game variation key. Game variations documented in TFT static data.
     */
    [JsonProperty("game_variation")]
    public string gameVariation { get; set; }

    /**
     * Game client version.
     */
    [JsonProperty("game_version")]
    public string gameVersion { get; set; }

    /**
     * Please refer to the League of Legends documentation.
     */
    [JsonProperty("queue_id")]
    public int queueId { get; set; }

    /**
     * Teamfight Tactics set number.
     */
    [JsonProperty("tft_set_number")]
    public int tftSetNumber { get; set; }
    
    /**
     * Teamfight Tactics game type.
     */
    [JsonProperty("tft_game_type")]
    public string tftGameType { get; set; }

    /**
     * Set core name.
     */
    [JsonProperty("tft_set_core_name")]

    public string tftSetCoreName { get; set; }
    
    /**
     * Game participants
     */
    [JsonProperty("participants")]
    public List<MatchParticipant> participants { get; set; }
    
}