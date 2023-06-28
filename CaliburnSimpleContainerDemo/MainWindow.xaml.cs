using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using System.Windows;

// public void RegisterSingleton(Type service, string key, Type implementation)
//  public static SimpleContainer Singleton<TService, TImplementation>(
//this SimpleContainer container,
//string key = null)
//where TImplementation : TService

//public static SimpleContainer Singleton<TImplementation>(
//    this SimpleContainer container,
//    string key = null)

namespace CaliburnSimpleContainerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimpleContainer container = new SimpleContainer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OneSingletonImplementAuto_OnClick(object sender, RoutedEventArgs e)
        {
            container.RegisterSingleton(typeof(Bus), null, typeof(Bus));
            var vehicle = container.GetInstance<Bus>();
            vehicle.Run();
        }

        private void NSingletonImplementAuto_OnClick(object sender, RoutedEventArgs e)
        {
            container.RegisterSingleton(typeof(Bus), "bus1", typeof(Bus));

          

            //var bus2 = container.GetInstance(typeof(Bus), "bus2") as Bus;
          
            //bus2?.Run();
        }

        private void OneSingletonInterfaceAuto_OnClick(object sender, RoutedEventArgs e)
        {
            container.RegisterSingleton(typeof(IVehicle), null, typeof(Car));
            container.RegisterSingleton(typeof(IVehicle), null, typeof(Bus));
            container.RegisterSingleton(typeof(IVehicle), null, typeof(Bus));
        }

        private void NSingletonInterfaceAuto_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}