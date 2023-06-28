using System.Threading.Tasks;
using System.Windows;

namespace 加载界面
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var splashScreen = new SplashScreen();
            splashScreen.BlockInfo.Text = "程序启动中";
            splashScreen.Show();
            await Task.Delay(2000);

            splashScreen.BlockInfo.Text = "读取命令行参数";
            await Task.Delay(2000);

            splashScreen.BlockInfo.Text = "加载配置参数";
            await Task.Delay(2000);

            splashScreen.BlockInfo.Text = "检查运行环境";
            await Task.Delay(2000);

            splashScreen.Close();

            this.ShutdownMode = ShutdownMode.OnMainWindowClose;

            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}