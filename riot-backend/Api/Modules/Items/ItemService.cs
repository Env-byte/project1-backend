using riot_backend.Api.Modules.Items.Types;

namespace riot_backend.Api.Modules.Items;

public class ItemService
{
    private readonly ItemProvider _itemProvider;

    public ItemService(ItemProvider itemProvider)
    {
        _itemProvider = itemProvider;
    }

    public List<Item> GetAll()
    {
        return _itemProvider.GetAll();
    }

    public Item Get(string key)
    {
        return _itemProvider.Get(key);
    }
}