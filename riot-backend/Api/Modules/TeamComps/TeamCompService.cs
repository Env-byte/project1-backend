using riot_backend.Api.Modules.TeamComps.Models;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompService
{
    private readonly TeamCompRepository _teamCompRepository;

    public TeamCompService(TeamCompRepository teamCompRepository)
    {
        _teamCompRepository = teamCompRepository;
    }

    internal object? Create(TeamRequest teamRequest)
    {
        throw new NotImplementedException();
    }

    internal object? Save(string id, TeamRequest teamRequest)
    {
        throw new NotImplementedException();
    }
}