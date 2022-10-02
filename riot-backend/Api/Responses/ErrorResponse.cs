using riot_backend.helpers;

namespace riot_backend.Api.Responses;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Message { get; set; }
    public string Stack { get; set; }

    public ErrorResponse(Exception ex)
    {
        Type = ex.GetType().Name.Replace("Exception", "").SplitOnCapitals();
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Message = ex.Message;
        Stack = env == "Development" ? ex.ToString() : "";
    }
}