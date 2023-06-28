using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.Conductor.Tutorial.ViewModels
{
   public class CModuleViewModel:Screen,IModule
    {
        public CModuleViewModel()
        {
                
        }

        public override string DisplayName { get; set; } = "C";
    }
}
