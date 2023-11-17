namespace TemperatureAutomation.SmartPlug
{
    using Newtonsoft.Json;
    using System.Text.Json.Serialization;


    public class ShellyResponse
    {
        public bool isok { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Device_Status device_status { get; set; }
    }

    public class Device_Status
    {
        [JsonPropertyName("switch:0")]
        public Switch dswitch { get; set; }

    }

    public class Switch
    {
        public bool output { get; set; }
    }
}