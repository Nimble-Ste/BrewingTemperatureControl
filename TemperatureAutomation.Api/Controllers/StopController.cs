namespace TemperatureAutomation.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TemperatureMonitor;

    [Route("api/[controller]")]
    [ApiController]
    public class StopController (ILogger<StartController> logger, TemperatureMonitorService temperatureMonitorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Stop");

            await temperatureMonitorService.StopMonitoringAsync();

            return Ok();
        }
    }
}
