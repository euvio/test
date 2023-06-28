using Caliburn.Micro;
using System;
using System.Threading;
using System.Windows;

namespace IResultDemo
{
    public class Loader : IResult
    {
        private readonly string message;

        public Loader(string message)
        {
            this.message = message;
        }
        
        public void Execute(CoroutineExecutionContext context)
        {
            //var view = context.View as FrameworkElement;
            //while (view != null)
            //{
            //var busyIndicator = view as BusyIndicator;
                //if (busyIndicator != null)
                //{
                //    if (!string.IsNullOrEmpty(message))
                //        busyIndicator.BusyContent = message;
                //    busyIndicator.IsBusy = !hide;
                //    break;
                //}
            //    view = view.Parent as FrameworkElement;
            //}

            Thread.Sleep(5000);

            MessageBox.Show("AAAAAAAAAAAA");

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}