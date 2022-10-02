using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using riot_backend.Api.Modules.TeamComps.Models;
using riot_backend.Api.Modules.TeamComps.Types;
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
        var guuid = Guid.NewGuid().ToString();
        const string query = @"INSERT INTO teams (
                    name,
                    tft_set,
                    is_public,
                    created_by,
                    created_on,
                    guuid
                    ) VALUES (
                    :name,
                    :tft_set,
                    false,
                    :created_by,
                    NOW(),
                    :guuid
                    ) RETURNING id;";
        int teamId;
        using (var conn = _databaseFactory.GetDatabase())
        {
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = teamRequest.Name });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "tft_set", Value = teamRequest.SetId });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "created_by", Value = _header.User.id });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "guuid", Value = guuid });
            cmd.Prepare();
            teamId = (int?)(cmd.ExecuteScalar()) ?? throw new InvalidOperationException($"'{nameof(teamId)}' cannot be null or empty."); ;
        }

        InsertChampions(teamId, teamRequest);

        return guuid;

    }

    public Team Get(string guuid)
    {
        using var conn = _databaseFactory.GetDatabase();

        Team team;

        using (var cmd = new NpgsqlCommand())
        {
            var query = @"SELECT id,name,tft_set,is_public,created_by,created_on,guuid FROM teams where guuid=:guuid;";
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "guuid", Value = guuid });
            cmd.Prepare();
            using var reader = cmd.ExecuteReader();
            //should only ever return one row
            if (!reader.HasRows) throw new KeyNotFoundException("Could not find team using " + guuid);
            reader.Read();
            team = Team.FromSqlReader(reader);
        }

        using (var cmd = new NpgsqlCommand())
        {
            const string query = @"SELECT champion_id,hex,item_ids FROM team_champions where team_id=:team_id;";
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "team_id", Value = team.Id });
            cmd.Prepare();
            using var reader = cmd.ExecuteReader();
            
            if (!reader.HasRows) return team;
            
            var champions = new List<TeamChampion>();
            while (reader.Read())
            {
                champions.Add(TeamChampion.FromSqlReader(reader));
            }
            team.Champions = champions;
        }

        return team;
    }

    public void UpdateOptions(string guuid, OptionsRequest optionsRequest)
    {
        using var conn = _databaseFactory.GetDatabase();

        using var cmd = new NpgsqlCommand();
        const string query = @"UPDATE teams SET name=:name,isPublic=:isPublic where guuid=:guuid;";
        cmd.CommandText = query;
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = optionsRequest.Name });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "isPublic", Value = optionsRequest.IsPublic });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "guuid", Value = guuid });
        cmd.Prepare();
        var rowsAffected = cmd.ExecuteNonQuery();
        //should only affect one row
        if (rowsAffected == 0) throw new KeyNotFoundException("Could not find team using " + guuid);
    }

    public void Update(int id, TeamRequest teamRequest)
    {
        using (var conn = _databaseFactory.GetDatabase())
        {
            using var cmd = new NpgsqlCommand();
            const string query = @"UPDATE teams SET name=:name,isPublic=:isPublic where id=:id;";
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = teamRequest.Name });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "isPublic", Value = teamRequest.IsPublic });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "guuid", Value = id });
            cmd.Prepare();
            var rowsAffected = cmd.ExecuteNonQuery();
            //should only affect one row
            if (rowsAffected == 0) throw new KeyNotFoundException("Could not find team using " + id);
        }

        InsertChampions(id, teamRequest);

    }

    private void InsertChampions(int id, TeamRequest teamRequest)
    {
        using var conn = _databaseFactory.GetDatabase();
        var transaction = conn.BeginTransaction();

        using (var cmd = new NpgsqlCommand())
        {
            cmd.CommandText = "DELETE FROM team_champions WHERE team_id=:team_id";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "team_id", Value = id });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        foreach (var hex in teamRequest.Hexes)
        {
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO team_champions (champion_id,team_id,hex,item_ids) values (:champion_id,:team_id,:hex,:item_ids) ON CONFLICT DO NOTHING;";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "champion_id", Value = hex.Champion.ChampionId });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "team_id", Value = id });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "hex", Value = hex.Position });
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "item_ids", Value = hex.Champion.Items });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        transaction.Commit();
    }

    internal List<Team> ListUserTeams(int id)
    {
        using var conn = _databaseFactory.GetDatabase();
        var teams = new List<Team>();
        using var cmd = new NpgsqlCommand();
        const string query = @"select id,
                           name,
                           tft_set,
                           is_public,
                           created_by,
                           created_on,
                           guuid,
                           cast(json_agg(
                                   jsonb_build_object(
                                           'champion_id', tc.champion_id,
                                           'hex', tc.hex,
                                           'item_ids', tc.item_ids
                                       )
                               ) AS text) as champions
                    from teams as t
                             left join team_champions tc on t.id = tc.team_id
                    WHERE t.created_by=:id
                    group by t.id, tc.champion_id ;";

        cmd.CommandText = query;
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var team = Team.FromSqlReader(reader);
            team.Champions = JsonConvert.DeserializeObject<List<TeamChampion>>(reader.GetString(7)) ?? new List<TeamChampion>();
            teams.Add(team);
        }

        return teams;
    }
}