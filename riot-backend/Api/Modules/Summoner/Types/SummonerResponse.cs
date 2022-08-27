using riot_backend.Api.Modules.Matches.Types;

namespace riot_backend.Api.Modules.Summoner.Types;

public class SummonerResponse : Summoner
{
    public static SummonerResponse Convert(Summoner summoner)
    {
        return
            new SummonerResponse()
            {
                matches = new List<Match>(),
                id = summoner.id,
                name = summoner.name,
                puuid = summoner.puuid,
                accountId = summoner.accountId,
                lastUpdate = summoner.lastUpdate,
                revisionDate = summoner.revisionDate,
                profileIconId = summoner.profileIconId,
                summonerLevel = summoner.summonerLevel,
            };
    }

    public List<Match> matches { get; set; }
}