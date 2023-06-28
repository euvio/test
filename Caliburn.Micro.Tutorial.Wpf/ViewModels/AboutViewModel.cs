using Caliburn.Micro.Tutorial.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.Tutorial.Wpf.ViewModels
{
    public class AboutViewModel :Screen
    {
        private AboutModel _aboutData;

        public AboutModel AboutData
        {
            get { return _aboutData; }
        }

        public Task CloseForm()
        {
            return TryCloseAsync();
        }

        protected override void OnViewLoaded(object view)
        {
            _aboutData = new AboutModel();
        }
    }
}
