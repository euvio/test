using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Caliburn.Micro;

namespace CaliburnSimpleContainerDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SimpleContainer container = new SimpleContainer();
            // 注册单例
            container.RegisterSingleton(typeof(IVehicle), null, typeof(Bus));
            container.RegisterSingleton(typeof(IVehicle), null, typeof(Bus));

            var bus = container.GetAllInstances(typeof(IVehicle), null);

            Console.WriteLine(bus);

            foreach (var bu in bus)
            {
                Console.WriteLine(bu);
            }

            //container.RegisterInstance(typeof(IVehicle), null, new Bus("BMW"));
            //container.RegisterPerRequest(typeof(IVehicle), null, typeof(Bus));

            //container.RegisterSingleton(typeof(Bus), null, typeof(Bus));

        }
    }
}
