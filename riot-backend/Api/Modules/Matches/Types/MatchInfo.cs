namespace riot_backend.Api.Modules.Matches.Types;

public class MatchInfo
{
    /**
     * Unix timestamp.
     */
    public long gameDatetime { get; set; }

    /**
     * Game length in seconds.
     */
    public float gameLength { get; set; }

    /**
     * Game variation key. Game variations documented in TFT static data.
     */
    public string gameVariation { get; set; }

    /**
     * Game client version.
     */
    public string gameVersion { get; set; }

    /**
     * Please refer to the League of Legends documentation.
     */
    public int queueId { get; set; }

    /**
     * Teamfight Tactics set number.
     */
    public int tftSetNumber { get; set; }

    /**
     * Game participants
     */
    public MatchParticipant[] participants { get; set; }

    public static MatchInfo FromJson(dynamic json)

    {
     return new MatchInfo
     {
      gameDatetime = json.game_datetime,
      gameLength = json.game_length,
      gameVariation = json.game_variation,
      gameVersion = json.game_version,
      queueId = json.queue_id,
      tftSetNumber = json.tft_set_number,
      participants = new MatchParticipant[]
      {
      }
     };
    }
}