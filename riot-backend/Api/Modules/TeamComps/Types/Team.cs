using Npgsql;
using System;
namespace riot_backend.Api.Modules.TeamComps.Types;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TftSetId { get; set; }
    public string Guuid { get; set; }
    public bool IsPublic { get; set; }
    public int CreatedBy { get; set; }
    public bool IsReadonly { get; set; }
    public List<TeamChampion> Champions { get; set; }

    public static Team FromSqlReader(NpgsqlDataReader reader)
    {
        return new Team()
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            TftSetId = reader.GetString(2),
            IsPublic = reader.GetBoolean(3),
            CreatedBy = reader.GetInt32(4),
            Guuid = reader.GetString(6),
        };
    }
}