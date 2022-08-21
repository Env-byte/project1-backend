using System.Text;
using Newtonsoft.Json;
using riot_backend.Api.Modules.Traits.Types;

namespace riot_backend.Api.Modules.Traits;

public class TraitLoader
{
    private List<Trait> traits { get; }

    public TraitLoader()
    {
        var path = AppContext.BaseDirectory + "/Cache/traits.json";
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var contents = streamReader.ReadToEnd();
        traits = JsonConvert.DeserializeObject<List<Trait>>(contents) ??
                 throw new InvalidOperationException("Could not decode file");
    }


    public List<Trait> GetAll()
    {
        return traits;
    }

    public Trait Get(string key)
    {
        var traitEnumerable = traits.Where(trait => trait.key == key);
        if (traitEnumerable.Count() == 1)
        {
            return traitEnumerable.GetEnumerator().Current;
        }
        throw new KeyNotFoundException();
    }
}