using System.Text;
using Newtonsoft.Json;
using riot_backend.Api.Modules.Items.Types;

namespace riot_backend.Api.Modules.Items;

public class ItemLoader
{
    private List<Item> items { get; }

    public ItemLoader()
    {
        var path = AppContext.BaseDirectory + "/Cache/items.json";
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var contents = streamReader.ReadToEnd();
        Console.Write(contents);
        items = JsonConvert.DeserializeObject<List<Item>>(contents) ??
                throw new InvalidOperationException("Could not decode file");
    }


    public List<Item> GetAll()
    {
        return items;
    }

    public Item Get(string key)
    {
        var item = items.Where(item => item.id == key);
        if (item.Count() == 1)
        {
            return item.GetEnumerator().Current;
        }

        throw new KeyNotFoundException();
    }
}