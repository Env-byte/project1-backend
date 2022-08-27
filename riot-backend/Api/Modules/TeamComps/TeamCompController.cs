namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompsController
{
    private readonly TeamCompService _teamCompsService;

    public TeamCompsController(TeamCompService teamCompsService)
    {
        _teamCompsService = teamCompsService;
    }
}