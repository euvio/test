using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Prism.Commands;
using Prism.Events;

namespace 学习Prism
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MyViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MyViewModel();
            this.DataContext = ViewModel;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(e.AddedItems[0].GetType().Name);
            MessageBox.Show(ReferenceEquals(e.AddedItems[0], ViewModel.Students[0]).ToString());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dd1 = ListBox.ItemContainerGenerator.ContainerFromItem(ListBox.Items[0]);
            var dd2 = ListBox.ItemContainerGenerator.ContainerFromItem(ListBox.Items[1]);


            var uuuu = ReferenceEquals(dd1, dd2);
        }
    }

    public class MyViewModel
    {
        public MyViewModel()
        {
            StudentsView = new ListCollectionView(Students);

            var target = StudentsView.GetItemAt(0);

            MessageBox.Show(ReferenceEquals(target, Students[0]).ToString());
        }

        public ListCollectionView StudentsView { get; }

        public List<Student> Students { get; set; } = new List<Student>()
        {

        };
    }


    public class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
