using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Ioc;

namespace 学习Prism
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Singleton S { get; set; } = new Singleton();

        public event PropertyChangedEventHandler? PropertyChanged;


        public DelegateCommand Command { get; }


        public MainViewModel()
        {
            var a = ContainerLocator.Container.Resolve<IEventAggregator>();

            var b = ContainerLocator.Container.Resolve<IContainerProvider>();

            if (b == null)
            {
                ;
            }
        }

        private bool CanExecute()
        {
            throw new NotImplementedException();
        }

        private void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class Singleton:INotifyPropertyChanged
    {
        private int myVar;

        public int MyProperty
        {
            get => myVar;
            set
            {
                myVar = value;

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OOOO(string s)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(s));
        }
    }
}
