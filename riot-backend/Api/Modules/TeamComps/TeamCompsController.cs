namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompsController
{
    private readonly TeamCompsService _teamCompsService;

    public TeamCompsController(TeamCompsService teamCompsService)
    {
        _teamCompsService = teamCompsService;
    }
}