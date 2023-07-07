using System;

namespace CaliburnSimpleContainerDemo
{
    public interface IVehicle
    {
        void Run();
    }

    internal class Car : IVehicle
    {
        private string _model;

        public Car(string model)
        {
            _model = model;
        }

        public void Run()
        {
            Console.WriteLine($"{GetHashCode()} + car + {_model}");
        }
    }

    internal class Bus : IVehicle
    {
        private string _model;

        public Bus(string model)
        {
            _model = model;
        }

        public void Run()
        {
            Console.WriteLine($"{GetHashCode()} + bus + {_model}");
        }

        public override string ToString()
        {
            return $"Model: {_model} \r\n" +
                   $"Hashcode: {GetHashCode()} \r\n" +
                   $"TypeName: {this.GetType().Name}";
        }
    }
}