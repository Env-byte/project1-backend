using Npgsql;
using System;
namespace riot_backend.Api.Modules.TeamComps.Types;
public class TeamChampion
{
    public string CharacterId { get; set; }
    public int Hex { get; set; }
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


