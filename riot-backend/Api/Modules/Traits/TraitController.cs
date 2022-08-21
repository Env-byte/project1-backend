using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Traits;

public class TraitController : Controller
{
    private readonly ILogger<TraitController> _logger;
    private readonly TraitLoader _loader;

    public TraitController(ILogger<TraitController> logger)
    {
        _logger = logger;
        _loader = new TraitLoader();
    }


    [HttpGet("api/trait/")]
    public List<Types.Trait> GetAll()
    {
        return _loader.GetAll();
    }

    [HttpGet("api/trait/name")]
    public Types.Trait Get(string key)
    {
        return _loader.Get(key);
    }
}