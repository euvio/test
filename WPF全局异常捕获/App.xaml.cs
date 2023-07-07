using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


/* AppDomain.UnhandledException会捕获所有Thread线程，Application.DispatcherUnhandledException只会捕获单线程模型的UI线程异常。
 * 普通的Thread线程的未捕获线程，直接抛到AppDomain.UnhandledException处理，单线程模型的UI线程的未捕获异常，先抛到Application.DispatcherUnhandledException
 * 处理，如果被标记成已处理(e.Handled=true),则到此为止，程序不会崩溃退出；如果未标记成已处理，下一步会接着抛到AppDomain.UnhandledException，最后程序崩溃退出。
 * 线程池线程未处理的异常，会被线程池吃掉，既不会抛到Application.DispatcherUnhandledException和AppDomain.UnhandledException，也不会导致程序崩溃退出。如果很在意线程池线程的异常，可以封送线程池异常到UI线程，借助UI线程抛出线程池线程。
 *
 *
 * Application和AppDomain都有Exit事件，先执行Application的Exit事件，再执行Appdomain的Exit事件。
 * 程序正常退出，会依次调用Application的Exit事件和Appdomain的Exit事件。
 * 如果是因为未捕获的异常导致程序退出，则不会调用任何Exit事件。
 * Appdomain的Exit事件不要再出现UI元素，如弹窗之类的，会导致异常。
 *
 */
namespace WPF全局异常捕获
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application app = sender as Application;

            app.DispatcherUnhandledException += App_DispatcherUnhandledException;
            app.Exit += App_Exit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Exception.ToString());

                if (e.Exception is ArgumentException)
                {
                    e.Handled = true; //把Handled属性设为true，表示此异常已处理，程序可以继续运行不会退出
                    return;
                }
                // 认为其他异常是致命的，让应用退出
                e.Handled = false;
                MessageBox.Show("UI线程发生致命错误,应用即将退出！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("UI线程发生致命错误,应用即将退出！");
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject);
            MessageBox.Show("发生致命错误");
        }

        private void CurrentDomainOnProcessExit(object? sender, EventArgs e)
        {
            // 不要在此函数中调用WPF元素，包括MessageBox，否则会异常。
            Console.WriteLine("AppDomain Exit");
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            MessageBox.Show("Application Exit");
            Application.Current.Dispatcher.UnhandledException +=
        }
    }
}

// 可以把UI线程的所有异常记录下来。程序运行一段时间后，看看有哪些异常，比如无伤大雅但经常疏忽的值转换器ValueConverter抛出的异常。