namespace riot_backend.Api;

public class UpdateResponse : BaseSuccessResponse
{
    public int id { get; set; }
    public string message { get; set; }
}