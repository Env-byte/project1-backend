using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Npgsql;

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

    [Column("first_name")] public string firstName { get; set; }

    [Column("last_name")] public string lastName { get; set; }

    public string email { get; set; }
    [Column("api_token")] public string apiToken { get; set; }
    [Column("api_type")] public ELoginType apiType { get; set; }
    [Column("access_token")] public string? accessToken { get; set; }

    public static User FromSqlReader(NpgsqlDataReader reader)
    {
       
        return new User
        {
            id = reader.GetInt32(0),
            firstName = reader.GetString(1),
            lastName = reader.GetString(2),
            email = reader.GetString(3),
            apiType = reader.GetFieldValue<ELoginType>(4),
            apiToken = reader.GetString(5),
            accessToken =  reader.GetString(6),
        };
    }
}