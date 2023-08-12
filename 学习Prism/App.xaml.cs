using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using Example;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace 学习Prism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ContainerLocator.SetContainerExtension(() => new DryIocContainerExtension());
            ContainerLocator.Current.Register<IDialogService, DialogService>();
            ContainerLocator.Current.Register<IEventAggregator, EventAggregator>();
    
            IContainerExtension<IContainer> a = new DryIocContainerExtension();
        }
    }
}
