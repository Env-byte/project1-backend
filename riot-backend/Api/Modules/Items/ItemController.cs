using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Items;

[Route("api/item/")]
public class ItemsController : Controller
{
    private readonly ILogger<ItemsController> _logger;
    private readonly ItemService _itemService;

    public ItemsController(ILogger<ItemsController> logger, ItemService itemService)
    {
        _logger = logger;
        _itemService = itemService;
    }


    [HttpGet("")]
    public List<Types.Item> GetAll()
    {
        return _itemService.GetAll();
    }

    [HttpGet("{id}")]
    public Types.Item Get(string key)
    {
        return _itemService.Get(key);
    }
}