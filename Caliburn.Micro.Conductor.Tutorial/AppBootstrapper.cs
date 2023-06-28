using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro.Conductor.Tutorial.ViewModels;

namespace Caliburn.Micro.Conductor.Tutorial
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
            _container.RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            _container.RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));

            _container.RegisterPerRequest(typeof(IModule),null,typeof(AModuleViewModel));
            _container.RegisterPerRequest(typeof(IModule),null,typeof(BModuleViewModel));
            _container.RegisterPerRequest(typeof(IModule),null,typeof(CModuleViewModel));

            _container.RegisterSingleton(typeof(MainViewModel),null,typeof(MainViewModel));

            base.Configure();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<MainViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }
    }
}
