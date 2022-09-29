using Npgsql;
using NpgsqlTypes;
using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerRepository
{
    private readonly DatabaseFactory _databaseFactory;
    private readonly Header _header;

    public SummonerRepository(DatabaseFactory databaseFactory, Header header)
    {
        _databaseFactory = databaseFactory;
        _header = header;
    }

    public Types.Summoner? Get(string id)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                $"SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE id= @id",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "id", Value = id});
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();

        //should only ever return one row
        if (!reader.HasRows) return null;
        reader.Read();
        return Types.Summoner.FromSqlReader(reader);
    }

    public Types.Summoner? GetByName(string name)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                $"SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE lower(replace(name,' ',''))= lower(@name) AND region=@region",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "name", Value = name});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "region", Value = _header.Region});
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        //should only ever return one row
        if (!reader.HasRows) return null;
        reader.Read();
        return Types.Summoner.FromSqlReader(reader);
    }

    public Types.Summoner? GetByPuuid(string puuid)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE puuid=@puuid  AND region=@region",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "puuid", Value = puuid});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "region", Value = _header.Region});
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();

        //should only ever return one row
        if (!reader.HasRows)
        {
            return null;
        }

        reader.Read();
        return Types.Summoner.FromSqlReader(reader);
    }

    public void Update(string puuid, Types.Summoner summoner)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "UPDATE summoners SET name=:name, profile_icon_id=:profile_icon_id, revision_date=:revision_date, summoner_level=:summoner_level,last_update=:last_update WHERE puuid= @puuid AND  AND region=@region",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "puuid", Value = puuid});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "region", Value = _header.Region});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "name", Value = summoner.name});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "profile_icon_id", Value = summoner.profileIconId});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "revision_date", Value = summoner.revisionDate});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "summoner_level", Value = summoner.summonerLevel});
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "last_update", Value = summoner.lastUpdate});
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }

    public Tuple<List<string>, List<Types.Summoner>> GetByPuuid(List<string> puuids)
    {
        var summoners = new List<Types.Summoner>();

        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                $"SELECT id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,last_update FROM summoners WHERE puuid =any(:puuid) AND region= :region",
                conn);
        cmd.Parameters.AddWithValue("puuid", NpgsqlDbType.Text | NpgsqlDbType.Array, puuids.ToArray());
        cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "region", Value = _header.Region});

        cmd.Prepare();
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var summoner = Types.Summoner.FromSqlReader(reader);
            summoners.Add(summoner);
            puuids.Remove(summoner.puuid);
        }

        return new Tuple<List<string>, List<Types.Summoner>>(puuids, summoners);
    }

    public void Insert(List<Types.Summoner> newSummoners)
    {
        using var conn = _databaseFactory.GetDatabase();
        var transaction = conn.BeginTransaction();
        foreach (var summoner in newSummoners)
        {
            using var cmd = new NpgsqlCommand();
            cmd.CommandText =
                "INSERT INTO summoners (id, account_id, puuid, name, profile_icon_id, revision_date, summoner_level,region) values (@id,@account_id,@puuid,@name,@profile_icon_id,@revision_date,@summoner_level,@region) ON CONFLICT DO NOTHING;";
            cmd.Connection = conn;
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "id", Value = summoner.id});
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "account_id", Value = summoner.accountId});
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "puuid", Value = summoner.puuid});
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "name", Value = summoner.name});
            cmd.Parameters.Add(
                new NpgsqlParameter {ParameterName = "profile_icon_id", Value = summoner.profileIconId});
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "revision_date", Value = summoner.revisionDate});
            cmd.Parameters.Add(new NpgsqlParameter
                {ParameterName = "summoner_level", Value = summoner.summonerLevel});
            cmd.Parameters.Add(new NpgsqlParameter {ParameterName = "region", Value = _header.Region});

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
    }


    public void Insert(Types.Summoner summoner)
    {
        Insert(new List<Types.Summoner> {summoner});
    }
}