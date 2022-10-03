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

    public User GoogleLogin(string token)
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

        return user;
    }

    private string GenerateAccessToken()
    {
        var guuid = Guid.NewGuid();
        return guuid.ToString();
    }

    public User AccessTokenLogin(string token)
    {
        var authUser = _userRepository.GetByAccessToken(token);
        if (authUser == null) throw new NotFoundException("Could not login using access token. User not found");
        return authUser;
    }

    //this is development only
    public User? GetFirstUser()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env != "Development")
        {
            throw new NotImplementedException();
        }
       return _userRepository.GetFirstUser();
    }
}