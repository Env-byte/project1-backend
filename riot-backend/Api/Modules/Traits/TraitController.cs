using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Traits;

[Route("api/trait")]
public class TraitController : Controller
{
    private readonly ILogger<TraitController> _logger;
    private readonly TraitLoader _loader;

    public TraitController(ILogger<TraitController> logger)
    {
        _logger = logger;
        _loader = new TraitLoader();
    }


    [HttpGet("")]
    public List<Types.Trait> GetAll()
    {
        return _loader.GetAll();
    }

    [HttpGet("{name}")]
    public Types.Trait Get(string name)
    {
        return _loader.Get(name);
    }
}