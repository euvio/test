using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro.Tutorial.Wpf.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private readonly WindowManager _windowManager;

        public ShellViewModel(WindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public Task EditCategories()
        {
            var viewmodel = IoC.Get<CategoryViewModel>();
            return ActivateItemAsync(viewmodel, new CancellationToken());
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await EditCategories();
        }

        public Task About()
        {
            return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
        }
    }




}
