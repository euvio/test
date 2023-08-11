using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 视图增删一行后台集合是否增删
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GetSourceCollection().AddRange(new List<Product>() { new Product() { Name = "1" }, new Product() { Name = "2" } });

        }

        private void SEEButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private ObservableCollectionEx<Product> GetSourceCollection()
        {
            return (listbox.ItemsSource as ObservableCollectionEx<Product>);
        }

        private ListCollectionView GetCollectionView()
        {
            return CollectionViewSource.GetDefaultView(listbox.ItemsSource) as ListCollectionView;
        }

        private void Button视图源集合仓库的元素指向堆中的同一个实例_OnClick(object sender, RoutedEventArgs e)
        {

            ObservableCollection<Product> observableProducts = new ObservableCollection<Product>(Repository.Instance);
            ListCollectionView lcv = CollectionViewSource.GetDefaultView(observableProducts) as ListCollectionView;

            observableProducts.Add(observableProducts[0]);

            /* CollectionView: lcv
             * ObservableCollection: observableProducts
             * Repository: Repository.Instance
             *
             * 分别遍历3个集合，输出的各个集合元素的GetHashCode(),会发现这些元素的
             *
             *
             *
             *
             * 结论：Product的GetHashCode没有重写，默认值是uuid，只要不是同一个实例，HashCode一定不同。HashCode相同，一定是同一个实例。
             * 所以，仓库集合元素的浅克隆到源集合，再浅克隆到视图，三者只是元素的数量不同，但值一定相同。
             */
            Console.WriteLine("视图");
            foreach (var VARIABLE in GetCollectionView())
            {
                Console.WriteLine(VARIABLE.GetHashCode());
            }

            Console.WriteLine("源集合");
            foreach (var product in listbox.ItemsSource as ObservableCollection<Product>)
            {
                Console.WriteLine(product.GetHashCode());
            }

            Console.WriteLine("仓库");
            foreach (var VARIABLE in Repository.Instance)
            {
                Console.WriteLine(VARIABLE.GetHashCode());
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }

        //public override bool Equals(object? obj)
        //{
        //    //return ReferenceEquals(obj, this);
        //    if (obj == null) return false;

        //    string name = obj.GetType().Name;
        //    //if (name == "NamedObject") return false;
        //    if (this.Name == (obj as Product).Name)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return Name.GetHashCode();
        //}
    }
}
