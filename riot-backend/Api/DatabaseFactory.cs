using Npgsql;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api;

public interface IDatabaseWrapper
{
    public NpgsqlConnection GetDatabase();
}

public class DatabaseWrapper : IDatabaseWrapper
{
    public NpgsqlConnection GetDatabase()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ELoginType>("login_method");
        const string connString =
            "Host=127.0.0.1;Username=tompenn;Password=;Database=riot_project;SearchPath=public,extensions";
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        return conn;
    }
}

public class DatabaseFactory
{
    public static NpgsqlConnection Get<T>() where T : IDatabaseWrapper, new()
    {
        IDatabaseWrapper wrap = new T();
        return wrap.GetDatabase();
    }
}