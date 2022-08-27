using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using riot_backend_tests.Helper;
using riot_backend.Api;
using riot_backend.Api.Modules.Users;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend_tests;

public class UserTest
{
    private UsersController _usersController;

    [SetUp]
    public void Setup()
    {
        //allow dapper to match database columns
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Directory.GetCurrentDirectory() + "/../../../appsettings.Test.json", optional: false,
                reloadOnChange: false)
            .Build();

        _usersController = new UsersController(new UserService(new UserRepository(configuration)));
    }

    [Test]
    public void TestCreate()
    {
        var expected = new User
        {
            firstName = "tom",
            lastName = "penn",
            email = "test@tom.co.uk",
            type = ELoginType.none,
            token = "123"
        };
        var result = _usersController.Insert(expected);
        var user = result.ToSuccess<User>();
        expected.id = user.id;
        user.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void TestGet()
    {
        var expected = new User
        {
            id = 1,
            firstName = "tom",
            lastName = "penn",
            email = "test@tom.co.uk",
            type = ELoginType.none,
            token = "123"
        };

        var result = _usersController.Get(1);
        var user = result.ToSuccess<User>();
        user.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void TestUpdate()
    {
        var user = new User
        {
            firstName = "tom",
            lastName = "penn",
            email = "test@tom.co.uk",
            type = ELoginType.none,
            token = "1234"
        };
        var result = _usersController.Update(2, user);
        result.ToSuccess<UpdateResponse>();
    }

    [Test]
    public void TestDelete()
    {
        var create = new User
        {
            firstName = "tom",
            lastName = "penn",
            email = "test@tom.co.uk",
            type = ELoginType.none,
            token = "123"
        };
        var createResult = _usersController.Insert(create);
        var user = createResult.ToSuccess<User>();

        var result = _usersController.Delete(user.id);
        result.ToSuccess<DeleteResponse>();
    }
}