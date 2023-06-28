using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 定时器
{
    internal class Program
    {
        public static Timer timer;
        static void Main(string[] args)
        {
            bool status = true;
            Timer timer = new Timer(new TimerCallback((state) =>
            {
                Console.WriteLine(state);
            }), status, 100, 100);
            Thread.Sleep(1000);



            using (ManualResetEvent ResetEvent = new ManualResetEvent(false))
            {
                bool ret = timer.Dispose(ResetEvent);
                if (ret)
                {
                    bool timeout = ResetEvent.WaitOne(2000);
                    if (timeout)
                    {
                        throw new Exception("等待线程池上所有的定时任务结束超时。");
                    }
                }
                
            }

            
           
            Console.WriteLine("定时器启动的所有线程都已经执行完毕，开始执行后续代码...");

            
        }


    }
}
