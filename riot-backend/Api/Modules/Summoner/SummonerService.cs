using System.ComponentModel;
using Npgsql;
using riot_backend.Api.Modules.Summoner.Types;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerService
{
    private readonly SummonerRepository _summonerRepository;
    private readonly SummonerProvider _summonerProvider;

    public SummonerService(IConfiguration configuration, SummonerProvider summonerProvider)
    {
        _summonerRepository = new SummonerRepository(configuration);
        _summonerProvider = summonerProvider;
    }

    public SummonerResponse GetByName(string name)
    {
        var summoner = _summonerRepository.GetByName(name);
        if (summoner == null)
        {
            summoner = _summonerProvider.GetByName(name);
            _summonerRepository.Insert(summoner);
        }

        return SummonerResponse.Convert(summoner);
    }

    /**
     * Refresh data using provider. must wait 5 minutes between refreshes
     */
    public SummonerResponse Refresh(string puuid)
    {
        var summoner = _summonerRepository.GetByPuuid(puuid);
        if (summoner == null)
        {
            throw new KeyNotFoundException("Could not refresh summoner with puuid: " + puuid + " as it does not exist");
        }

        //12:30 last update 12:35
        // 12:32 now 
        var nextUpdate = summoner.lastUpdate.AddMinutes(5);
        if (nextUpdate > DateTime.Now)
        {
            var ts = nextUpdate - DateTime.Now;

            throw new WarningException(
                "Please wait " + ts.TotalSeconds + " second(s) before trying to refresh this summoner again.");
        }

        summoner = _summonerProvider.GetByPuuid(puuid);
        summoner.lastUpdate = DateTime.Now;
        _summonerRepository.Update(puuid, summoner);

        return SummonerResponse.Convert(summoner);
    }

    public SummonerResponse GetByPuuid(string puuid)
    {
        var summoner = _summonerRepository.GetByPuuid(puuid);

        if (summoner == null)
        {
            var providerResponse = _summonerProvider.GetByPuuid(puuid);
            _summonerRepository.Insert(providerResponse);
            summoner = providerResponse;
        }

        return SummonerResponse.Convert(summoner);
    }

    public List<Types.Summoner> GetByPuuid(List<string> puuids)
    {
        var (notFound, summoners) = _summonerRepository.GetByPuuid(puuids);
        Console.WriteLine("notFound:" + notFound.ToList());
        var newSummoners = notFound.Select(puuid => _summonerProvider.GetByPuuid(puuid)).ToList();
        _summonerRepository.Insert(newSummoners);
        return summoners.Concat(newSummoners).ToList();
    }
}