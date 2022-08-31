namespace riot_backend.Api;

public class ErrorWrapper
{
    public ErrorWrapper(string message, int code)
    {
        this.message = message;
        this.code = code;
    }

    public string message { get; set; }
    public int code { get; set; }
}