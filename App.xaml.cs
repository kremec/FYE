using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace FYE
{
    public partial class App : Application
    {
        public App()
        {
            string appPath = Directory.GetCurrentDirectory();
            string licencePath = Path.Combine(appPath, "SyncfusionLicence.json");
            string key = String.Empty;
            if (!File.Exists(licencePath))
            {
                MessageBox.Show($"Could not find the licence key file in: {licencePath}", "ERROR");
            }
            else
            {
                LicenceKey licenceKey = JsonConvert.DeserializeObject<LicenceKey>(File.ReadAllText(licencePath));
                if (licenceKey != null)
                {
                    key = licenceKey.Key;

                }
            }
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);
        }
    }

    public class LicenceKey()
    {
        public string Key { get; set; } = String.Empty;
    }
}
