using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users.Models;

public class UserResponse
{
    public string firstName { get; set; }

    public string lastName { get; set; }

    public string email { get; set; }

    public string accessToken { get; set; }

    public static UserResponse FromUser(User user)
    {
        return new UserResponse()
        {
            firstName = user.firstName,
            lastName = user.lastName,
            email = user.email,
            accessToken = user.accessToken
        };
    }
}