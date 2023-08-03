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
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();
            Products.Add(new Product() { Name = "牛奶" });
            Products.Add(new Product() { Name = "香蕉" });
            Products.Add(new Product() { Name = "可乐" });
            listbox.ItemsSource = Products;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //(CollectionViewSource.GetDefaultView(Products) as ListCollectionView).AddNewItem(new Product() { Name = "苹果" });
            //var target = new Product() { Name = "可乐" };
            //int index = ((CollectionViewSource.GetDefaultView(Products) as ListCollectionView)).IndexOf(target);
            //((CollectionViewSource.GetDefaultView(Products) as ListCollectionView)).RemoveAt(0);

            Product p = null;
            foreach (var VARIABLE in (CollectionViewSource.GetDefaultView(Products) as ListCollectionView))
            {
                if (VARIABLE.Equals(new Product() { Name = "牛奶" }))
                {
                    p = VARIABLE as Product;
                }
            }

            Products.Remove(p);
        }

        private void SEEButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            int a = Products.Count;
        }
    }

    public class Product
    {
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            //return ReferenceEquals(obj, this);
            if (obj == null) return false;

            string name = obj.GetType().Name;
            //if (name == "NamedObject") return false;
            if (this.Name == (obj as Product).Name)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }


    public class Repository
    {

    }
}
