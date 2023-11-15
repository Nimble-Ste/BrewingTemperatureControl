namespace TemperatureAutomation.TemperatureMonitor
{
    using BrewFather;

    public class BrewFatherService(BrewFatherApi brewFatherApi)
    {
        public async Task<float> GetTemperatureAsync()
        {
            var batchId = await brewFatherApi.GetBatchIdAsync();
            var temp = await brewFatherApi.GetTemperatureAsync(batchId);

            return temp;
        }
    }
}