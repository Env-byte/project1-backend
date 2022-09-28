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

    public async Task Invoke(HttpContext context, Header region)
    {
        var regionStr = context.Request.Headers["region"].ToString();
        
        if (string.IsNullOrEmpty(regionStr))
        {
            //default to euw1
            regionStr = "EUW1";
        }

        Console.WriteLine("RegionHandlerMiddleware region: " +regionStr);
        region.region = regionStr;
        region.platformRoute = Config.PlatformRoutes[regionStr] ?? string.Empty;
        region.regionalRoute = Config.RegionalRoutes[regionStr] ?? string.Empty;

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