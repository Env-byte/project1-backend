using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Traits;

[Route("api/trait")]
public class TraitController : Controller
{
    private readonly ILogger<TraitController> _logger;
    private readonly TraitService _traitService;

    public TraitController(ILogger<TraitController> logger, TraitService traitService)
    {
        _logger = logger;
        _traitService = traitService;
    }


    [HttpGet("")]
    public List<Types.Trait> GetAll()
    {
        return _traitService.GetAll();
    }

    [HttpGet("{name}")]
    public Types.Trait Get(string name)
    {
        return _traitService.Get(name);
    }
}