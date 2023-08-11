using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 异步方法
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.Read();
        }

        public static async void Test()
        {
            await AsyncMethodWillThrowException();
            throw new Exception();
        }

        public static Task AsyncMethodWillThrowException()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                throw new Exception();
            });
        }
    }


}
