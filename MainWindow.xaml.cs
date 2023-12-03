using FYE.DataMethods;
using FYE.DataObjects;
using Microsoft.Win32;
using Newtonsoft.Json;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace FYE
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;

            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;

            LoadInitialSettings();
        }
        private void LoadInitialSettings()
        {
            if (Properties.Settings.Default.ImportJsonLocations != null)
            {
                List<string> filePaths = [.. Properties.Settings.Default.ImportJsonLocations];
                LoadMeasurementsJson(filePaths);
            }

            ViewModel.DatumOd = Properties.Settings.Default.DatumOd;
            ViewModel.DatumDo = Properties.Settings.Default.DatumDo;

            ViewModel.MočBlok1 = Properties.Settings.Default.DogovorjenaMočBlok1;
            ViewModel.MočBlok2 = Properties.Settings.Default.DogovorjenaMočBlok2;
            ViewModel.MočBlok3 = Properties.Settings.Default.DogovorjenaMočBlok3;
            ViewModel.MočBlok4 = Properties.Settings.Default.DogovorjenaMočBlok4;
            ViewModel.MočBlok5 = Properties.Settings.Default.DogovorjenaMočBlok5;
        }

        private void ImportJSONButton_Click(object sender, RoutedEventArgs e)
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
            ViewModel.Podatki = new OmrežninaPodatki();

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
                            ViewModel.Podatki.MeritveMoč.AddRange(oldDataBetter.MeritveMoč);
                            ViewModel.Podatki.MaxMoč_1003 = Math.Max(ViewModel.Podatki.MaxMoč_1003, oldDataBetter.MaxMoč_1003);
                            ViewModel.Podatki.MinMoč_1003 = ViewModel.Podatki.MinMoč_1003 == 0 ? oldDataBetter.MinMoč_1003 : Math.Min(ViewModel.Podatki.MinMoč_1003, oldDataBetter.MinMoč_1003);
                            ViewModel.Podatki.VsotaMoč_1003 += oldDataBetter.VsotaMoč_1003;
                        }

                        var oldDataDefault = JsonConvert.DeserializeObject<ElektroMeritve>(jsonString);
                        if (oldDataDefault != null && oldDataDefault.data != null)
                        {
                            ViewModel.Podatki.MeritveMoč.AddRange(from oldMeasurement in oldDataDefault.data.meritve
                                                          let newMeasurement = new MeritevMoč() { Čas = oldMeasurement.datum, Meritev_1003 = oldMeasurement.registri._1003 }
                                                          select newMeasurement);
                            ViewModel.Podatki.MaxMoč_1003 = Math.Max(ViewModel.Podatki.MaxMoč_1003, oldDataDefault.data.registerMaxValue._1003);
                            ViewModel.Podatki.MinMoč_1003 = ViewModel.Podatki.MinMoč_1003 == 0 ? oldDataDefault.data.registerMinValue._1003 : Math.Min(ViewModel.Podatki.MinMoč_1003, oldDataDefault.data.registerMinValue._1003);
                            ViewModel.Podatki.VsotaMoč_1003 += oldDataDefault.data.vsotaRegistrov._1003;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Datoteka na lokaciji: " + filePath + " ne obstaja več!", "ERROR");
                    return;
                }
            }

            if (ViewModel.Podatki.MeritveMoč.Count > 0)
            {
                ViewModel.Podatki.ČasOd = ViewModel.Podatki.MeritveMoč.Min(measurement => measurement.Čas);
                ViewModel.Podatki.ČasDo = ViewModel.Podatki.MeritveMoč.Max(measurement => measurement.Čas);
            }
        }

        private void ExportJSONButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Podatki.MeritveMoč.Count > 0)
                ExportBetterMeasurementsJson();
            else
                MessageBox.Show("Izvozitev JSON ni možna, uvozite nove podatke!", "ERROR");
        }
        private void ExportBetterMeasurementsJson()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Izvozitev JSON podatkov";
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.FileName = "ElectricityData_" + ViewModel.Podatki.ČasOd.ToString("dd-MM-yyyy") + "_" + ViewModel.Podatki.ČasDo.ToString("dd-MM-yyyy");
            saveFileDialog.DefaultExt = ".json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string jsonNewData = JsonConvert.SerializeObject(ViewModel.Podatki);
                File.WriteAllText(saveFileDialog.FileName, jsonNewData);
            }
        }


        private void ParametersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.Podatki.MesečniPodatki.Clear();
            ViewModel.PodatkiOgled.Clear();
            var podatkiPoMesecuLetu = ViewModel.Podatki.MeritveMoč
                .GroupBy(meritev => new { meritev.Čas.Year, meritev.Čas.Month });
            foreach (var skupina in podatkiPoMesecuLetu)
            {
                int leto = skupina.Key.Year;
                int mesec = skupina.Key.Month;

                MesečniPodatki mesečniPodatki = new MesečniPodatki() { Leto = leto, Mesec = mesec };

                var meritveLetoMesec = skupina.ToList();
                foreach (var meritev in meritveLetoMesec)
                {
                    int blokMeritve = OmreznineMetode.PridobiBlokZaČas(meritev.Čas);
                    double dogovorjenaMočBloka = ViewModel.MočBloka(blokMeritve);
                    if (dogovorjenaMočBloka != -1)
                    {
                        if (meritev.Meritev_1003 > dogovorjenaMočBloka)
                        {
                            mesečniPodatki.ŠteviloPreseženihIntervalovBloka[blokMeritve - 1]++;
                            ViewModel.Podatki.ŠteviloPreseženihIntervalovBloka[blokMeritve - 1]++;
                        }
                    }
                }

                ViewModel.Podatki.MesečniPodatki.Add(mesečniPodatki);
            }

            foreach (var mesečniPodatki in ViewModel.Podatki.MesečniPodatki)
            {
                DateTime časPodatkov = new DateTime(mesečniPodatki.Leto, mesečniPodatki.Mesec, 1);
                if (časPodatkov.Date >= ViewModel.DatumOd.Date && časPodatkov.Date <= ViewModel.DatumDo.Date)
                {
                    PodatkiOgled podatki = new PodatkiOgled()
                    {
                        Leto = mesečniPodatki.Leto,
                        Mesec = mesečniPodatki.Mesec,
                        ŠteviloPrekoračitevBlok1 = mesečniPodatki.ŠteviloPreseženihIntervalovBloka[0],
                        ŠteviloPrekoračitevBlok2 = mesečniPodatki.ŠteviloPreseženihIntervalovBloka[1],
                        ŠteviloPrekoračitevBlok3 = mesečniPodatki.ŠteviloPreseženihIntervalovBloka[2],
                        ŠteviloPrekoračitevBlok4 = mesečniPodatki.ŠteviloPreseženihIntervalovBloka[3],
                        ŠteviloPrekoračitevBlok5 = mesečniPodatki.ŠteviloPreseženihIntervalovBloka[4]
                    };
                    ViewModel.PodatkiOgled.Add(podatki);
                }
            }
        }


        public void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            Properties.Settings.Default.DatumOd = ViewModel.DatumOd;
            Properties.Settings.Default.DatumDo = ViewModel.DatumDo;

            Properties.Settings.Default.DogovorjenaMočBlok1 = ViewModel.MočBlok1;
            Properties.Settings.Default.DogovorjenaMočBlok2 = ViewModel.MočBlok2;
            Properties.Settings.Default.DogovorjenaMočBlok3 = ViewModel.MočBlok3;
            Properties.Settings.Default.DogovorjenaMočBlok4 = ViewModel.MočBlok4;
            Properties.Settings.Default.DogovorjenaMočBlok5 = ViewModel.MočBlok5;

            Properties.Settings.Default.Save();
        }
    }
}