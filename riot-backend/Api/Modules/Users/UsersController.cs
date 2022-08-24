using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

[Route("api/users")]
public class UsersController : Controller
{
    private IUserService _userService;
    private IMapper _mapper;

    public UsersController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        _userService.Create(user);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, User user)
    {
        _userService.Update(id, user);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}