using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace 典型的WPF启动模板
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex;

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // 程序关闭时执行
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            /******************** 防止程序多开 **************************/
            mutex = new Mutex(true, "li6v's application", out bool ret);
            if (!ret)
            {
                MessageBox.Show("已有一个程序实例运行");
                this.Shutdown();
                return;
            }
            /******************** 防止程序多开 **************************/


            /* ============================= 注册全局异常捕获 ===================================*/
            // 在UI线程上执行的代码抛出的异常，会被Application.Current.DispatcherUnhandledException捕获
            // 在Thread开启的线程上执行的代码抛出的异常会被AppDomain.CurrentDomain.UnhandledException捕获
            // Task抛出异常会被忽略，二者都无法捕获，可以将Task的异常封送到UI线程，来报告异常
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            /* ============================= 注册全局异常捕获 ===================================*/

            // 必须设置，否则App会把等待界面作为主窗体，关闭等待界面后，应用程序直接退出
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // 展示友好的等待界面
            Window waiting = new Window() { Title = "等待初始化界面" };
            waiting.Show();

            if (e.Args.Length > 0)
            {
                // 读取命令行参数
            }
            // 加载配置文件
            if (File.Exists("C:\\config.json"))
            {
                await Task.Delay(1000);
                File.ReadLines("C:\\config.json");
            }
            // 模拟耗时的其他初始化操作，不可以用Thread.Sleep(5000)，因为Startup的代码是在UI线程上执行的，会阻塞UI，导致等待界面卡死，无法被拖动。
            await Task.Delay(5000);
            // 初始化完成，关闭等待界面
            waiting.Close();

            if(Random.Shared.Next(1,10) % 2 == 0)
            {
                // 初始化成功
                this.ShutdownMode = ShutdownMode.OnMainWindowClose; // 主窗体关闭，程序自动退出
                Window main = new Window() { Title = "主界面" };
                this.MainWindow = main;
                main.Show(); // 不能是ShowDialog，否则Startup一直不返回
            }
            else // 初始化失败
            {
                MessageBox.Show("初始化失败，展示失败原因，即将关闭程序");
                this.Shutdown();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.IsTerminating); // 如果此异常会导致CLR退出，是True
            Console.WriteLine((e.ExceptionObject as Exception));
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true; // 设置成true，告诉Main函数此异常被忽略，不要关闭程序。如果是false，程序会自动关闭。
            Console.WriteLine($"UI线程出现{e.Exception}，但被忽略程序继续执行。");
        }
    }
}