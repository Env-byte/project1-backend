using riot_backend.Api.Modules.TeamComps.Models;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompService
{
    private readonly TeamCompRepository _repository;

    public TeamCompService(TeamCompRepository teamCompRepository)
    {
        _repository = teamCompRepository;
    }

    internal string Create(TeamRequest teamRequest)
    {
        return _repository.Insert(teamRequest);
    }

    internal object? Save(string id, TeamRequest teamRequest)
    {
        throw new NotImplementedException();
    }
}