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
        Types.SummonerResponse summoner;
        try
        {
            summoner = (Types.SummonerResponse)_summonerRepository.GetByName(name);
        }
        catch (KeyNotFoundException e)
        {
            var providerResponse = _summonerProvider.GetByName(name);
            _summonerRepository.Insert(providerResponse);
            summoner = (Types.SummonerResponse)providerResponse;
        }

        return summoner;
    }

    public Types.SummonerResponse GetByPuuid(string puuid)
    {
        Types.SummonerResponse summoner;
        try
        {
            summoner = (Types.SummonerResponse)_summonerRepository.GetByPuuid(puuid);
        }
        catch (KeyNotFoundException e)
        {
            var providerResponse = _summonerProvider.GetByPuuid(puuid);
            _summonerRepository.Insert(providerResponse);
            summoner = (Types.SummonerResponse)providerResponse;
        }

        return summoner;
    }

    /**
     * Refresh data using provider. must wait 5 mins between refreshes
     */
    public Types.SummonerResponse Refresh(string puuid)
    {
        var summoner = (Types.SummonerResponse)_summonerRepository.GetByPuuid(puuid);
        //12:30 last update 12:35
        // 12:32 now 
        var nextUpdate = summoner.lastUpdate.AddMinutes(5);
        if (nextUpdate > DateTime.Now)
        {
            var ts = nextUpdate - DateTime.Now;

            throw new WarningException(
                "Please wait " + ts.TotalSeconds + " second(s) before trying to refresh this summoner again.");
        }

        _summonerProvider.GetByPuuid(puuid);
        summoner.lastUpdate = DateTime.Now;
        _summonerRepository.Update(puuid, summoner);

        return summoner;
    }
}