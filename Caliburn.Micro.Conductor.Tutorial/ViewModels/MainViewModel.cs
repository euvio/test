using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.Conductor.Tutorial.ViewModels
{
    public class MainViewModel : Conductor<IModule>.Collection.OneActive
    {
        public MainViewModel(IEnumerable<IModule> modules)
        {
            this.Items.AddRange(modules);
        }

        protected override void OnViewLoaded(object view)
        {
            var _ = this.ActivateItemAsync(this.Items[0]);
        }
    }
}
