using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FYE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Odpri JSON datoteke";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach(string filePath in openFileDialog.FileNames)
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        var myObject = JsonConvert.DeserializeObject<ElektroMeritve>(sr.ReadToEnd());
                        Debug.WriteLine("Test");
                    }
                }
            }
        }
    }

    public class ElektroMeritve
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public List<Meritve> meritve { get; set; }
        public ParametriRegistrov parametriRegistrov { get; set; }
        public VsotaRegistrov vsotaRegistrov { get; set; }
        public RegisterMinValue registerMinValue { get; set; }
        public RegisterMaxValue registerMaxValue { get; set; }
        public List<object> flatOdbirki { get; set; }
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

        [JsonProperty("1004")]
        public double _1004 { get; set; }
    }
    public class ParametriRegistrov
    {
        [JsonProperty("1003")]
        public _1003 _1003 { get; set; }

        [JsonProperty("1004")]
        public _1004 _1004 { get; set; }
    }
    public class _1003
    {
        public string naziv { get; set; }
        public string enotaMoc { get; set; }
        public string enotaEnergija { get; set; }
        public string barva { get; set; }
    }

    public class _1004
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

        [JsonProperty("1004")]
        public double _1004 { get; set; }
    }

    public class RegisterMinValue
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }

        [JsonProperty("1004")]
        public double _1004 { get; set; }
    }

    public class RegisterMaxValue
    {
        [JsonProperty("1003")]
        public double _1003 { get; set; }

        [JsonProperty("1004")]
        public double _1004 { get; set; }
    }
}