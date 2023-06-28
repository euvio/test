using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SyncFusionDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Xceed.Wpf.Toolkit.Licenser.LicenseKey = ("WTK43-3UJ2R-CXCXH-5TFA");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTExOTcyQDMxMzkyZTM0MmUzMFRXV01UZ3V3VGs5T0xlWUVLRVJIS09xYmtvZU1HQkxuMkxXWkx2OU1HMWs9");
        }
    }
}
