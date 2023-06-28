using Caliburn.Micro.Tutorial.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Caliburn.Micro.Tutorial.Wpf;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new SimpleContainer();

    public Bootstrapper()
    {
        Initialize();
    }

    protected override void Configure()
    {
        _container.Instance(_container);
        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<ShellViewModel>()
            .Singleton<CategoryViewModel>();

        //foreach (var assembly in SelectAssemblies())
        //{
        //    assembly.GetTypes()
        //        .Where(type => type.IsClass)
        //        .Where(type => type.Name.EndsWith("ViewModel"))
        //        .ToList()
        //        .ForEach(viewModelType => _container.RegisterPerRequest(
        //            viewModelType, viewModelType.ToString(), viewModelType));
        //}
    }

    protected override IEnumerable<Assembly> SelectAssemblies()
    {
        // https://www.jerriepelser.com/blog/split-views-and-viewmodels-in-caliburn-micro/

        var assemblies = base.SelectAssemblies().ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());
        return assemblies;
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        LogManager.GetLog = type => new DebugLog(type);

        await DisplayRootViewForAsync(typeof(ShellViewModel));
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
}