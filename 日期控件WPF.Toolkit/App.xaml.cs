using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace 日期控件WPF.Toolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Xceed.Wpf.Toolkit.Licenser.LicenseKey = "WTK43-CU520-TX2NS-9T7A";

        }
    }
}
