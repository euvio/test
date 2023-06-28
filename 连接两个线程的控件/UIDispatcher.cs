using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Threading;

namespace Walterlv.Demo
{
    /// <summary>
    /// 包含扩展 <see cref="Dispatcher"/> 的一些方法。
    /// </summary>
    public static class UIDispatcher
    {
        /// <summary>
        /// 创建一个可以运行 <see cref="Dispatcher"/> 的后台 UI 线程，并返回这个线程的调度器 <see cref="Dispatcher"/>。
        /// </summary>
        /// <param name="name">线程的名称，如果不指定，将使用 “BackgroundUI”。</param>
        /// <returns>后台线程创建并启动的 <see cref="Dispatcher"/>。</returns>
        public static Dispatcher RunNew( string name = null)
        {
            var resetEvent = new AutoResetEvent(false);

            // 记录原线程关联的 Dispatcher，以便在意外时报告异常。
            var originDispatcher = Dispatcher.CurrentDispatcher;
            Exception innerException = null;
            Dispatcher dispatcher = null;

            // 创建后台线程。
            var thread = new Thread(() =>
            {
                try
                {
                    // 获取关联此后台线程的 Dispatcher。
                    dispatcher = Dispatcher.CurrentDispatcher;

                    // 设置此线程的 SynchronizationContext，以便此线程上 await 之后能够返回此线程。
                    SynchronizationContext.SetSynchronizationContext(
                        new DispatcherSynchronizationContext(dispatcher));

                    // 报告 Dispatcher 已创建完毕，使用 ResetEvent 同步等待 Dispatcher 创建的地方可以继续执行了。
                    resetEvent.Set();
                }
                catch (Exception ex)
                {
                    // 报告创建过程中发生的异常。
                    innerException = ex;
                    resetEvent.Set();
                }

                // 此线程的以下代码将脱离异步状态机的控制，需要自己处理异常。
                try
                {
                    // 启动 Dispatcher，开始此线程上消息的调度。
                    Dispatcher.Run();
                }
                catch (Exception ex)
                {
                    // 如果新的 Dispatcher 线程上出现了未处理的异常，则将其抛到原调用线程上。
                    originDispatcher.InvokeAsync(() => ExceptionDispatchInfo.Capture(ex).Throw());
                }
            })
            {
                Name = name ?? "BackgroundUI",
                IsBackground = true,
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            resetEvent.WaitOne();
            resetEvent.Dispose();
            resetEvent = null;
            if (innerException != null)
            {
                ExceptionDispatchInfo.Capture(innerException).Throw();
            }
            return dispatcher;
        }
    }
}