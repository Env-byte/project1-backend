using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace riot_backend.Api.Modules.Users.Types;

public enum ELoginType
{
    google,

    //used only for testing
    none
}

[Table(name: "users", Schema = "general")]
public class User : BaseSuccessResponse
{
    public int id { get; set; }

    [Column("first_name")]
    public string firstName { get; set; }

    [Column("last_name")]
    public string lastName { get; set; }

    public string email { get; set; }
    public string token { get; set; }
    public ELoginType type { get; set; }
}