using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace 可通知的集合
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollectionEx<Product> ObservationProducts { get; } = new ObservableCollectionEx<Product>()
        {
            new Product(){Name = "1"}
        };

        public MainWindow()
        {
            InitializeComponent();
            listBox.ItemsSource = ObservationProducts;
            ObservationProducts.CollectionChanged += ObservationProducts_CollectionChanged;
        }

        private void ObservationProducts_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems[0] == ObservationProducts[e.NewStartingIndex])
                {
                    MessageBox.Show(ObservationProducts[e.NewStartingIndex].Name);
                }
            }
        }

        public class Product : INotifyPropertyChanged
        {
            private string name;

            public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); } }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObservationProducts[0].Name = "277777777777";
        }
    }
}
