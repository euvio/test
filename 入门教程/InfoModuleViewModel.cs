using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace 入门教程
{
    internal class InfoModuleViewModel:Screen,IModule
    {
        public InfoModuleViewModel()
        {
            DisplayName = nameof(InfoModuleViewModel);
        }
    }
}
