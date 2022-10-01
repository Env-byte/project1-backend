namespace riot_backend.Api;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Message { get; set; }

    public ErrorResponse(Exception ex)
    {
        Type = ex.GetType().Name;
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Development")
        {
            Message = ex.ToString();
        }
        else
        {
            Message = ex.Message;
        }
    }
}