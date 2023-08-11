using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
using CodingNinja.Wpf.ObjectModel;

namespace WpfRangeObservableCollectionXLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WpfObservableRangeCollection<Product> ObservableProducts = new WpfObservableRangeCollection<Product>()
        {
            new Product(){Name = "11"},
            new Product(){Name = "22"},
            new Product(){Name = "33"},
        };

        public MainWindow()
        {
            InitializeComponent();

            ListBox.ItemsSource = ObservableProducts;

            ObservableProducts.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {

                    var items = args.NewItems;
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    ;
                }
                else
                {
                    ;
                }
            };
        }

        private void ButtonClick_OnClick(object sender, RoutedEventArgs e)
        {


            ObservableProducts[0].Name = "OK";

            //Stopwatch sw2 = Stopwatch.StartNew();
            //for (int i = 0; i < 100; i++)
            //{
            //    ObservableProducts.AddRange(new List<Product>()
            //    {
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //        new Product(){Name = "1"},
            //        new Product(){Name = "2"},
            //        new Product(){Name = "3"},
            //    });
            //}
            //sw2.Stop();
            //Console.WriteLine(sw2.ElapsedMilliseconds);


            //ObservableProducts.Clear();

            //Stopwatch sw = Stopwatch.StartNew();
            //for (int i = 0; i < 100; i++)
            //{
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //    ObservableProducts.Add(new Product() { Name = "1" });
            //    ObservableProducts.Add(new Product() { Name = "2" });
            //    ObservableProducts.Add(new Product() { Name = "3" });
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            //ObservableProducts.Clear();
        }
    }

    public class Product
    {
        public string Name { get; set; }
    }
}
