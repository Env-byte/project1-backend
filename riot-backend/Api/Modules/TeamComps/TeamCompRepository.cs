using System;
using Npgsql;
using riot_backend.Api.Modules.TeamComps.Models;
using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompRepository
{

    private readonly DatabaseFactory _databaseFactory;
    private readonly Header _header;

    public TeamCompRepository(DatabaseFactory databaseFactory, Header header)
    {
        _databaseFactory = databaseFactory;
        _header = header;
    }

    public string Insert(TeamRequest teamRequest)
    {

        using var conn = _databaseFactory.GetDatabase();
        var transaction = conn.BeginTransaction();
        var guuid = Guid.NewGuid().ToString();
        var query = @"INSERT INTO teams (
                    name,
                    tft_set,
                    is_public,
                    created_by,
                    created_on,
                    guuid
                    ) VALUES (
                    :name,
                    :tft_set,
                    :created_by,
                    NOW(),
                    :guuid
                    ); RETURNING id;";
        int? teamId = null;
        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = teamRequest.Name });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "tft_set", Value = teamRequest.SetId });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "created_by", Value = "" });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "guuid", Value = Guid.NewGuid().ToString() });
            cmd.Prepare();
            teamId = (int)cmd.ExecuteScalar();
        }

        if (teamId == null)
        {
            throw new InvalidOperationException($"'{nameof(teamId)}' cannot be null or empty.");
        }

        foreach (var hex in teamRequest.Hexes)
        {
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO team_champions (champion_id,team_id,hex,item_ids) values (:champion_id,:team_id,:hex,:item_ids) ON CONFLICT DO NOTHING;";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "champion_id", Value = hex.Champion.ChampionId });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "team_id", Value = teamId });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "hex", Value = hex.Position });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "item_ids", Value = hex.Champion.Items });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        return guuid;

    }
}