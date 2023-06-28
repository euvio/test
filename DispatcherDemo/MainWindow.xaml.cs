using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DispatcherDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application

            InitializeComponent();
            Console.WriteLine("Main ui Thread: " + Thread.CurrentThread.ManagedThreadId); // 输出 1
            Button btn = null;
            Thread thread = new Thread(() =>
            {
                btn = new Button();
                btn.Width = 200;
                btn.Height = 200;
                btn.Background = Brushes.DarkSalmon;
                Console.WriteLine("second ui Thread: " + Thread.CurrentThread.ManagedThreadId); // 非1的正整数
                System.Windows.Threading.Dispatcher.Run(); // 这一步骤很重要，它会开启当前线程关联的Dispatcher的消息循环机制，这样通过Dispatcher.Invoke()推送
                //的代码才会在关联线程上执行，同时这行代码是阻塞的，保证当前线程不会结束，一直循环监控等待Invoke封送。
            });

            thread.SetApartmentState(ApartmentState.STA); // 必须先设置成单线程模型再启动
            thread.IsBackground = true;
            thread.Start();
            Thread.Sleep(200);
            System.Windows.Threading.Dispatcher.FromThread(thread).BeginInvoke(() =>
            {
                btn.Content = DateTime.Now;
                var w = new Window();
                w.Content = btn;
                w.Show();
            });
        }
    }
}

// DispatcherObject

// Dispatcher

// Application.Run();

// Dispatcher.Run();

// Dispatcher.CurrentDispatcher

// 控件的实例成员DIspatcher和Application.Current.Dispatcher之间的区别？