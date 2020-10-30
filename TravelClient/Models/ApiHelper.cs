using System.Threading.Tasks;
using RestSharp;

namespace TravelClient.Models
{
  class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("http://localhost:6000/api");
      RestRequest request = new RestRequest($"reviews", Method.GET);
      request.AddHeader("Authorization", $"Bearer {EnvironmentVariables.ApiKey}");
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("http://localhost:6000/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.GET);
      request.AddHeader("Authorization", $"Bearer {EnvironmentVariables.ApiKey}");
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task Post(string newReview)
    {
      RestClient client = new RestClient("http://localhost:6000/api");
      RestRequest request = new RestRequest($"reviews", Method.POST);
      request.AddHeader("Authorization", $"Bearer {EnvironmentVariables.ApiKey}");
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newReview);
      var response = await client.ExecuteTaskAsync(request);
    }

    public static async Task Put(int id, string newReview)
    {
      RestClient client = new RestClient("http://localhost:6000/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.PUT);
      request.AddHeader("Authorization", $"Bearer {EnvironmentVariables.ApiKey}");
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newReview);
      var response = await client.ExecuteTaskAsync(request);
    }

    public static async Task Delete(int id)
    {
      RestClient client = new RestClient("http://localhost:6000/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.DELETE);
      request.AddHeader("Authorization", $"Bearer {EnvironmentVariables.ApiKey}");
      request.AddHeader("Content-Type", "application/json");
      var response = await client.ExecuteTaskAsync(request);
    }
  }

}