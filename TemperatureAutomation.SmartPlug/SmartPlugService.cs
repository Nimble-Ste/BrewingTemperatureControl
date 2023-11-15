namespace TemperatureAutomation.SmartPlug
{
    public class SmartPlugService
    {
        public async Task<bool> IsOnAsync()
        {
            await Task.Delay(100);

            return true;
        }

        public async Task TurnOnAsync()
        {
            await Task.Delay(100);
        }

        public async Task TurnOffAsync()
        {
            await Task.Delay(100);
        }
    }
}
