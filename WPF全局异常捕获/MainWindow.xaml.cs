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

namespace WPF全局异常捕获
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 点击按钮事件
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 案例1
            throw new ArgumentException(); // 先触发Application.DispatcherUnhandledException,可能又触发AppDomain.UnhandledException事件

            // 案例2
            Thread thread = new Thread(() =>
            {
                throw new NotImplementedException(); // 触发AppDomain.UnhandledException事件，程序退出。
            });
            thread.Start();

            // 案例3
            Task.Run(() =>
            {
                try
                {
                    // 线程池线程DO Something...
                }
                catch (Exception exception)
                {
                    Application.Current.Dispatcher.Invoke(() => throw exception);
                }
            });
        }
    }
}
