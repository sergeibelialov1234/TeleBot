using System.Text.Json;

namespace Tele.Bot.Client;

public class RestApiClient : IRestApiClient
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public RestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<T> SendGetRequest<T>(string url) where T : class, new()
    {
        var result = await _httpClient.GetAsync(url);

        result.EnsureSuccessStatusCode();
            
        var stringJson = await result.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<T>(stringJson, _options);

        return response;
    }

    public async Task<T> SendPostRequest<T>(string url, object content) where T : class, new()
    {
        var contentJson = JsonSerializer.Serialize(content);
        
        var result = await _httpClient.PostAsync(url, new StringContent(contentJson));

        result.EnsureSuccessStatusCode();
            
        var stringJson = await result.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<T>(stringJson, _options);

        return response;
    }
}