using Newtonsoft.Json;

namespace FYE
{
    public class ElektroMeritve
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public List<Meritve> meritve { get; set; }
        public VsotaRegistrov vsotaRegistrov { get; set; }
        public RegisterMinValue registerMinValue { get; set; }
        public RegisterMaxValue registerMaxValue { get; set; }
    }

    public class Meritve
    {
        public DateTime datum { get; set; }
        public Registri registri { get; set; }
    }
    public class Registri
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }
    }
    public class _1003
    {
        public string naziv { get; set; }
        public string enotaMoc { get; set; }
        public string enotaEnergija { get; set; }
        public string barva { get; set; }
    }

    public class VsotaRegistrov
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }
    }

    public class RegisterMinValue
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }
    }

    public class RegisterMaxValue
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }
    }
}
