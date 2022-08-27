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
    public IActionResult GetAll()
    {
        return Ok(_itemService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        return Ok(_itemService.Get(id));
    }
}