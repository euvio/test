using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncRootDemo
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    List<int> list = new List<int>();
        //    list.Add(1);
        //    list.Add(2);
        //    list.Add(3);

        //    Console.WriteLine((list as ICollection).IsSynchronized);
        //    Task.Run(() =>
        //    {
        //        lock ((list as ICollection).SyncRoot)
        //        {
        //            for (int i = 0; i < 1000; i++)
        //            {
        //                Thread.Sleep(1000);
        //                Console.WriteLine((list as ICollection).IsSynchronized);
        //            }
        //        }
        //    });

        //    Thread.Sleep(2000);

        //    //lock ((list as ICollection).SyncRoot)
        //    //{
        //    list.Add(4);
        //    //}




        //    Console.Read();

        //    foreach (var i in list)
        //    {
        //        Console.WriteLine(i);
        //    }

        //    Console.Read();
        //    Console.Read();
        //}
    }
}
