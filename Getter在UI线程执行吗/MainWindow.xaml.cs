using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Getter在UI线程执行吗
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person person = new Person();

        public MainWindow()
        {
            InitializeComponent();
            TextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name") { Source = person, IsAsync = true });
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            person.OnPropertyChanged("Name");
        }
    }

    class Person : INotifyPropertyChanged
    {
        public string Name
        {
            get
            {
                Thread.Sleep(200);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                return DateTime.Now.ToString();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
