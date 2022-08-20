namespace riot_backend.Api.Modules.Summoner.Types;

public class Summoner
{
    /**
     * Encrypted summoner ID. Max length 63 characters.
     */
    public string id { get; set; }

    /**
     * Encrypted account ID. Max length 56 characters.
     */
    public string accountId { get; set; }

    /**
     * Encrypted PUUID. Exact length of 78 characters.
     */
    public string puuid { get; set; }
    
    /**
     * Summoner name.
     */
    public string name { get; set; }

    /**
     * ID of the summoner icon associated with the summoner.
     */
    public int profileIconId { get; set; }

    /**
     * Date summoner was last modified specified as epoch milliseconds.
     * The following events will update this timestamp: summoner name change,
     * summoner level change, or profile icon change.
     */
    public long revisionDate { get; set; }
    
    /**
     * Summoner level associated with the summoner.
     */
    public long summonerLevel { get; set; }

    /**
     * url to the users icon
     */
    public string iconUrl { get; set; }

    public static Summoner FromJson(dynamic json)
    {
        return new Summoner
        {
            id = json.id,
            accountId = json.accountId,
            puuid = json.puuid,
            name = json.name,
            profileIconId = json.profileIconId,
            revisionDate = json.revisionDate,
            summonerLevel = json.summonerLevel,
            iconUrl = "https://ddragon.leagueoflegends.com/cdn/12.15.1/img/profileicon/" + json.profileIconId + ".png"
        };
    }
}