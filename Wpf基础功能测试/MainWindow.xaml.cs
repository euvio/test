using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Wpf基础功能测试
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataSource dataSource = new DataSource();

        public MainWindow()
        {
            InitializeComponent();
            Button.SetBinding(Button.ContentProperty, new Binding()
            {
                Source = dataSource,
                Path = new PropertyPath(nameof(Name)),
                Converter = new MyConverter()
            });
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                dataSource.Name = "JK";
            });
        }
    }

    internal class DataSource : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public DataSource()
        {
            PropertyChanged += (sender, args) =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            };
        }

        public event PropertyChangingEventHandler? PropertyChanging;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    internal class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            s += DateTime.UtcNow.ToString();
            Console.WriteLine($"转换器的线程ID是：{Thread.CurrentThread.ManagedThreadId}");
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}