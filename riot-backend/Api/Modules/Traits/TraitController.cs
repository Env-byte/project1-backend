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

/*
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(_traitService.GetAll());
    }

    [HttpGet("{name}")]
    public IActionResult Get(string name)
    {
        return Ok(_traitService.Get(name));
    }*/
}