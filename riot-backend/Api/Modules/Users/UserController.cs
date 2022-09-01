using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api.Modules.Users;

public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/api/user/google/login")]
    public IActionResult GoogleLogin(string token )
    {
        return Ok(_userService.GoogleLogin(token));
    }

    [HttpPost("/api/user/access-token/login")]
    public IActionResult AccessTokenLogin(string token)
    {
        return Ok(_userService.AccessTokenLogin(token));
    }
}