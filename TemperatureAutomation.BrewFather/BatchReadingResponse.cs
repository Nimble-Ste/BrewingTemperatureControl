namespace TemperatureAutomation.BrewFather
{
    public class BatchReadingResponse
    {
        public string id { get; set; }
        public string type { get; set; }
        public float temp { get; set; }
        public int rssi { get; set; }
        public float sg { get; set; }
        public float angle { get; set; }
        public long time { get; set; }
        public float battery { get; set; }
    }

}
