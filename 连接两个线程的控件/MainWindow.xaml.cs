using System;
using System.Collections.Generic;
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
using Walterlv.Demo;

namespace 连接两个线程的控件
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var _ = Host.SetChildAsync(() =>
            // {
            //     var box = new TextBox
            //     {
            //         Text = "吕毅 - walterlv",
            //         Margin = new Thickness(16),
            //     };
            //     return box;
            // });


            var dispatcher =  UIDispatcher.RunNew("walterlv's testing thread");

            // 使用这个后台线程的 Dispatcher 创建一个自己的用户控件。
            var control =  dispatcher.InvokeAsync(() =>
            {
                Button btn = new Button();
                btn.Height = 200;
                btn.Width = 200;
                btn.Background = Brushes.DarkSeaGreen;

                var binding = new Binding() { Path = new PropertyPath("Content"), Source = new MainViewModel()};
         
                
                btn.SetBinding(ContentProperty,binding);



                //btn.Content = "666666";
                return btn;
            });

             Host.SetChildAsync(control.GetAwaiter().GetResult());
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MainViewModel();
        }
    }
}
