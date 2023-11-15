namespace TemperatureAutomation.TemperatureMonitor
{
    using BrewFather;
    using Microsoft.Extensions.Logging;

    public class BrewFatherService(BrewFatherApi brewFatherApi, ILogger<BrewFatherService> logger)
    {
        public async Task<float> GetTemperatureAsync()
        {
            var batchId = await brewFatherApi.GetBatchIdAsync();
            var temp = await brewFatherApi.GetTemperatureAsync(batchId);

            logger.LogInformation($"Temperature is {temp}");

            return temp;
        }
    }
}