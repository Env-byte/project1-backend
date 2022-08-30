using riot_backend.Api.Modules.Leagues.Types;

namespace riot_backend.Api.Modules.Leagues;

public class LeagueService
{
    private readonly LeagueProvider _leagueProvider;
    private readonly LeagueRepository _leagueRepository;

    public LeagueService(LeagueProvider leagueProvider, LeagueRepository leagueRepository)
    {
        _leagueProvider = leagueProvider;
        _leagueRepository = leagueRepository;
    }

    public List<League> GetSummonerId(string summonerId)
    {
        var leagues = _leagueRepository.Get(summonerId);

        if (leagues.Count != 0) return leagues;

        leagues = _leagueProvider.GetSummonerLeague(summonerId);
        _leagueRepository.Insert(leagues);

        return leagues;
    }
}