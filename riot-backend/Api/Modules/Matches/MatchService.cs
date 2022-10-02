using riot_backend.Api.Modules.Matches.Types;
namespace riot_backend.Api.Modules.Matches;
public class MatchService
{
    private readonly MatchRepository _matchRepository;
    private readonly MatchProvider _matchProvider;

    public MatchService(MatchRepository matchRepository, MatchProvider matchProvider)
    {
        _matchRepository = matchRepository;
        _matchProvider = matchProvider;
    }

    public List<Match> GetMatches(List<string> matchPuuid)
    {
        var (matchesNotFound, matches) = _matchRepository.GetMatches(matchPuuid);
        var newMatch = matchesNotFound.Select(puuid => _matchProvider.GetMatch(puuid)).ToList();
        _matchRepository.Insert(newMatch);
        matches = matches.Concat(newMatch).ToList();
        return matches;
    }

    public List<string> GetMatchPuuids(string summonerPuuid)
    {
        var matchPuuids = _matchRepository.GetMatchPuuids(summonerPuuid);
        if (matchPuuids.Count != 0) return matchPuuids;
        
        matchPuuids = _matchProvider.GetMatchPuuids(summonerPuuid);
        _matchRepository.InsertMatchPuuids(summonerPuuid, matchPuuids);

        return matchPuuids;
    }
    
    public Match GetMatch(string matchPuuid)
    {
        return GetMatches(new List<string> { matchPuuid })[0];
    }
}