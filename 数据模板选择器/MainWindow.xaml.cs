using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 数据模板选择器
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            BindingList<int>
        }
    }


    public class Device
    {
        public enum CommunicationInterface
        {
            Socket,
            SerialPort
        }

        public string Name { get; set; }
        public string Port { get; set; }
        public CommunicationInterface Interface { get; set; }
    }

    public class DeviceDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SocketDataTemplate { get; set; }
        public DataTemplate SerialPortDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var device = (Device)item;
            if (device.Interface == Device.CommunicationInterface.Socket)
            {
                return SocketDataTemplate;
            }
            else if(device.Interface == Device.CommunicationInterface.SerialPort)
            {
                return SerialPortDataTemplate;
            }

            throw new ApplicationException();
        }
    }
}
