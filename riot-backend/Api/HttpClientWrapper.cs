using System.Net;
using Newtonsoft.Json;

namespace riot_backend.Api;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly IHttpClientFactory _factory;

    public HttpClientWrapper(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<T> Get<T>(string name) where T : new()
    {
        using var client = _factory.CreateClient();
        
        client.DefaultRequestHeaders.Add("X-Riot-Token", Config.ApiKey);

        var url = Config.Endpoints[0, 1] + "/tft/summoner/v1/summoners/by-name/" + Uri.EscapeDataString(name);
        Console.Write("CurlGet: " + url + "\n\r");
        var response = await client.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Api call to " + url + " failed. Status Code: " + response.StatusCode);
        }

        Console.Write("Result: " + response.Content.ReadAsStringAsync().Result + "\n\r");

        var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) ??
                     throw new InvalidOperationException("Could not decode response");

        return result;
    }
}

public interface IHttpClientWrapper
{
    Task<T> Get<T>(string name) where T : new();
}