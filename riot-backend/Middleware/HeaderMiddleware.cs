using riot_backend.Api;
using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Users;
using riot_backend.Api.Modules.Users.Types;
using riot_backend.ScopedTypes;

namespace riot_backend.Middleware;

public class HeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly UserService _userService;

    public HeaderMiddleware(RequestDelegate next, UserRepository userRepository, GoogleAuthService googleAuthService)
    {
        _next = next;
        _userService = new UserService(userRepository, googleAuthService);
    }

    public async Task Invoke(HttpContext context, Header header)
    {
        var region = context.Request.Headers["region"].ToString();

        if (string.IsNullOrEmpty(region))
        {
            //default to euw1
            region = "EUW1";
        }

        Console.WriteLine("RegionHandlerMiddleware region: " + region);
        header.Region = region;
        header.PlatformRoute = Config.PlatformRoutes[region] ?? string.Empty;
        header.RegionalRoute = Config.RegionalRoutes[region] ?? string.Empty;

        header.User = GetUser(context.Request.Headers["token"].ToString());

        await _next(context);
    }

    protected User? GetUser(string token)
    {

        //if testing use first user in db
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Development" && token == string.Empty)
        {
            var user = _userService.GetFirstUser();
            if (user != null)
            {
                token = user.accessToken ?? "";
            }
        }
        if (token != string.Empty)
        {
            return _userService.AccessTokenLogin(token);
        }
        return null;
    }
}

public static class HeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseHeaderHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HeaderMiddleware>();
    }
}