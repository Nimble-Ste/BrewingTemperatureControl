namespace TemperatureAutomation.TemperatureMonitor
{
    using System.Reactive.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SmartPlug;

    public class TemperatureMonitorService(BrewFatherService brewFatherService, SmartPlugService smartPlugService, IConfiguration configuration,
        ILogger<TemperatureMonitorService> logger)
    {
        private IDisposable? MonitoringLoop;

        private float minTemp = configuration.GetValue<float>("MinTemp");
        private float maxTemp = configuration.GetValue<float>("MaxTemp");

        private bool plugIsOn;


        public async Task StartMonitoringAsync()
        {

            this.plugIsOn = await smartPlugService.IsOnAsync();

            logger.LogInformation($"using min temp {minTemp}");

            logger.LogInformation($"using max temp {maxTemp}");

            if (MonitoringLoop != null)
            {
                logger.LogInformation("Already monitoring");
            }
            else
            {

                MonitoringLoop = Observable.Interval(TimeSpan.FromMinutes(10)).StartWith(0).Subscribe(async _ =>
                {
                    try
                    {

                        logger.LogInformation($"Is Plug On {plugIsOn}");

                        var temp = await brewFatherService.GetTemperatureAsync();

                        if (!plugIsOn && temp < minTemp)
                        {

                            plugIsOn = true;

                            logger.LogInformation("Plug Turning On");

                            await smartPlugService.TurnOnAsync();
                        }

                        if (plugIsOn && temp > maxTemp)
                        {
                            plugIsOn = false;
                            logger.LogInformation("Plug Turning Off");

                            await smartPlugService.TurnOffAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex, ex.Message);
                        await this.StopMonitoringAsync();
                    }
                });
            }


        }

        public async Task StopMonitoringAsync()
        {
            logger.LogInformation("Stop Monitoring. Turning Plug Off");

            plugIsOn = false;

            await smartPlugService.TurnOffAsync();

            MonitoringLoop?.Dispose();
            MonitoringLoop = null;
        }
    }
}
