public class Example
{
    public static void Main()
    {
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


        try
        {
            throw new Exception("1");

        }
        catch (Exception e)
        {
            Console.WriteLine("Catch clause caught : {0} \n", e.Message);
        }

        //Thread th = new Thread(() =>
        //{
        //    throw new Exception("2");
        //});

        //th.IsBackground = true;

        //th.Start();

        Task.Run(() =>
        {
            throw new Exception("3");
        });

        List<char> arr = new List<char>();
        while (true)
        {
            char[] array = new char[100000];
            arr.AddRange(array);
            GC.Collect();
        }

        Console.Read();
    }

    private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Console.WriteLine("llllll" + e.Exception + e.Observed.ToString());
    }

    private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
    {
        Exception e = (Exception)args.ExceptionObject;
        Console.WriteLine("MyHandler caught : " + e.Message);
        Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
    }
}

// The example displays the following output:
//       Catch clause caught : 1
//
//       MyHandler caught : 2
//       Runtime terminating: True
//
//       Unhandled Exception: System.Exception: 2
//          at Example.Main()