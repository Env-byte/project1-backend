using Google.Apis.Auth;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.GoogleAuth;

public class GoogleAuthService
{
    public User ValidateToken(string token)
    {
        var payload = GoogleJsonWebSignature.ValidateAsync(token);
        var result = payload.Result;
        
        return new User
        {
            firstName = result.GivenName,
            lastName = result.FamilyName,
            email = result.Email,
            apiToken = result.Subject,
            apiType = ELoginType.google,
            accessToken = null
        };
    }
}