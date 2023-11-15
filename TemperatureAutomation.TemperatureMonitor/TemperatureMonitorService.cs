namespace TemperatureAutomation.TemperatureMonitor
{
    using System.Reactive.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SmartPlug;

    public class TemperatureMonitorService(BrewFatherService brewFatherService, SmartPlugService smartPlugService, IConfiguration configuration,
        ILogger<TemperatureMonitorService> logger)
    {
        private IDisposable MonitoringLoop;

        private float minTemp = configuration.GetValue<float>("MinTemp");
        private float maxTemp = configuration.GetValue<float>("MaxTemp");


        public async Task StartMonitoringAsync()
        {
            MonitoringLoop = Observable.Interval(TimeSpan.FromSeconds(30)).StartWith(0).Subscribe(async _ =>
            {
                var isPlugOn = await smartPlugService.IsOnAsync();

                var temp = await brewFatherService.GetTemperatureAsync();

                if (!isPlugOn && temp < minTemp)
                {

                    logger.LogInformation("Plug Turning On");

                    await smartPlugService.TurnOnAsync();
                }

                if (isPlugOn && temp > maxTemp)
                {
                    logger.LogInformation("Plug Turning Off");

                    await smartPlugService.TurnOffAsync();
                }

            });
        }

        public async Task StopMonitoringAsync()
        {
            logger.LogInformation("Stop Monitoring. Turning Plug Off");

            await smartPlugService.TurnOffAsync();

            MonitoringLoop?.Dispose();
        }
    }
}
