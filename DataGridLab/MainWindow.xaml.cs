using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataGridLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>()
            {
                new Product()
                {
                    UnitPrice = 300
                },
                new Product()
                {
                    UnitPrice = 200
                },
                new Product()
                {
                    UnitPrice = 100
                }
            };
            this.DataContext = Products;
        }

        public ObservableCollection<Product> Products { get; set; }

        /*  e.Column.SortDirection = ListSortDirection.Descending;
         *  这行代码并不能触发表格排序，它有2个作用：
         * 作用1：表头有上箭头，下箭头，无箭头 3种状态；分别对应ListSortDirectionAscending;ListSortDirection.Descending,Null.
         * 这行代码能够设置箭头的朝向，箭头的朝向和行升降序没有必然的关系，如果显示上箭头时需要行记录是升序的，需要编程保证。
         *
         * 程序刚启动时，这个值是null,第一次点击是升序还是降序，取决于代码
         *
         * 作用2：每当对视图排序后，应当更新图标，以保证下次点击是升序还是降序，同时应当抹除其他列的表头箭头，这样就能知道最近一次
         * 点击的表头是哪一个。当然，如果点击的表头是让多个列进行排序，可以都显示箭头，如 点击A列表头进行 A DESC, B ASC排序，那么可以让
         * A 箭头向下，B箭头向上，其他列不显示箭头。
         *
         */
        private void DataGrid_OnSorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true; // 中断路由事件的继续传播，这行必须要有，否则，原来内置的排序会覆盖此排序方法排序后的效果。
            DataGrid dg = sender as DataGrid;
            var view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;

            if (e.Column.DisplayIndex == 0) // 使用DisplayIndex来区分点击的是哪一列的表头
            {
                if (e.Column.SortDirection == ListSortDirection.Ascending ||
                    e.Column.SortDirection == null)
                {
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("UnitPrice", ListSortDirection.Descending));
                    e.Column.SortDirection = ListSortDirection.Descending;
                    view.Refresh();
                }
                else
                {
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("UnitPrice", ListSortDirection.Ascending));
                    e.Column.SortDirection = ListSortDirection.Ascending;
                    view.Refresh();
                }
            }

            foreach (var dgColumn in dg.Columns)
            {
                if (dgColumn.DisplayIndex != e.Column.DisplayIndex)
                {
                    dgColumn.SortDirection = null;
                }
            }
        }
    }

    public class Product
    {
        public decimal UnitPrice { get; set; }
    }
}