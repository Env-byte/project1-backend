using NUnit.Framework;
using riot_backend.Api;
using riot_backend.Api.Modules.Matches;
using riot_backend.Api.Modules.Matches.Types;

namespace riot_backend_tests;

public class Tests
{
    private Match _result;

    [SetUp]
    public void Setup()
    {
        var subject = new MatchesController(null, new FakeHttpClientWrapper());

        _result = subject.Get("thing");
    }

    [Test]
    public void Test1()
    {
        Assert.That(_result.info.gameDatetime, Is.EqualTo(123));
    }
}

public class FakeHttpClientWrapper : IHttpClientWrapper
{
    public Task<T> Get<T>(string name) where T : new()
    {
        return Task.FromResult<T>(new T());
    }
}