using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 能够暂停的计时器
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class MyClass
    {
        private Timer _timer;

        public MyClass()
        {

        }

        public void Start()
        {
            _timer = new Timer((state) =>
            {
                _timer.Change(1000, Timeout.Infinite);
            }, null, 1000, Timeout.Infinite);
        }

        public void Pause()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            using (ManualResetEvent waitHandle = new ManualResetEvent(false))
            {
                if (_timer.Dispose(waitHandle) && !waitHandle.WaitOne(1000))
                {
                    throw new TimeoutException("wait all timer job ended timeout.");
                }
            }
        }
    }
}
