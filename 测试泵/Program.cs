using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试泵
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent waiter = new AutoResetEvent(false);

            int flag = 0;

            SerialPort sp = new SerialPort();

            sp.PortName = "COM3";
            sp.BaudRate = 19200;
            sp.DataReceived += (sender, eventArgs) =>
            {
                var port = sender as SerialPort;
                if (Interlocked.CompareExchange(ref flag, 1, 0) == 0)
                {
                    while (true)
                    {
                        if (port.BytesToRead >= 95)
                        {
                            Console.WriteLine(port.BytesToRead);
                            port.ReadExisting();

                            waiter.Set();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("MISS");
                }
     
            };
            sp.Open();

            while (true)
            {
                waiter.Reset(); 
                sp.Write(new byte[] { 0x9E, 0x03, 0x08, 0x99, 0x00, 0x2D, 0x4A, 0x37 },0,8);
               sp.BaseStream.Flush();
                var timeout =  waiter.WaitOne(5000);
               if (!timeout)
               {
                   Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\" + sp.BytesToRead);
                   
               }
               Thread.Sleep(100);
            }
        }
    }
}
