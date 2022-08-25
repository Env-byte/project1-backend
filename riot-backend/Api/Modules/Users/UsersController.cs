using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

[Route("api/users")]
public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
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
    public IActionResult Get(int id)
    {
        var user = _userService.Get(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Insert(User user)
    {
        user = _userService.Insert(user);
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