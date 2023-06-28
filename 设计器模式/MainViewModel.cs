using Caliburn.Micro;
using System.ComponentModel;
using System.Windows;

namespace 设计器模式
{
    internal class MainViewModel : Screen
    {
        private readonly IWindowManager _WindowManager;

        private bool IsInDesignMode
        {
            get => (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
        }

        public MainViewModel(IWindowManager windowManager)
        {
            _WindowManager = windowManager;
        }

        public MainViewModel()
        {
            _WindowManager = IoC.Get<IWindowManager>();
        }

        protected override void OnViewLoaded(object view)
        {
            if (IsInDesignMode)
            {
                Info = "Designed Mode";
            }
            else
            {
                Info = "Running Time";
            }
            
            NotifyOfPropertyChange(nameof(Info));
            base.OnViewLoaded(view);
        }

        public string Info { get; set; }
    }
}