using Newtonsoft.Json;
using Npgsql;
using System;
namespace riot_backend.Api.Modules.TeamComps.Types;
public class TeamChampion
{
    [JsonProperty("champion_id")]
    public string CharacterId { get; set; }

    [JsonProperty("hex")]
    public int Hex { get; set; }

    [JsonProperty("items")]
    public List<short> ItemIds { get; set; }
    public static TeamChampion FromSqlReader(NpgsqlDataReader reader)
    {
        return new TeamChampion()
        {
            CharacterId = reader.GetString(0),
            Hex = reader.GetInt32(1),
            ItemIds = new List<short>((short[])reader.GetValue(2))
        };
    }
}


