using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Json.Net.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //JsonConvert.DeserializeObject<List<IVehicle>>("xxxxxx");
            //JsonConvert.DeserializeObject<Worker>("xxxxxx");

            ////File.WriteAllText("D:\\jjjjjj.json", JsonConvert.SerializeObject("abc\u000Dedf😀\ud83d\ude01\u0002gh."),Encoding.UTF8);

            File.WriteAllText("D:\\jjjjjj.json", JsonConvert.SerializeObject("\uD111"), Encoding.UTF8);
        }
    }


    class Worker
    {
        public IVehicle Vehicle { get; set; }
    }

    class MyClass
    {
        public IHuman Human { get; set; }
    }


    interface IVehicle
    {
        
    }




    interface IHuman
    {
        string Name { get; }
    }

    class Student : IHuman
    {
        public string Name { get; set; }
        public int SchoolId { get; set; }
    }

    class Pupil : Student
    {


    }

    class Doctor : Pupil
    {

    }
}