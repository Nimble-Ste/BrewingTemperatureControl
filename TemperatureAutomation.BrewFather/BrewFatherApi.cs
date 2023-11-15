namespace TemperatureAutomation.BrewFather
{
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;

    public class BrewFatherApi (IOptions<BrewFatherConfig> configuration, ILogger<BrewFatherApi> logger)
    {
        private string AuthKey = Base64Encode($"{configuration.Value.User}:{configuration.Value.Key}");
        private string BaseURL = "https://api.brewfather.app/v1";

        public async Task<string> GetBatchIdAsync()
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthKey);

            var result = await httpClient.GetAsync(BaseURL + "/batches");


            var batches = await result.Content.ReadFromJsonAsync<List<BatchResponse>>();

            var latest = batches?.First(x => x.status == "Fermenting");

            return latest!._id;
        }

        public async Task<float> GetTemperatureAsync(string batchId)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthKey);

            var result = await httpClient.GetAsync($"{BaseURL}/batches/{batchId}/readings/last");

            var batch = await result.Content.ReadFromJsonAsync<BatchReadingResponse>();

            return batch.temp;

        }


        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
