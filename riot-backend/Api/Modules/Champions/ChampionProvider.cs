using System.Text;
using Newtonsoft.Json;
using riot_backend.Api.Modules.Champions.Types;

namespace riot_backend.Api.Modules.Champions;

public class ChampionProvider
{
    private List<Champion> champions { get; }

    public ChampionProvider()
    {
        var path = AppContext.BaseDirectory + "/Cache/traits.json";
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var contents = streamReader.ReadToEnd();
        champions = JsonConvert.DeserializeObject<List<Champion>>(contents) ??
                    throw new InvalidOperationException("Could not decode file");
    }


    public List<Champion> GetAll()
    {
        return champions;
    }

    public Champion Get(string championId)
    {
        var championEnumerable = champions.Where(champion => champion.championId == championId);
        if (championEnumerable.Count() == 1)
        {
            return championEnumerable.GetEnumerator().Current;
        }
        throw new KeyNotFoundException();
    }
}