using Newtonsoft.Json;
using Npgsql;

namespace riot_backend.Api.Modules.Summoner.Types;

public class Summoner : BaseSuccessResponse
{
    /**
     * Encrypted summoner ID. Max length 63 characters.
     */
    [JsonProperty("id")]
    public string id { get; set; }

    /**
     * Encrypted account ID. Max length 56 characters.
     */
    [JsonProperty("accountId")]
    public string accountId { get; set; }

    /**
     * Encrypted PUUID. Exact length of 78 characters.
     */
    [JsonProperty("puuid")]
    public string puuid { get; set; }

    /**
     * Summoner name.
     */
    [JsonProperty("name")]
    public string name { get; set; }

    /**
     * ID of the summoner icon associated with the summoner.
     */
    [JsonProperty("profileIconId")]
    public int profileIconId { get; set; }

    /**
     * Date summoner was last modified specified as epoch milliseconds.
     * The following events will update this timestamp: summoner name change,
     * summoner level change, or profile icon change.
     */
    [JsonProperty("revisionDate")]
    public long revisionDate { get; set; }

    /**
     * Summoner level associated with the summoner.
     */
    [JsonProperty("summonerLevel")]
    public long summonerLevel { get; set; }

    /**
     * url to the users icon
     */
    public string iconUrl =>
        "https://ddragon.leagueoflegends.com/cdn/12.15.1/img/profileicon/" + profileIconId + ".png";

    public DateTime lastUpdate { get; set; }

    public static Summoner FromSqlReader(NpgsqlDataReader reader)
    {
        return new Summoner()
        {
            id = reader.GetString(0),
            accountId = reader.GetString(1),
            puuid = reader.GetString(2),
            name = reader.GetString(3),
            profileIconId = reader.GetInt32(4),
            revisionDate = reader.GetInt64(5),
            summonerLevel = reader.GetInt64(6),
            lastUpdate = reader.GetDateTime(7)
        };
    }
}