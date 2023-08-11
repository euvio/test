using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Guid = System.Guid;

namespace 界面更新后台的原理
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int count = 0;
        private string _name = "Hi";

        public string Age
        {
            get
            {
                Console.WriteLine("age  get.........");
                return DateTime.Now.ToLongTimeString();
            }
        }



        public string Name
        {
            get
            {
                Interlocked.Increment(ref count);
                if (count % 2 != 0)
                {
                    Thread.Sleep(2000);
                }
                Console.WriteLine(count);
                return Guid.NewGuid().ToString().Substring(0, 5);
            }
            set
            {
                _name = value;
                Thread.Sleep(200);
                OnPropertyChanged(nameof(Name));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
