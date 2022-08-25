using Dapper;
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

    public IEnumerable<User> GetAll()
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand($"SELECT id,first_name,last_name,email,type,token FROM users", conn);
        using var reader = cmd.ExecuteReader();
        var parser = reader.GetRowParser<User>(typeof(User));
        var users = new List<User>();
        while (reader.Read())
        {
            var user = parser(reader);
            if (user != null)
            {
                users.Add(user);
            }
        }

        return users.ToArray();
    }

    public User Insert(User user)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "INSERT INTO users (first_name,last_name,email,type,token) values (@first_name,@last_name,@email,@type,@token) returning id;";
        cmd.Connection = conn;

        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "first_name", Value = user.firstName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "last_name", Value = user.lastName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "email", Value = user.email });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "type", Value = user.type });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "token", Value = user.token });
        cmd.Prepare();
        var val = cmd.ExecuteScalar();
        var newId = (int)(val ?? throw new InvalidOperationException());
        return Get(newId);
    }

    public void Update(int id, User user)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText =
            "UPDATE users SET first_name= :first_name, last_name= :last_name, email = :email WHERE id = @id";
        cmd.Connection = conn;

        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "first_name", Value = user.firstName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "last_name", Value = user.lastName });
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "email", Value = user.email });

        cmd.Prepare();

        var val = cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd = new NpgsqlCommand();
        cmd.CommandText = "DELETE FROM users  WHERE id = @id";
        cmd.Connection = conn;
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Prepare();
        var val = cmd.ExecuteNonQuery();
    }


    public User Get(int id)
    {
        using var conn = _databaseFactory.GetDatabase();
        using var cmd =
            new NpgsqlCommand($"SELECT id,first_name,last_name,email,type,token FROM users WHERE id= @id", conn);
        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "id", Value = id });
        cmd.Prepare();
        using var reader = cmd.ExecuteReader();
        var parser = reader.GetRowParser<User>(typeof(User));
        //should only ever return one row
        reader.Read();
        var user = parser(reader);
        if (user != null)
        {
            // get first user
            return user;
        }

        throw new KeyNotFoundException("User with id:  " + id + " not found.");
    }
}