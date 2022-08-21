using Newtonsoft.Json;

namespace riot_backend.Api.Modules.Matches.Types;

public class MatchParticipant
{
    /**
     * Participant's companion.
     */
    [JsonProperty("companion")]
    public Companion companion { get; set; }

    /**
     * Gold left after participant was eliminated.
     */
    [JsonProperty("gold_left")]
    public int goldLeft { get; set; }

    /**
     * The round the participant was eliminated in. Note: If the player was eliminated in stage 2-1 their last_round would be 5.
     */
    [JsonProperty("last_round")]
    public int lastRound { get; set; }

    /**
     * Participant Little Legend level. Note: This is not the number of active units.
     */
    [JsonProperty("level")]
    public int level { get; set; }

    /**
     * Participant placement upon elimination.
     */
    [JsonProperty("placement")]
    public int placement { get; set; }

    /**
     * Number of players the participant eliminated.
     */
    [JsonProperty("players_eliminated")]
    public int playersEliminated { get; set; }

    /**
     * Damage the participant dealt to other players.
     */
    [JsonProperty("total_damage_to_players")]
    public int totalDamageToPlayers { get; set; }


    [JsonProperty("puuid")] 
    public string puuid { get; set; }

    /**
     * The number of seconds before the participant was eliminated.
     */
    [JsonProperty("time_eliminated")]
    public float timeEliminated { get; set; }

    /**
     * A complete list of traits for the participant's active units.
     */
    [JsonProperty("traits")]
    public List<MatchTrait> traits { set; get; }

    /**
     * A list of active units for the participant.
     */
    [JsonProperty("units")]
    public List<MatchUnit> units { set; get; }

}