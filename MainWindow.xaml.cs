using Microsoft.Win32;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FYE
{
    public partial class MainWindow : Window
    {
        OmrežninaPodatki podatki = new OmrežninaPodatki();

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;

            LoadInitialSettings();
            int test = OmreznineMetode.PridobiBlokZaČas(new DateTime(2022, 4, 18, 12, 15, 0));
            Debug.WriteLine(test);
        }
        private void LoadInitialSettings()
        {
            if (Properties.Settings.Default.ImportJsonLocations != null)
            {
                List<string> filePaths = [.. Properties.Settings.Default.ImportJsonLocations];
                LoadMeasurementsJson(filePaths);
            }
        }

        private void ImportJSONButton_Click(object sender, RoutedEventArgs e)
        {
            ImportMeasurementsJson(podatki);
        }
        private void ImportMeasurementsJson(OmrežninaPodatki newData)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Uvozi JSON datoteke s podatki omrežnin";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                LoadMeasurementsJson(openFileDialog.FileNames.ToList());
            }
        }
        private void LoadMeasurementsJson(List<string> filePaths)
        {
            podatki = new OmrežninaPodatki();

            if (Properties.Settings.Default.ImportJsonLocations == null)
                Properties.Settings.Default.ImportJsonLocations = new System.Collections.Specialized.StringCollection();
            else
                Properties.Settings.Default.ImportJsonLocations.Clear();

            foreach (string filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    Properties.Settings.Default.ImportJsonLocations.Add(filePath);
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        string jsonString = sr.ReadToEnd();
                        var oldDataBetter = JsonConvert.DeserializeObject<OmrežninaPodatki>(jsonString);
                        if (oldDataBetter != null && oldDataBetter.MeritveMoč != null)
                        {
                            podatki.MeritveMoč.AddRange(oldDataBetter.MeritveMoč);
                            podatki.MaxMoč_1003 = Math.Max(podatki.MaxMoč_1003, oldDataBetter.MaxMoč_1003);
                            podatki.MinMoč_1003 = podatki.MinMoč_1003 == 0 ? oldDataBetter.MinMoč_1003 : Math.Min(podatki.MinMoč_1003, oldDataBetter.MinMoč_1003);
                            podatki.VsotaMoč_1003 += oldDataBetter.VsotaMoč_1003;
                        }

                        var oldDataDefault = JsonConvert.DeserializeObject<ElektroMeritve>(jsonString);
                        if (oldDataDefault != null && oldDataDefault.data != null)
                        {
                            podatki.MeritveMoč.AddRange(from oldMeasurement in oldDataDefault.data.meritve
                                                          let newMeasurement = new MeritevMoč() { Čas = oldMeasurement.datum, Meritev_1003 = oldMeasurement.registri._1003 }
                                                          where oldMeasurement.datum.Date <= DateTime.Today
                                                          select newMeasurement);
                            podatki.MaxMoč_1003 = Math.Max(podatki.MaxMoč_1003, oldDataDefault.data.registerMaxValue._1003);
                            podatki.MinMoč_1003 = podatki.MinMoč_1003 == 0 ? oldDataDefault.data.registerMinValue._1003 : Math.Min(podatki.MinMoč_1003, oldDataDefault.data.registerMinValue._1003);
                            podatki.VsotaMoč_1003 += oldDataDefault.data.vsotaRegistrov._1003;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Datoteka na lokaciji: " + filePath + " ne obstaja več!", "ERROR");
                    return;
                }
            }

            if (podatki.MeritveMoč.Count > 0)
            {
                podatki.ČasOd = podatki.MeritveMoč.Min(measurement => measurement.Čas);
                podatki.ČasDo = podatki.MeritveMoč.Max(measurement => measurement.Čas);
            }
        }

        private void ExportJSONButton_Click(object sender, RoutedEventArgs e)
        {
            if (podatki.MeritveMoč.Count > 0)
                ExportBetterMeasurementsJson(podatki);
            else
                MessageBox.Show("Izvozitev JSON ni možna, uvozite nove podatke!", "ERROR");
        }
        private void ExportBetterMeasurementsJson(OmrežninaPodatki newData)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Izvozitev JSON podatkov";
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.FileName = "ElectricityData_" + newData.ČasOd.ToString("dd-MM-yyyy") + "_" + newData.ČasDo.ToString("dd-MM-yyyy");
            saveFileDialog.DefaultExt = ".json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string jsonNewData = JsonConvert.SerializeObject(newData);
                File.WriteAllText(saveFileDialog.FileName, jsonNewData);
            }
        }


        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}