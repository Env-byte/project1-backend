using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class Match
{
    /**
     * Match metadata.
     */
    [JsonProperty("metadata")]
    public MatchMetadata metadata { get; set; }

    /**
     * Match info.
     */
    [JsonProperty("info")]
    public MatchInfo info { get; set; }

}