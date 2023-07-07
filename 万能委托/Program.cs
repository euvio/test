namespace 万能委托
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Delegate d = new Func<int, int, int>((a, b) =>
            {
                return a + b;
            });

            var ret = d.DynamicInvoke(1, 2);
            Console.WriteLine(ret);
        }
    }
}