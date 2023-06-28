using Newtonsoft.Json;
using System.Drawing;

namespace 测试代码工程
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
         
            string json = JsonConvert.SerializeObject(new Student(),Formatting.Indented);

            Console.WriteLine(json);
        }
    }

    [JsonObject(MemberSerialization.Fields)]
    public class Student
    {
        public Student()
        {
            Name = "Jack";
            Age = 20;
            Address = "广东珠海";
            Email = "123@qq.com";
            Height = 177;
            HairColor = Color.Aqua;
            weight = 63;
            className = "大二";
            teacher = "玛丽亚";
            girlFriend = "琳娜";
            schoolId = 97251;
            identificationId = 123456789;
        }

        #region Properties


        public string MMMMMMM
        {
            get => Name + Age + Email;
        }
        public string Name { get; set; }
        public int Age { get; set; }

        protected string Address { get; set; }
        protected string Email { get; set; }
        
        private int Height { get; set; }
     
        private Color HairColor { get; set; }

        #endregion Properties

        #region Fields
  
        public double weight;
        public string className;

        protected string teacher;
        protected string girlFriend;
 
        private int schoolId;
        private int identificationId;

        #endregion Fields
    }
}