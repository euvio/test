//public class MyComputer : IComputer
//{
//    public Task Do()
//    {
//        //return Task.Run(() => { Console.WriteLine("逻辑很简单执行很快的代码块，新建一个任务或线程得不偿失，反而损伤性能。"); });
//        return Task.Run<string>(() => "逻辑很简单执行很快的代码块，新建一个任务或线程得不偿失，反而损伤性能。");
//    }

//    public async Task<string> DoString()
//    {
//        //return Task.Run<string>(() => "逻辑很简单执行很快的代码块，新建一个任务或线程得不偿失，反而损伤性能。");
//        return await Task.Run<string>(() => "逻辑很简单执行很快的代码块，新建一个任务或线程得不偿失，反而损伤性能。");
//    }
//}

public interface IComputer
{
    Task Do();

    Task<string> DoString();
}

public class MyComputer : IComputer
{
    public Task Do()
    {
        // return Task.FromResult("Hello New Boy");
        return Task.CompletedTask;
    }

    public Task<string> DoString()
    {
        return Task.FromResult("Hello New Boy");
    }
}