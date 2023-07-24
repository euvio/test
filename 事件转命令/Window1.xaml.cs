using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace 事件转命令
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new MyViewModel();

            int a = 1;
            object b = a;
            if (a.Equals(b))
            {
                ;
            }

        }

        private void Cmb_OnSelected(object sender, RoutedEventArgs e)
        {
            var a = sender as ComboBox;
            MessageBox.Show(a.SelectedIndex.ToString());
        }
    }

    class MyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = value as string;
            if (name == "A")
            {
                return 0;
            }

            if (name == "B")
                return 1;
            return 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = System.Convert.ToInt32(value);
            if (index == 0)
            {
                return "A";
            }

            if (index == 1)
            {
                return "B";

            }

            return "C";
        }
    }
}
