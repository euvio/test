using System.Collections;
using System.IO.Ports;

namespace 串口测试
{
    internal class Program
    {
        private static int sum = 0;

        private static void Main(string[] args)
        {
            //  单帧发送字节数不要超出写缓存大小，

            // 读写缓存大小，若不够大则等待。

            // 协议的最大自己数数量。

            //  如果缓冲区已满，Write仍旧会全覆盖。
            //  接收缓冲区满了，仍旧有大量数据过来，接收缓存区会被覆盖。对于扫码枪和GPS模块，要把接收缓冲区和读取速度加快。

            SerialPort sp = new SerialPort();
            sp.PortName = "COM1";
            List<byte> list1 = new List<byte>();

            for (int i = 0; i < 20000; i++)
            {
                list1.AddRange((Enumerable.Range(0, 255)).Select(x => (byte)65));
         
            }

            List<byte> list2 = new List<byte>();
            for (int i = 0; i < 20000; i++)
            {
                list2.AddRange((Enumerable.Range(0, 255)).Select(x => (byte)66));
            }

            List<byte> list3 = new List<byte>();
            for (int i = 0; i < 20000; i++)
            {
                list3.AddRange((Enumerable.Range(0, 255)).Select(x => (byte)67));
            }

            sp.WriteBufferSize = 9;
            sp.Open();


            //sp.Write(list.ToArray(),0,list.Count);

            List<Task> tasks = new List<Task>();

           tasks.Add(Task.Run(() =>
           {
               sp.Write(list1.ToArray(), 0, list1.Count);
           }));

           tasks.Add(Task.Run(() =>
           {
               sp.Write(list2.ToArray(), 0, list2.Count);
           }));

           tasks.Add(Task.Run(() =>
           {
               sp.Write(list3.ToArray(), 0, list3.Count);
           }));

    

            //while (true)
            //{
            //    Console.WriteLine(sp.BytesToWrite);
            //}

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("全部发送完毕...");
            Console.Read();



            sp.WriteLine("Hello World...");

            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();


            Console.Read();







            #region 测试默认属性的值


            //SerialPort serialPort = new SerialPort();
            //serialPort.PortName = "COM1";
            //serialPort.Open();
            //var properties = serialPort.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    if (!property.Name.Contains("tream"))
            //        Console.WriteLine($"{property.Name} : {property.GetValue(serialPort)}");
            //}

            #endregion 测试默认属性的值

            #region 接收缓存溢出

            //SerialPort port = new SerialPort();

            //port.PortName = "COM1";
            ////port.Handshake = Handshake.RequestToSend;
            ////port.WriteTimeout = 10000;
            //port.Open();
            //Thread.Sleep(3000);

            //for (int i = 0; i < 2048; ++i)
            //{
            //    //port.WriteBufferSize = 1028;
            //    //port.WriteTimeout = 2;
            //    port.Write("ABCDEFGHIJKLMN");
            //}

            //for (int i = 0; i < 1024; ++i)
            //{
            //    port.Write("B");
            //}

            

            //Console.Read();

            //var properties = port.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    if (!property.Name.Contains("tream"))
            //        Console.WriteLine($"{property.Name} : {property.GetValue(port)}");
            //}

            #endregion 接收缓存溢出

            //SerialPort port = new SerialPort();
            //port.PortName = "COM1";
            //port.BaudRate = 9600;
            //port.Parity = Parity.None;
            //port.StopBits = StopBits.None;
            //port.ReceivedBytesThreshold = 10;
            //port.DataReceived += Port_DataReceived;

            //port.Open();

            //byte[] buffer = new byte[1024];
            //port.ReadTimeout = 10000;
            //port.ReadByte();
            //port.Write(buffer, 0, buffer.Length);

            Console.ReadKey();
        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("sum : " + Interlocked.Increment(ref sum));
            Console.WriteLine($"thread id : {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}