using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 连接两个线程的控件
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _content;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {

            _content = "1111111111";


            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Content = "2222222222";
                Thread.Sleep(2000);
                Content = "333333333333333333333";
                Thread.Sleep(2000);
                Content = "55555555555555555";
            });
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Content)));
            }
        }
    }
}
