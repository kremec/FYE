using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FYE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ElectricityData newData = new ElectricityData();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open JSON data files";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach(string filePath in openFileDialog.FileNames)
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        var oldData = JsonConvert.DeserializeObject<ElektroMeritve>(sr.ReadToEnd());
                        if (oldData != null)
                        {
                            newData.Measurements.AddRange(from oldMeasurement in oldData.data.meritve
                                                          let newMeasurement = new ElectricityMeasurement() { Date = oldMeasurement.datum, Measurement_1003 = oldMeasurement.registri._1003 }
                                                          where oldMeasurement.datum.Date <= DateTime.Today
                                                          select newMeasurement);
                            newData.RegisterMax_1003 = Math.Max(newData.RegisterMax_1003, oldData.data.registerMaxValue._1003);
                            newData.RegisterMin_1003 = newData.RegisterMin_1003 == 0 ? oldData.data.registerMinValue._1003 : Math.Min(newData.RegisterMin_1003, oldData.data.registerMinValue._1003);
                            newData.RegisterSum_1003 += oldData.data.vsotaRegistrov._1003;
                        }
                    }
                }

                newData.DateTimeFrom = newData.Measurements.Min(measurement => measurement.Date);
                newData.DateTimeTo = newData.Measurements.Max(measurement => measurement.Date);

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
                Debug.WriteLine("TEST");
            }
        }
    }
}