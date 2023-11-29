using Newtonsoft.Json;

namespace FYE
{
    public class ElectricityData
    {
        public ElectricityData()
        {
            Measurements = new List<ElectricityMeasurement>();
        }
        public List<ElectricityMeasurement> Measurements { get; set; }
        public double RegisterSum_1003 { get; set; }
        public double RegisterMin_1003 { get; set; }
        public double RegisterMax_1003 { get; set; }
        public DateTime DateTimeFrom {  get; set; }
        public DateTime DateTimeTo { get; set; }
    }

    public class ElectricityMeasurement
    {
        public DateTime Date { get; set; }
        public double Measurement_1003 { get; set; }
    }
}
