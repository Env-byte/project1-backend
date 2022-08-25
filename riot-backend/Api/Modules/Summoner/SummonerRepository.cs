using Npgsql;
using riot_backend.Api.Modules.Summoner.Types;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public SummonerRepository(IConfiguration configuration)
    {
        _databaseFactory = new DatabaseFactory(configuration);
    }


    public void Insert(Types.Summoner summoner)
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

    public Types.Summoner Get(string id)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                $"SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE id= @id",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();

        //should only ever return one row
        if (!reader.HasRows) throw new KeyNotFoundException("Summoner with id:  " + id + " not found.");
        reader.Read();

        return Types.Summoner.FromSqlReader(reader);
    }

    public Types.Summoner GetByName(string name)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                $"SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE lower(name)= lower(@name)",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = name });
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        //should only ever return one row
        if (!reader.HasRows) throw new KeyNotFoundException("Summoner with name:  " + name + " not found.");
        reader.Read();

        return Types.Summoner.FromSqlReader(reader);
    }

    public Types.Summoner GetByPuuid(string puuid)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE puuid= @puuid",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = puuid });
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();

        //should only ever return one row
        if (!reader.HasRows) throw new KeyNotFoundException("Summoner with name:  " + puuid + " not found.");
        reader.Read();

        return Types.Summoner.FromSqlReader(reader);
    }

    public void Update(string puuid, Types.Summoner summoner)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "UPDATE summoners SET name=:name, profile_icon_id=:profile_icon_id, revision_date=:revision_date, summoner_level=:summoner_level,last_update=:last_update FROM summoners WHERE puuid= @puuid",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "puuid", Value = puuid });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "name", Value = summoner.name });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "profile_icon_id", Value = summoner.profileIconId });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "revision_date", Value = summoner.revisionDate });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "summoner_level", Value = summoner.summonerLevel });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "last_update", Value = summoner.lastUpdate });
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }
}