namespace riot_backend.Api.Modules.Matches.Types;

public class MatchParticipant
{
    /**
     * Participant's companion.
     */
    public Companion companion { get; set; }

    /**
     * Gold left after participant was eliminated.
     */
    public int goldLeft { get; set; }

    /**
     * The round the participant was eliminated in. Note: If the player was eliminated in stage 2-1 their last_round would be 5.
     */
    public int lastRound { get; set; }

    /**
     * Participant Little Legend level. Note: This is not the number of active units.
     */
    public int level { get; set; }

    /**
     * Participant placement upon elimination.
     */
    public int placement { get; set; }

    /**
     * Number of players the participant eliminated.
     */
    public int playersEliminated { get; set; }

    /**
     * Damage the participant dealt to other players.
     */
    public int totalDamageToPlayers { get; set; }


    public string puuid { get; set; }

    /**
     * The number of seconds before the participant was eliminated.
     */
    public float timeEliminated { get; set; }

    /**
     * A complete list of traits for the participant's active units.
     */
    public List<Traits> traits { set; get; }

    /**
     * A list of active units for the participant.
     */
    public Units units { set; get; }

    public static MatchParticipant FromJson(dynamic json)
    {
        var thisTraits = new List<Traits>();
        foreach (var trait in json.traits)
        {
         thisTraits.Add(Traits.FromJson(trait));
        }

        return new MatchParticipant
        {
            companion = new Companion(),
            goldLeft = json.gold_left,
            lastRound = json.last_round,
            level = json.level,
            placement = json.placement,
            playersEliminated = json.players_eliminated,
            totalDamageToPlayers = json.total_damage_to_players,
            puuid = json.puuid,
            timeEliminated = json.time_eliminated,
            traits = thisTraits
        };
    }
}