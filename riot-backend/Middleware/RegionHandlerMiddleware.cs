using riot_backend.Api;
using riot_backend.Services;

namespace riot_backend.Middleware;
public class RegionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public RegionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, RegionService regionService)
    {
        var region = context.Request.Headers["region"].ToString();
        if (string.IsNullOrEmpty(region))
        {
            //default to euw1
            region = "EUW1";
        }
        regionService.platformRoute = Config.PlatformRoutes[region] ?? string.Empty;
        regionService.regionalRoute = Config.RegionalRoutes[region] ?? string.Empty;

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