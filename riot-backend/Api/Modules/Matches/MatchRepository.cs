using Newtonsoft.Json;
using Npgsql;
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
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO match (puuid,data) values (@puuid,@data);";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = match.metadata.matchId });
            cmd.Parameters.Add(new NpgsqlParameter
                { ParameterName = "data", Value = JsonConvert.SerializeObject(match) });
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            foreach (var participant in match.metadata.participants)
            {
                cmd.CommandText =
                    "INSERT INTO summoner_matches (match_puuid,summoner_puuid) values (@match_puuid,@summoner_puuid);";
                cmd.Connection = conn;
                cmd.Parameters.Add(
                    new NpgsqlParameter { ParameterName = "match_puuid", Value = match.metadata.matchId });
                cmd.Parameters.Add(new NpgsqlParameter
                    { ParameterName = "summoner_puuid", Value = participant });
                cmd.Prepare();
                cmd.ExecuteNonQuery();
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
        using var cmd = new NpgsqlCommand("SELECT puuid,data FROM match WHERE puuid in (@puuid)", conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = string.Join(',', puuids) });
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        //should only ever return one row

        if (!reader.HasRows)
        {
            return new Tuple<List<string>, List<Match>>(puuids, matches);
        }

        reader.Read();
        while (reader.Read())
        {
            var match = JsonConvert.DeserializeObject<Match>(reader.GetString(2));
            if (match == null) continue;
            matches.Add(match);
            puuids.Remove(match.metadata.matchId);
        }

        return new Tuple<List<string>, List<Match>>(puuids, matches);
    }
}