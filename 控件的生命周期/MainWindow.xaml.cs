using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace 控件的生命周期
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object o;
        public MainWindow()
        {
            InitializeComponent();
            o = Button1;

            Button1.Initialized += (sender, args) =>
            {
                Console.WriteLine("Initialized");
            };

            Button1.Loaded += Button1_Loaded;

            Button1.Unloaded += Button1_Unloaded;


            Button1.Click += (sender, args) =>
            {
                Console.WriteLine("Click");
            };

            this.Button1.IsVisibleChanged += (sender, args) =>
            {
                Console.WriteLine("IsVisibleChanged");
            };
        }

        private void Button1_Unloaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Unloaded");
        }

        private void Button1_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Loaded");
        }
    }
}
