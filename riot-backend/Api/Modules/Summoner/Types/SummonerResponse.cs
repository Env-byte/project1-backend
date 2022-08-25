using riot_backend.Api.Modules.Matches.Types;
namespace riot_backend.Api.Modules.Summoner.Types;

public class SummonerResponse : Summoner
{
    public List<Match> matches { get; set; }
}