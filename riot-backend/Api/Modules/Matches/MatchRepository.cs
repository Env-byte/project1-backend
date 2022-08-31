using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using Match = riot_backend.Api.Modules.Matches.Types.Match;

namespace riot_backend.Api.Modules.Matches;

public class MatchRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public MatchRepository(DatabaseFactory database)
    {
        _databaseFactory = database;
    }

    public Match? GetMatch(string puuid)
    {
        return GetMatches(new List<string> { puuid }).Item2.ElementAtOrDefault(0);
    }

    public void Insert(Match matchInfo)
    {
        Insert(new List<Match> { matchInfo });
    }

    public void Insert(List<Match> matches)
    {
        using var conn = _databaseFactory.GetDatabase();
        var transaction = conn.BeginTransaction();
        foreach (var match in matches)
        {
            var json = JsonConvert.SerializeObject(match);
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO match (puuid,data) values (@puuid,@data);";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = match.metadata.matchId });
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "data", Value = json, NpgsqlDbType = NpgsqlDbType.Text
            });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    /**
     * @T1 list of ids not found
     * @T2 list of found matches
     */
    public Tuple<List<string>, List<Match>> GetMatches(List<string> puuids)
    {
        var matches = new List<Match>();

        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand($"SELECT puuid,CAST(data AS text) FROM match WHERE  puuid = ANY(:puuid);",
            conn);
        cmd.Parameters.AddWithValue("puuid", NpgsqlDbType.Text | NpgsqlDbType.Array, puuids.ToArray());
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var match = JsonConvert.DeserializeObject<Match>(reader.GetString(1));
            if (match == null) continue;
            matches.Add(match);
            puuids.Remove(match.metadata.matchId);
        }

        return new Tuple<List<string>, List<Match>>(puuids, matches);
    }

    public List<string> GetMatchPuuids(string summonerPuuid)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "SELECT match_puuid FROM summoner_matches where summoner_puuid=@puuid;";
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = summonerPuuid });
        cmd.Prepare();

        var matchPuuids = new List<string>();

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            matchPuuids.Add(reader.GetString(0));
        }

        return matchPuuids;
    }


    public void Remove(string summonerPuuid)
    {
        using var conn = _databaseFactory.GetDatabase();
        using (var cmd = new NpgsqlCommand("DELETE FROM summoner_matches where summoner_puuid=@puuid;", conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = summonerPuuid });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        using (var cmd = new NpgsqlCommand(
                   "DELETE FROM match where puuid not in (SELECT match_puuid FROM summoner_matches );", conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = summonerPuuid });
            cmd.ExecuteNonQuery();
        }
    }

    public void InsertMatchPuuids(string summonerPuuid, List<string> matchPuuids)
    {
        using var conn = _databaseFactory.GetDatabase();
        foreach (var match in matchPuuids)
        {
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO summoner_matches (match_puuid,summoner_puuid) values (@match_puuid,@summoner_puuid);";
            cmd.Connection = conn;
            cmd.Parameters.Add(
                new NpgsqlParameter { ParameterName = "match_puuid", Value = match });
            cmd.Parameters.Add(new NpgsqlParameter
                { ParameterName = "summoner_puuid", Value = summonerPuuid });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}