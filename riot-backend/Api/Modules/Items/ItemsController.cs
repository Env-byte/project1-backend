using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Items;

[Route("api/item/")]
public class ItemsController : Controller
{
    private readonly ILogger<ItemsController> _logger;
    private readonly ItemLoader _loader;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
        _loader = new ItemLoader();
    }


    [HttpGet("")]
    public List<Types.Item> GetAll()
    {
        return _loader.GetAll();
    }

    [HttpGet("{id}")]
    public Types.Item Get(string key)
    {
        return _loader.Get(key);
    }
}