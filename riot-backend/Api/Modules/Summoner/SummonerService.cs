using Npgsql;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerService
{
    private readonly DatabaseFactory _databaseFactory;

    public SummonerService(IConfiguration configuration)
    {
        _databaseFactory = new DatabaseFactory(configuration);
    }

    public Types.SummonerResponse GetSummoner()
    {
    }

    private void Insert(Types.Summoner summoner)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "INSERT INTO summoners (id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level) values (@id,@account_id,@puuid,@name,@profile_icon_id,@revision_date,@summoner_level);";
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = summoner.id });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "account_id", Value = summoner.accountId });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = summoner.puuid });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = summoner.name });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "profile_icon_id", Value = summoner.profileIconId });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "revision_date", Value = summoner.revisionDate });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "summoner_level", Value = summoner.summonerLevel });
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }

    private Types.Summoner Get(string id)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand($"SELECT id,first_name,last_name,email,type,token FROM users WHERE id= @id", conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Prepare();
        
        using var reader = cmd.ExecuteReader();
        
        //should only ever return one row
        if (!reader.HasRows) throw new KeyNotFoundException("Summoner with id:  " + id + " not found.");
       
        reader.Read();
        var summoner = new Types.Summoner()
        {
        };

        // get first user
        return summoner;

    }
}