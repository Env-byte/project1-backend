using Npgsql;
using NUnit.Framework;
using riot_backend.Api;
using riot_backend.Api.Modules.Matches.Types;
using riot_backend.Api.Modules.Users;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend_tests;

public class UserTest
{
    private Match _result;

    [SetUp]
    public void Setup()
    {
        var subject = new UsersController(new UserService<FakeDatabaseWrapper>());
    }

    [Test]
    public void Test1()
    {
        Assert.That(_result.info.gameDatetime, Is.EqualTo(123));
    }
}

public class FakeDatabaseWrapper : IDatabaseWrapper
{
    public NpgsqlConnection GetDatabase()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ELoginType>("login_method");
        const string connString =
            "Host=127.0.0.1;Username=tompenn;Password=;Database=riot_project;SearchPath=test,extensions";
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        return conn;
    }
}