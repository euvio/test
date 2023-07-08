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

namespace 属性通知核心
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Root root { get; set; } = new Root()
        {
            A = new A()
            {
                B = new B()
                {
                    C = new C()
                    {
                        Number = 2022
                    }
                }
            }
        };

        public MainWindow()
        {
            InitializeComponent();

            var bind = new Binding()
            {
                Source = root,
                Path = new PropertyPath("A.B.C"),
            };
            TextBlock.SetBinding(TextBlock.TextProperty, bind);

            Task.Run(() =>
            {

                while (true)
                {
                    Thread.Sleep(4000);
                    

                    //root.OnPropertyChanged("A");
                    //root.A.OnPropertyChanged("B");
                    //root.A.B.OnPropertyChanged("C");

                    //root.A  = new A()
                    //    {
                    //        B = new B()
                    //        {
                    //            C = new C()
                    //            {
                    //                Number = DateTime.Now.Second
                    //            }
                    //        }
                    //};

                    //root.A.B.OnPropertyChanged("C");
                }
            });
        }
    }


    public class Root : INotifyPropertyChanged
    {
        public A A { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class A : INotifyPropertyChanged
    {
        public B B { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class B : INotifyPropertyChanged
    {
        public C C { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }

    public class C
    {
        public int Number { get; set; }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
