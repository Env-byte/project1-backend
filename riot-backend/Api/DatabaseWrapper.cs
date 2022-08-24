using Npgsql;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api;

public class DatabaseWrapper
{
    public static NpgsqlConnection GetDatabase()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ELoginType>("login_method");
        const string connString = "Host=127.0.0.1;Username=tompenn;Password=;Database=riot_project";
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        return conn;
    }
}