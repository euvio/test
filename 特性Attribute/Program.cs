using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace 特性Attribute
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyClass2 myclass2 = new MyClass2();
            var info = typeof(MyClass2).GetMethod("Method");
           var attributes = Attribute.GetCustomAttributes(info, typeof(MyAttribute),true);
           ;
           Console.Read();
        }
    }

    
    class MyClass1
    {
        [MyAttribute]
        public virtual void Method(){}
    }

    class MyClass2 : MyClass1
    {
        //[MyAttribute]
        public void Method() { }
    }


    [AttributeUsage(AttributeTargets.All,Inherited = true,AllowMultiple = true)]
    class MyAttribute : Attribute
    {
        public MyAttribute()
        {
            
        }
    }
}