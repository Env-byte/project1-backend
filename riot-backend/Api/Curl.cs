using System.Net;
using System.Text;
using Microsoft.AspNetCore.Components.RenderTree;

namespace riot_backend.Api;

// Include the `System.Net` and `System.IO` into your C# project
// and install NuGet `Newtonsoft.Json` package before executing the C# code.
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

abstract public class Curl
{
    protected static async Task<T> CurlGet<T>(string name)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Riot-Token", Config.ApiKey);

        var url = Config.Endpoints[0, 1] + "/tft/summoner/v1/summoners/by-name/" + Uri.EscapeDataString(name);
        Console.Write("CurlGet: " + url + "\n\r");
        var response = await client.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Api call to " + url + " failed. Status Code: " + response.StatusCode);
        }

        Console.Write("Result: " + response.Content.ReadAsStringAsync().Result + "\n\r");

        T result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) ??
                         throw new InvalidOperationException("Could not decode response");

        return result;
    }
}