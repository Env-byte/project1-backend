using riot_backend.Api.Modules.TeamComps.Models;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompService
{
    private readonly TeamCompRepository _repository;

    public TeamCompService(TeamCompRepository teamCompRepository)
    {
        _repository = teamCompRepository;
    }

    public string Create(TeamRequest teamRequest)
    {
        return _repository.Insert(teamRequest);
    }

    public TeamRequest Save(string guuid, TeamRequest teamRequest)
    {
        throw new NotImplementedException();
    }

    public TeamRequest Get(string guuid)
    {
        var team = _repository.Get(guuid);
        return TeamRequest.FromTeam(team);
    }

    internal object? GetPublic(int start)
    {
        throw new NotImplementedException();
    }
}