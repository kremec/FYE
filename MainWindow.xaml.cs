using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FYE
{
    public partial class MainWindow : Window
    {
        ElectricityData newData = new ElectricityData();

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;

            LoadInitialSettings();
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
            ImportMeasurementsJson(newData);
        }
        private void ImportMeasurementsJson(ElectricityData newData)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open JSON data files";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                LoadMeasurementsJson(openFileDialog.FileNames.ToList());
            }
        }
        private void LoadMeasurementsJson(List<string> filePaths)
        {
            newData = new ElectricityData();

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
                        var oldDataBetter = JsonConvert.DeserializeObject<ElectricityData>(jsonString);
                        if (oldDataBetter != null && oldDataBetter.Measurements != null)
                        {
                            newData.Measurements.AddRange(oldDataBetter.Measurements);
                            newData.RegisterMax_1003 = Math.Max(newData.RegisterMax_1003, oldDataBetter.RegisterMax_1003);
                            newData.RegisterMin_1003 = newData.RegisterMin_1003 == 0 ? oldDataBetter.RegisterMin_1003 : Math.Min(newData.RegisterMin_1003, oldDataBetter.RegisterMin_1003);
                            newData.RegisterSum_1003 += oldDataBetter.RegisterSum_1003;
                        }

                        var oldDataDefault = JsonConvert.DeserializeObject<ElektroMeritve>(jsonString);
                        if (oldDataDefault != null && oldDataDefault.data != null)
                        {
                            newData.Measurements.AddRange(from oldMeasurement in oldDataDefault.data.meritve
                                                          let newMeasurement = new ElectricityMeasurement() { Date = oldMeasurement.datum, Measurement_1003 = oldMeasurement.registri._1003 }
                                                          where oldMeasurement.datum.Date <= DateTime.Today
                                                          select newMeasurement);
                            newData.RegisterMax_1003 = Math.Max(newData.RegisterMax_1003, oldDataDefault.data.registerMaxValue._1003);
                            newData.RegisterMin_1003 = newData.RegisterMin_1003 == 0 ? oldDataDefault.data.registerMinValue._1003 : Math.Min(newData.RegisterMin_1003, oldDataDefault.data.registerMinValue._1003);
                            newData.RegisterSum_1003 += oldDataDefault.data.vsotaRegistrov._1003;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The file in location: " + filePath + " does not exist!\nRemoving it from default import list.", "ERROR");
                    return;
                }
            }

            if (newData.Measurements.Count > 0)
            {
                newData.DateTimeFrom = newData.Measurements.Min(measurement => measurement.Date);
                newData.DateTimeTo = newData.Measurements.Max(measurement => measurement.Date);
            }
        }

        private void ExportJSONButton_Click(object sender, RoutedEventArgs e)
        {
            if (newData.Measurements.Count > 0)
                ExportBetterMeasurementsJson(newData);
            else
                MessageBox.Show("Cannot export JSON as there is no data to be exported! Import new data files!", "ERROR");
        }
        private void ExportBetterMeasurementsJson(ElectricityData newData)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save JSON in better format";
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.FileName = "ElectricityData_" + newData.DateTimeFrom.ToString("dd-MM-yyyy") + "_" + newData.DateTimeTo.ToString("dd-MM-yyyy");
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