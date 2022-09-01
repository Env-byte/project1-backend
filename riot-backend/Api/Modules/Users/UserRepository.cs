using Dapper;
using Newtonsoft.Json;
using Npgsql;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

public class UserRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public UserRepository(IConfiguration configuration)
    {
        _databaseFactory = new DatabaseFactory(configuration);
    }

    public User? Get(string userApiToken)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "SELECT id,first_name,last_name,email,api_type,api_token,access_token FROM users WHERE api_token=:api_token",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "api_token", Value = userApiToken });
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();

        //should only ever return one row

        if (reader.Read() == false) return null;
        var user = User.FromSqlReader(reader);
        // get first user
        return user;
    }

    public void Insert(User user)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "INSERT INTO users (first_name, last_name, email, api_type, api_token, access_token) VALUES (:first_name,:last_name,:email,:api_type,:api_token,:access_token)",
                conn);

        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "first_name", Value = user.firstName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "last_name", Value = user.lastName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "email", Value = user.email });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "api_type", Value = user.apiType });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "api_token", Value = user.apiToken });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "access_token", Value = user.accessToken });

        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }

    public User GetByAccessToken(string token)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand(
                "SELECT id,first_name,last_name,email,api_type,api_token,access_token FROM users WHERE access_token=:access_token",
                conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "access_token", Value = token });
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();

        //should only ever return one row

        if (reader.Read() == false) return null;
        var user = User.FromSqlReader(reader);
        // get first user
        return user;
    }
}