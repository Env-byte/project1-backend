using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Users.Models;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly GoogleAuthService _googleAuthService;

    public UserService(UserRepository userRepository, GoogleAuthService googleAuthService)
    {
        _userRepository = userRepository;
        _googleAuthService = googleAuthService;
    }

    public UserResponse GoogleLogin(string token)
    {
        var user = _googleAuthService.ValidateToken(token);
        var existingUser = _userRepository.Get(user.apiToken);
        if (existingUser == null)
        {
            user.accessToken = GenerateAccessToken();
            _userRepository.Insert(user);
        }
        else
        {
            user = existingUser;
        }

        return UserResponse.FromUser(user);
    }

    private string GenerateAccessToken()
    {
        var guuid = Guid.NewGuid();
        return guuid.ToString();
    }

    public UserResponse AccessTokenLogin(string token)
    {
        var authUser = _userRepository.GetByAccessToken(token);
        if (authUser == null) throw new KeyNotFoundException("Could not login using access token. User not found");
        return UserResponse.FromUser(authUser);
    }
}