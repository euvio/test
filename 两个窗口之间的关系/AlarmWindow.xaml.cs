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
using System.Windows.Shapes;

namespace 两个窗口之间的关系
{
    /// <summary>
    /// Interaction logic for AlarmWindow.xaml
    /// </summary>
    public partial class AlarmWindow : Window
    {
        public AlarmWindow()
        {
            InitializeComponent();

            this.StateChanged += (sender, args) =>
            {
                Window w = sender as Window;
                if (w.WindowState == WindowState.Minimized)
                {
                    if (w.Owner != null)
                    {
                        w.Owner.WindowState = WindowState.Minimized;
                    }
                }
            };
        }
    }
}
