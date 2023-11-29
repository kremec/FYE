namespace FYE
{
    public class OmrežninaPodatki
    {
        public OmrežninaPodatki()
        {
            MeritveMoč = new List<MeritevMoč>();
        }
        public List<MeritevMoč> MeritveMoč { get; set; }
        public double VsotaMoč_1003 { get; set; }
        public double MinMoč_1003 { get; set; }
        public double MaxMoč_1003 { get; set; }
        public DateTime ČasOd {  get; set; }
        public DateTime ČasDo { get; set; }
    }

    public class MeritevMoč
    {
        public DateTime Čas { get; set; }
        public double Meritev_1003 { get; set; }
    }
}
