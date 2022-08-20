namespace riot_backend.Api.Modules.Matches.Types;

public class Metadata
{
    /**
     * Match data version.
     */
    public string dataVersion { get; set; }

    /**
     * Match id.
     */
    public string matchId { get; set; }

    /**
     * A list of participant PUUIDs.
     */
    public string[] participants { get; set; }

    public static Metadata FromJson(dynamic json)
    {
        return new Metadata
        {
            dataVersion = json.data_version,
            matchId = json.match_id,
            participants = json.participants
        };
    }
}