namespace TemperatureAutomation.BrewFather
{
    public class BatchResponse
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int batchNo { get; set; }
        public string status { get; set; }
        public string brewer { get; set; }
        public long brewDate { get; set; }
        public Recipe recipe { get; set; }
    }

    public class Recipe
    {
        public string name { get; set; }
    }
}