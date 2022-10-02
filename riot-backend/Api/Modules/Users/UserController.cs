using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Users.Models;

namespace riot_backend.Api.Modules.Users;

public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserRepository userRepository, GoogleAuthService googleAuthService)
    {
        
        _userService = new UserService(userRepository, googleAuthService);
    }

    [HttpPost("/api/user/google/login")]
    public IActionResult GoogleLogin(string token)
    {
        return Ok(UserResponse.FromUser(_userService.GoogleLogin(token)));
    }

    [HttpPost("/api/user/access-token/login")]
    public IActionResult AccessTokenLogin(string token)
    {
        return Ok(UserResponse.FromUser(_userService.AccessTokenLogin(token)));
    }
}