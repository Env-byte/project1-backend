using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.Matches.Types;
using riot_backend.Api.Modules.Summoner;

namespace riot_backend.Api.Modules.Matches;

public class MatchService
{
    private readonly MatchRepository _matchRepository;
    private readonly MatchProvider _matchProvider;
    private readonly SummonerService _summonerService;

    public MatchService(MatchRepository matchRepository, MatchProvider matchProvider, SummonerService summonerService)
    {
        _matchRepository = matchRepository;
        _matchProvider = matchProvider;
        _summonerService = summonerService;
    }

    public List<Match> GetMatches(List<string> matchPuuid)
    {
        var (matchesNotFound, matches) = _matchRepository.GetMatches(matchPuuid);
        Console.WriteLine(matchesNotFound.ToString());

        var newMatch = matchesNotFound.Select(puuid => _matchProvider.GetMatch(puuid)).ToList();
        _matchRepository.Insert(newMatch);
        matches = matches.Concat(newMatch).ToList();
        foreach (var match in matches)
        {
            match.metadata.summoners = _summonerService.GetByPuuid(match.metadata.participants);
        }

        return matches;
    }

    public List<string> GetMatchPuuids(string summonerPuuid)
    {
        var matchPuuids = _matchRepository.GetMatchPuuids(summonerPuuid);
        if (matchPuuids.Count == 0)
        {
            matchPuuids = _matchProvider.GetMatchPuuids(summonerPuuid);
        }
        return matchPuuids;
    }

    public List<Match> GetMatchesByName(string summonerName)
    {
        var summoner = _summonerService.GetByName(summonerName);
        var matchIds = GetMatchPuuids(summoner.puuid);
        var matches = GetMatches(matchIds);
        return matches;
    }

    public Match GetMatch(string matchPuuid)
    {
        return GetMatches(new List<string> { matchPuuid })[0];
    }
}