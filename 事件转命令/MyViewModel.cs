using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 事件转命令
{
    public class MyViewModel
    {
        public List<string> Options { get; } = new List<string>()
        {
            "A", "B", "C"
        };

        public List<Student> Students { get; set; } = new List<Student>()
        {
            new Student() { Name = "B", Age = 29 }
        };

    }

    public class Student : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public int Age
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnCompleted(Action continuation)
        {

        }
    }
}
