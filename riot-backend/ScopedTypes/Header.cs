using riot_backend.Api;
using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.ScopedTypes;

public class Header
{
    public string PlatformRoute { get; set; }
    public string RegionalRoute { get; set; }
    public string Region { get; set; }
    public User? User { get; internal set; }
}