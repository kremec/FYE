using System.Collections.ObjectModel;

namespace FYE.DataObjects
{
    public class OmrežninaPodatki
    {
        public OmrežninaPodatki()
        {
            MeritveMoč = new List<MeritevMoč>();
            MesečniPodatki = new List<MesečniPodatki>();
            ŠteviloPreseženihIntervalovBloka = new int[5];
            CenaPreseženihIntervalovBloka = new int[5];
        }
        public List<MeritevMoč> MeritveMoč { get; set; }
        public double VsotaMoč_1003 { get; set; }
        public double MinMoč_1003 { get; set; }
        public double MaxMoč_1003 { get; set; }
        public DateTime ČasOd { get; set; }
        public DateTime ČasDo { get; set; }
        public List<MesečniPodatki> MesečniPodatki { get; set; }
        public int[] ŠteviloPreseženihIntervalovBloka;
        public int[] CenaPreseženihIntervalovBloka;
    }

    public class MeritevMoč
    {
        public DateTime Čas { get; set; }
        public double Meritev_1003 { get; set; }
    }

    public class MesečniPodatki
    {
        public MesečniPodatki()
        {
            ŠteviloPreseženihIntervalovBloka = new int[5];
            CenaPreseženihIntervalovBloka = new int[5];
        }
        public int Leto { get; set; }
        public int Mesec { get; set; }
        public int[] ŠteviloPreseženihIntervalovBloka;
        public int[] CenaPreseženihIntervalovBloka;
    }
}
