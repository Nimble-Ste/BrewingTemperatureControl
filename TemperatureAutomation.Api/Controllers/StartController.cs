namespace TemperatureAutomation.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TemperatureMonitor;

    [Route("api/[controller]")]
    [ApiController]
    public class StartController
        (ILogger<StartController> logger, TemperatureMonitorService temperatureMonitorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Starting monitor");

            await temperatureMonitorService.StartMonitoringAsync();

            return Ok();
        }
    }
}