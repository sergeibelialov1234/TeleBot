using Tele.Bot.Client;

namespace Tele.Bot.Services;

public class RentService : IRentService
{
   private readonly IRestApiClient _restApiClient;

   public RentService(RestApiClient restApiClient)
   {
      _restApiClient = restApiClient;
   }

   public async Task<Root> GetRentals()
   {
     var result = await _restApiClient.SendGetRequest<Root>("api/v1/locations/buildings/?featured=1&in_bbox=-97.51093%2C49.70414%2C-96.79339%2C50.00337&limit=100");
     
     return result;
   }
}

public interface IRentService
{
    Task<Root> GetRentals();
}