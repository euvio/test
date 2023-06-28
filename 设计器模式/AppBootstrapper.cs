using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace 设计器模式
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public AppBootstrapper()
        {
            _container = new SimpleContainer();
            Initialize();
        }

        protected override void Configure()
        {
            _container.RegisterInstance(typeof(SimpleContainer), null, _container);
            _container.RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            _container.RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            _container.RegisterSingleton(typeof(MainViewModel), null, typeof(MainViewModel));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);
        }
    }
}
