using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            base.OnStartup(e);
            SimpleContainer container = new SimpleContainer();
            container.Instance(container);
            container.Singleton<IVehicle, Bus>();


            Console.WriteLine(container.GetInstance<IVehicle>().GetHashCode());
            Console.WriteLine(container.GetInstance<IVehicle>().GetHashCode());
            Console.WriteLine(container.GetInstance<IVehicle>().GetHashCode());
        }

        class Target
        {
            public Target(IVehicle vehicles)
            {
                
            }
        }
       
    }
}
