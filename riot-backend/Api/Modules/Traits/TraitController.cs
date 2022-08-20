using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Traits;

public class TraitController : Controller
{
    private readonly ILogger<TraitController> _logger;
    private readonly TraitLoader _loader;
    
    [HttpGet("api/Summoner/name")]
    public Types.Trait GetAll(string name)
    {
        return null;
    }
}