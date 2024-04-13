using System.Text.Json;

namespace Tele.Bot.Client;

public class RestApiClient : IRestApiClient
{
    private readonly HttpClient _httpClient;


    public RestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<T> SendGetRequest<T>(string url) where T : class, new()
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };
        
        var result = await _httpClient.SendAsync(request);

        result.EnsureSuccessStatusCode();
            
        var stringJson = await result.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<T>(stringJson);

        return response;

    }

    public async Task<T> SendPostRequest<T>(string url, object content) where T : class, new()
    {
        var contentJson = JsonSerializer.Serialize(content);
        
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Content = new StringContent(contentJson)
        };
        
        var result = await _httpClient.SendAsync(request);

        result.EnsureSuccessStatusCode();
            
        var stringJson = await result.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<T>(stringJson);

        return response;
    }
}