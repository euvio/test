using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace 加载界面_双UI线程
{
    public class Program
    {
        private static async Task<Dispatcher> GetNotNullDispatcher(Thread thread)
        {
            while (true)
            {
                var dispatcher = Dispatcher.FromThread(thread);
                if (dispatcher != null)
                {
                    return dispatcher;
                }

                await Task.Delay(100);
            }
        }

        private static async Task Main(string[] args)
        {
            加载界面.SplashScreen splashScreen = null;
            Thread thread = new Thread(() =>
            {
                splashScreen = new 加载界面.SplashScreen();
                splashScreen.BlockInfo.Text = "程序启动中";
                splashScreen.Show();
                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
            Dispatcher dispatcher = await GetNotNullDispatcher(thread); // System.Windows.Threading.Dispatcher.CurrentDispatcher;被调用才会为线程创建Dispatcher

            await Task.Delay(2000);

            dispatcher.Invoke(() =>
            {
                if (splashScreen != null)
                {
                    splashScreen.BlockInfo.Text = "读取命令行参数";
                }
            });

            await Task.Delay(2000);

            dispatcher.Invoke(() =>
            {
                splashScreen?.Close();
            });

            Thread mainThread = new Thread(() =>
            {
                Application app = new Application();
                MainWindow main = new MainWindow();
                main.Topmost = true;
                app.ShutdownMode = ShutdownMode.OnMainWindowClose;

                app.Run(main);
            });

            mainThread.SetApartmentState(ApartmentState.STA);
            mainThread.Start();

            mainThread.Join();
        }
    }
}