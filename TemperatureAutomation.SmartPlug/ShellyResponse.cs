namespace TemperatureAutomation.SmartPlug
{
    using Newtonsoft.Json;
    using System.Text.Json.Serialization;

    public class ShellyResponse
    {
        public bool isok { get; set; }
        public Data data { get; set; }
    }

    public class Aenergy
    {
        public List<int> by_minute { get; set; }
        public int minute_ts { get; set; }
        public double total { get; set; }
    }

    public class AvailableUpdates
    {
        public Beta beta { get; set; }
        public Stable stable { get; set; }
    }

    public class Beta
    {
        public string version { get; set; }
    }

    public class Cloud
    {
        public bool connected { get; set; }
    }

    public class Data
    {
        public bool online { get; set; }
        public DeviceStatus device_status { get; set; }
    }

    public class DeviceStatus
    {
        public string code { get; set; }
        public List<object> pluguk_ui { get; set; }
        public Ws ws { get; set; }
        public List<object> ble { get; set; }

        [JsonPropertyName("switch:0")]
        public Switch0 switch0 { get; set; }
        public string _updated { get; set; }
        public Sys sys { get; set; }
        public double serial { get; set; }
        public Cloud cloud { get; set; }
        public Wifi wifi { get; set; }
        public string id { get; set; }
        public Mqtt mqtt { get; set; }
    }

    public class Mqtt
    {
        public bool connected { get; set; }
    }

    public class Stable
    {
        public string version { get; set; }
    }

    public class Switch0
    {
        public int id { get; set; }
        public Aenergy aenergy { get; set; }
        public string source { get; set; }
        public bool output { get; set; }
        public int apower { get; set; }
        public double voltage { get; set; }
        public int current { get; set; }
        public Temperature temperature { get; set; }
    }

    public class Sys
    {
        public int cfg_rev { get; set; }
        public string mac { get; set; }
        public bool restart_required { get; set; }
        public string time { get; set; }
        public int unixtime { get; set; }
        public int uptime { get; set; }
        public int ram_size { get; set; }
        public int ram_free { get; set; }
        public int fs_size { get; set; }
        public int fs_free { get; set; }
        public int kvs_rev { get; set; }
        public int schedule_rev { get; set; }
        public int webhook_rev { get; set; }
        public AvailableUpdates available_updates { get; set; }
    }

    public class Temperature
    {
        public double tC { get; set; }
        public double tF { get; set; }
    }

    public class Wifi
    {
        public string sta_ip { get; set; }
        public string status { get; set; }
        public string ssid { get; set; }
        public int rssi { get; set; }
    }

    public class Ws
    {
        public bool connected { get; set; }
    }



}
