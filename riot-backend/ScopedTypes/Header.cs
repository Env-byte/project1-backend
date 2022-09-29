using riot_backend.Api;

namespace riot_backend.ScopedTypes;

public class Header
{
    public string PlatformRoute { get; set; }
    public string RegionalRoute { get; set; }
    public string Region { get; set; }
    public string Token { get; set; }
}