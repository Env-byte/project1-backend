using Npgsql;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api;

public class DatabaseFactory
{
    private readonly IConfiguration _configuration;

    public DatabaseFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public NpgsqlConnection GetDatabase()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ELoginType>("extensions.login_method");

        var conn = new NpgsqlConnection(_configuration.GetConnectionString("database"));
        conn.Open();
        return conn;
    }
}