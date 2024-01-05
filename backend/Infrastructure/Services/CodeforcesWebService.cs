using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Contracts.Infrastructure;

namespace Infrastructure.Services
{
    public class CodeforcesWebService : ICodeforcesWebService
    {
        private readonly string BASE_URL = "https://codeforces.com/api/";
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public CodeforcesWebService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

            // Set up the HttpClient with the base URL and any other configuration needed
            _httpClient.BaseAddress = new Uri(BASE_URL);
            // You can also set additional configurations like timeouts, headers, etc., if needed
        }

        public async Task GetStanding(int contestId)
        {
            // Add the API key to the request headers
            _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);

            // Construct the specific URL for your API call, e.g., "https://codeforces.com/api/someendpoint"
            string requestUrl = $"contest.standings?&&contestId={contestId}"; // Replace with the actual endpoint

            try
            {
                // Make an HTTP GET request using the configured HttpClient
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                // Handle the response here...
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseBody}");
                }
                else
                {
                    // Output the error status code if the request was not successful
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
