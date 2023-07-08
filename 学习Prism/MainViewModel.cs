using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Command = new DelegateCommand(() =>
            {
                ;
            },
                () =>
                {
                    var ssss = Random.Shared.NextInt64() % 2 == 0;

                    Console.WriteLine(ssss);
                    return ssss;

                }).ObservesProperty(() => S.MyProperty);


            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    S.OOOO("MyProperty");
                }
            });


        //DelegateCommand command1 = new DelegateCommand(Execute);
        //// command1 命令源始终处于可触发状态，随时可以执行命令

        //DelegateCommand command2 = new DelegateCommand(Execute, CanExecute);
        //// 影响CanExecute返回值的变量(因素)的值发生变化时，必须调用command2.RaiseCanExecuteChanged()以刷新命令源的启禁用状态。

        DelegateCommand command3 = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => Name)
          .ObservesProperty(() => Age);

        //// ObservesProperty()的作用就是当某个继承了INotifyPropertyChanged的对象发布了它的某个属性发生了变化时，立刻调用一次command3.RaiseCanExecuteChanged()以刷新命令源的启禁用状态。




        //DelegateCommand command4 = new DelegateCommand(Execute).ObservesCanExecute(() => IsEnable);

        //DelegateCommand command5 = new DelegateCommand(Execute).ObservesCanExecute(() => IsEnable).ObservesProperty(()=>Age);

        //     // command.RaiseCanExecuteChanged();

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
