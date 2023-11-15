namespace TemperatureAutomation.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TemperatureAutomation.SmartPlug;
    using TemperatureAutomation.TemperatureMonitor;

    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(BrewFatherService brewFatherService, SmartPlugService smartPlugService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var temperatureTask = brewFatherService.GetTemperatureAsync();
            var plugStatusTask = smartPlugService.IsOnAsync();

            await Task.WhenAll(temperatureTask, plugStatusTask);

            return Ok(new Status(await temperatureTask, await plugStatusTask));
        }

        public record Status(float temperature, bool plugIsOn);
    }
}
