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
}