namespace riot_backend.Api.Modules.Matches.Types;

public class Match
{
    /**
     * Match metadata.
     */
    public Metadata metadata { get; set; }

    /**
     * Match info.
     */
    public MatchInfo info { get; set; }

    public static Match FromJson(dynamic json)
    {
        return new Match
        {
            metadata = Metadata.FromJson(json.metadata),
            info = MatchInfo.FromJson(json.info)
        };
    }
}