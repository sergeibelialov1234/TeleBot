namespace Tele.Bot.Client;

public interface IRestApiClient
{
    Task<T> SendGetRequest<T>(string url) where T : class, new();
    Task<T> SendPostRequest<T>(string url, object content) where T : class, new();
}