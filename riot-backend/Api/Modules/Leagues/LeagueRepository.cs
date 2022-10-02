using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using riot_backend.Api.Modules.Leagues.Types;

namespace riot_backend.Api.Modules.Leagues;

public class LeagueRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public LeagueRepository(DatabaseFactory database)
    {
        _databaseFactory = database;
    }

    public List<League> Get(string summonerId)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "SELECT data FROM summoner_league WHERE summoner_id=:summoner_id;";
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "summoner_id", Value = summonerId });
        cmd.Prepare();
        var data = cmd.ExecuteScalar();
        if (data == null) return new List<League>();
        var json = (string)data;
        if (string.IsNullOrEmpty(json)) return new List<League>();

        return JsonConvert.DeserializeObject<List<League>>(json) ??
               throw new InvalidOperationException("League could not be deserialized");
    }

    public void Insert(List<League> leagues)
    {
        using var conn = _databaseFactory.GetDatabase();
        var json = JsonConvert.SerializeObject(leagues);
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "INSERT INTO summoner_league (summoner_id,data) values (@puuid,@data);";
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = leagues[0].SummonerId });
        cmd.Parameters.Add(new NpgsqlParameter
        {
            ParameterName = "data", Value = json, NpgsqlDbType = NpgsqlDbType.Text
        });
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }
}