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


    private async Task<string> _ExecGet(string url)
    {
        using var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Riot-Token", Config.ApiKey);
        var response = await client.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Api call to " + url + " failed. Status Code: " + response.StatusCode);
        }

        return response.Content.ReadAsStringAsync().Result;
    }

    public T Get<T>(string url)
    {
        var response = _ExecGet(url).Result;
        var result = JsonConvert.DeserializeObject<T>(response) ??
                     throw new InvalidOperationException("Could not decode response");
        return result;
    }
}

public interface IHttpClientWrapper
{
    
    T Get<T>(string name);
}