namespace TemperatureAutomation.SmartPlug
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using Microsoft.Extensions.Options;
    using Models;

    public class SmartPlugService(IOptions<ShellyConfig> configuration)
    {
        public async Task<bool> IsOnAsync()
        {
            using var httpClient = new HttpClient();
            var settings = configuration.Value;


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("id", settings.DeviceId),
                new KeyValuePair<string, string>("auth_key", settings.Key)
            });


            var resp = await httpClient.PostAsync($"{settings.Server}/device/status", content);

            var test = await resp.Content.ReadAsStringAsync();

            var shellyResponse = await resp.Content.ReadFromJsonAsync<ShellyResponse>();

            return shellyResponse.data.device_status.dswitch.output;
        }


        public async Task TurnOnAsync()
        {
            using var httpClient = new HttpClient();
            var settings = configuration.Value;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("channel", "0"),
                new KeyValuePair<string, string>("turn", "on"),
                new KeyValuePair<string, string>("id", settings.DeviceId),
                new KeyValuePair<string, string>("auth_key", settings.Key)
            });


            var resp = await httpClient.PostAsync($"{settings.Server}/device/relay/control", content);
        }

        public async Task TurnOffAsync()
        {
            using var httpClient = new HttpClient();
            var settings = configuration.Value;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("channel", "0"),
                new KeyValuePair<string, string>("turn", "off"),
                new KeyValuePair<string, string>("id", settings.DeviceId),
                new KeyValuePair<string, string>("auth_key", settings.Key)
            });


            var resp = await httpClient.PostAsync($"{settings.Server}/device/relay/control", content);
        }
    }
}
