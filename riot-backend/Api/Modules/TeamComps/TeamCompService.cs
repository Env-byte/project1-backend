namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompService
{
    private readonly TeamCompRepository _teamCompRepository;

    public TeamCompService(TeamCompRepository teamCompRepository)
    {
        _teamCompRepository = teamCompRepository;
    }
}