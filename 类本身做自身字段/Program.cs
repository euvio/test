namespace 类本身做自身字段
{
    internal class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
        }
    }


    class A
    {
        public List<A> P { get; set; }

        public A()
        {
            P = new List<A>();
        }
    }
}