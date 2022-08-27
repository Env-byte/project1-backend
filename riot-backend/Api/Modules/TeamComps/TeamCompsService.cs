namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompsService
{
    private readonly TeamCompsRepository _teamCompsRepository;

    public TeamCompsService(TeamCompsRepository teamCompsRepository)
    {
        _teamCompsRepository = teamCompsRepository;
    }
}