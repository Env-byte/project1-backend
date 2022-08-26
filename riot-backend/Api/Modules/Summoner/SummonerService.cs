using System.ComponentModel;
using Npgsql;

namespace riot_backend.Api.Modules.Summoner;

public class SummonerService
{
    private readonly SummonerRepository _summonerRepository;
    private readonly SummonerProvider _summonerProvider;

    public SummonerService(IConfiguration configuration, IHttpClientWrapper http)
    {
        _summonerRepository = new SummonerRepository(configuration);
        _summonerProvider = new SummonerProvider(http);
    }

    public Types.SummonerResponse GetByName(string name)
    {
        var summoner = _summonerRepository.GetByName(name);
        if (summoner == null)
        {
            var providerResponse = _summonerProvider.GetByName(name);
            _summonerRepository.Insert(providerResponse);
            summoner = (Types.SummonerResponse)providerResponse;
        }

        var summonerResponse = (Types.SummonerResponse)summoner;
        return summonerResponse;
    }

    /**
     * Refresh data using provider. must wait 5 minutes between refreshes
     */
    public Types.SummonerResponse Refresh(string puuid)
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

        var summonerResponse = (Types.SummonerResponse)summoner;
        return summonerResponse;
    }
    
    public Types.SummonerResponse GetByPuuid(string puuid)
    {
        var summoner = _summonerRepository.GetByPuuid(puuid);

        if (summoner == null)
        {
            var providerResponse = _summonerProvider.GetByPuuid(puuid);
            _summonerRepository.Insert(providerResponse);
            summoner = providerResponse;
        }

        var summonerResponse = (Types.SummonerResponse)summoner;
        return summonerResponse;
    }

    public List<Types.Summoner> GetByPuuid(List<string> puuids)
    {
        var (notFound, summoners) = _summonerRepository.GetByPuuid(puuids);
        var newSummoners = notFound.Select(puuid => _summonerProvider.GetByPuuid(puuid)).ToList();
        _summonerRepository.Insert(newSummoners);
        return summoners.Concat(newSummoners).ToList();
    }
}