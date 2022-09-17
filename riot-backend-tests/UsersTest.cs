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

    }

    [Test]
    public void TestCreate()
    {
      
    }

    [Test]
    public void TestGet()
    {
      
    }

    [Test]
    public void TestUpdate()
    {
      
    }

    [Test]
    public void TestDelete()
    {
     
    }
}