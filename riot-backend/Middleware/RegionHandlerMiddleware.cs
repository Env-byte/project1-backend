using riot_backend.Api;
using riot_backend.ScopedTypes;

namespace riot_backend.Middleware;

public class RegionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public RegionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, Region region)
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

public static class RegionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseRegionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RegionHandlerMiddleware>();
    }
}