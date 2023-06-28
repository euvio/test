using System.Diagnostics;

namespace 方法的调用堆栈
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Test);
      
            Console.ReadLine();
        }

        private static void Test()
        {
            var result = Sum(1, 2);
        }

        private static int Sum(int num1, int num2)
        {
            var stacktrace = new StackTrace();
            for (var i = 0; i < stacktrace.FrameCount; i++)
            {
                var method = stacktrace.GetFrame(i).GetMethod();
                Console.WriteLine(method.Name);
            }
            return num1 + num2;
        }
    }
}