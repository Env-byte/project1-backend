using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using riot_backend.Api.Modules.Matches.Types;
using riot_backend.Api.Modules.Users;

namespace riot_backend_tests;

public class UserTest
{
    private Match _result;

    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: false)
            .Build();
        var subject = new UsersController(new UserService(configuration));
    }

    [Test]
    public void Test1()
    {
        Assert.That(_result.info.gameDatetime, Is.EqualTo(123));
    }
}