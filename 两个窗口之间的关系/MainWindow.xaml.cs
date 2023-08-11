using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace 两个窗口之间的关系
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.Dispatcher.Invoke(() =>
                {
                    var a = new AlarmWindow();
                    a.Title = "1000";
                    a.ShowDialog();
                });
            });

            Task.Run(() =>
            {
                Thread.Sleep(2000);
                this.Dispatcher.Invoke(() =>
                {
                    var a = new AlarmWindow();
                    a.Title = "2000";
                    a.ShowDialog();
                });
            });


            Task.Run(() =>
            {
                Thread.Sleep(3000);
                this.Dispatcher.Invoke(() =>
                {
                    var a = new AlarmWindow();
                    a.Title = "3000";
                    a.ShowDialog();
                });
            });


            Task.Run(() =>
            {
                Thread.Sleep(4000);
                this.Dispatcher.Invoke(() =>
                {
                    var a = new AlarmWindow();
                    a.Title = "4000";
                    a.Topmost = true;
                    a.Show();
                });
            });


            Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(() =>
                {
                    var a = new AlarmWindow();
                    a.Title = "5000";
                    a.Topmost = true;
                    a.ShowDialog();
                });
            });
        }

        private void HideButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();

            Task.Run(() =>
            {
                Thread.Sleep(3000);
                this.Dispatcher.Invoke(() =>
                {
                    this.Activate();
                    this.Show();
                });
            });
        }

        private void ShowButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void ActiateButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //AlarmWindow aw = new AlarmWindow();
            //aw.Owner = this;
            //aw.Show();
        }
    }
}
