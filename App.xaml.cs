using System.Configuration;
using System.Data;
using System.Windows;

namespace FYE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIfEx+WmFZfV1gfF9CZVZURmYuP1ZhSXxQd0djWH9WdHFQRGlYUEU=");
        }
    }

}
