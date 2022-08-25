using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

[Route("api/users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
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
        user = _userService.Create(user);
        return Ok(user);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, User user)
    {
        _userService.Update(id, user);

        return Ok(new UpdateResponse
        {
            message = "User Updated",
            id = id
        });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new DeleteResponse
        {
            message = "User Deleted",
            id = id
        });
    }
}