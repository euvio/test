using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Commands;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Primitives;
using MessageBox = System.Windows.MessageBox;

namespace 日期控件WPF.Toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            CheckComboBox.Text = "查看日志";
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //CheckComboBox.IsDropDownOpen = false;
            //string stext = CheckComboBox.SelectedValue;
            //string text = CheckComboBox.Text;
            //CheckComboBox.Text = "请选择";
            //CheckComboBox.IsDropDownOpen = true;
            CheckComboBox.MaxDropDownHeight = 50;
            var s = CheckComboBox.SelectedItems;
            var t = CheckComboBox.SelectedItemsOverride;
            CheckComboBox.Text = "查看日志";
        }



        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Enumerable.Range(1, 200).Select(elem => "COM" + elem.ToString());


        }

        private void CheckComboBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            CheckComboBox.IsDropDownOpen = true;
        }

        private void CheckComboBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            CheckComboBox.Text = "日志清单";
        }

        private void Mouse_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;
        }

        private void Mouse_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;
        }

        private void Mouse_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;
        }

        private void Mouse_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;
        }

        private void CheckComboBox_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void C1heckComboBox_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        private void CheckComboBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;
        }

        private void C2heckComboBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() != typeof(CheckComboBox))
                e.Handled = true;

        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public decimal UnitCost { get; set; }
    }
}
