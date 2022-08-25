namespace riot_backend.Api;

public class DeleteResponse : BaseSuccessResponse
{
    public int id { get; set; }
    public string message { get; set; }
}