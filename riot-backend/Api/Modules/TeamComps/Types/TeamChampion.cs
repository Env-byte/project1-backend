using System;
namespace riot_backend.Api.Modules.TeamComps.Types;
public class TeamChampion
{
    public string CharacterId { get; set; }
    public int TeamId { get; set; }
    public int Hex { get; set; }
    public List<int> ItemIds { get; set; }
}


