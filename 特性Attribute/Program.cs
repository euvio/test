using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;

namespace 特性Attribute
{
    internal class Program
    {

        private void GetSomething()
        {

        }

        static void Main(string[] args)
        {
            var a = typeof(Program).GetMethods(BindingFlags.NonPublic);
          
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