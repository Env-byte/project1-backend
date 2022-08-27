using riot_backend.Api.Modules.Champions.Types;

namespace riot_backend.Api.Modules.Champions;

public class ChampionService
{
    private readonly ChampionProvider _championProvider;

    public ChampionService(ChampionProvider championProvider)
    {
        _championProvider = championProvider;
    }

    public List<Champion> GetAll()
    {
        return _championProvider.GetAll();
    }

    public Champion Get(string championId)
    {
        return _championProvider.Get(championId);
    }
}