using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace 数据采集模型
{
    // 缺点：刷新界面频率太高，即使服务端数据未变，仍旧刷新客户端界面
    public class MainViewModel : INotifyPropertyChanged
    {
        private Timer _timer;

        public MainViewModel()
        {
            _timer = new Timer((state) =>
            {
                OnPropertyChanged(nameof(Value));
            }, null, 1000, 100);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Value
        {
            get
            {
                PollDataMockClient client = new PollDataMockClient();
                return client.ConvertReply(client.Send("running_time"), typeof(string)) as string;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // OK ,可以避免不必要的刷新，但是定时器
    public class MainViewModel2 : INotifyPropertyChanged
    {
        private Timer _timer;

        private string _value;

        public MainViewModel2()
        {
            _timer = new Timer((state) =>
            {
                PollDataMockClient client = new PollDataMockClient();
                var newValue = client.ConvertReply(client.Send("running_time"), typeof(string)) as string;
                Value = newValue;
            }, null, 1000, 1000);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // 语法糖
    public class MainViewModel3 : INotifyPropertyChanged
    {
        private Timer _timer;

        public MainViewModel3()
        {
            _timer = new Timer((state) =>
            {
                Type type = this.GetType();
                var propertyinfos = type.GetProperties();
                foreach (var propertyinfo in propertyinfos)
                {
                    var oldValue = propertyinfo.GetValue(this);
                    PollDataMockClient client = new PollDataMockClient();
                    var newValue = client.ConvertReply(client.Send("running_time"), typeof(string)) as string;
                    if (oldValue != null && !oldValue.Equals(newValue))
                    {
                        propertyinfo.SetValue(this, newValue);
                        OnPropertyChanged(propertyinfo.Name);
                    }
                }
            }, null, 1000, 1000);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [Description("running_time")]
        public string Value { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainViewModel4 : INotifyPropertyChanged
    {
        private string _value;

        public MainViewModel4(Service service)
        {
            service.ValueChanged += (sender, args) =>
            {
                this.Value = service.Value;
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Service
        {
            private Timer _timer;

            public Service()
            {
                _timer = new Timer((state) =>
                {
                    PollDataMockClient client = new PollDataMockClient();
                    var newValue = client.ConvertReply(client.Send("running_time"), typeof(string)) as string;
                    if (newValue != Value)
                    {
                        Value = newValue;
                        ValueChanged?.Invoke(this, EventArgs.Empty);
                    }
                }, null, 1000, 1000);
            }

            public event EventHandler ValueChanged;

            public string Value { get; set; }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    /// <summary>
    /// 发送请求协议接收响应，模拟数据轮询客户端。
    /// 协议格式：请求 -> 请求数据项。 响应 -> 请求数据项的值(JSON格式)
    /// </summary>
    public class PollDataMockClient
    {
        public object ConvertReply(string jsonReply, Type target) => (JObject.Parse(jsonReply)["Value"])?.ToObject(target);

        public string Send(string key)
        {
            return JsonConvert.SerializeObject(new
            {
                key = key,
                Value = DateTime.Now
            });
        }
    }
}