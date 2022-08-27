using System.Data;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using riot_backend.helpers;
using Match = riot_backend.Api.Modules.Matches.Types.Match;

namespace riot_backend.Api.Modules.Matches;

public class MatchRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public MatchRepository(IConfiguration configuration)
    {
        _databaseFactory = new DatabaseFactory(configuration);
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
                ParameterName = "data", Value = json, NpgsqlDbType = NpgsqlDbType.Jsonb
            });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            foreach (var participant in match.metadata.participants)
            {
                using var cmd2 = new NpgsqlCommand();
                cmd2.CommandText =
                    "INSERT INTO summoner_matches (match_puuid,summoner_puuid) values (@match_puuid,@summoner_puuid);";
                cmd2.Connection = conn;
                cmd2.Parameters.Add(
                    new NpgsqlParameter { ParameterName = "match_puuid", Value = match.metadata.matchId });
                cmd2.Parameters.Add(new NpgsqlParameter
                    { ParameterName = "summoner_puuid", Value = participant });
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
            }
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

        Console.WriteLine("matches length: " + matches.Count);
        Console.WriteLine("puuids length: " + puuids.Count);


        return new Tuple<List<string>, List<Match>>(puuids, matches);
    }
}