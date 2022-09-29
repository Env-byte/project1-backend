using riot_backend.Api;
using riot_backend.ScopedTypes;

namespace riot_backend.Middleware;

public class HeaderMiddleware
{
    private readonly RequestDelegate _next;

    public HeaderMiddleware(RequestDelegate next)
    {
        _next = next;
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

        var userToken = context.Request.Headers["token"].ToString();
        header.Token = userToken;

        await _next(context);
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