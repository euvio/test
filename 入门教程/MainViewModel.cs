using Caliburn.Micro;
using CommunityToolkit.Mvvm.Messaging;

namespace 入门教程
{
    public class MainViewModel : Conductor<IModule>.Collection.OneActive
    {
        public MainViewModel()
        {
            
            

            EnsureItem(new InfoModuleViewModel());

            ActivateItemAsync((new InfoModuleViewModel())).GetAwaiter().GetResult();
        }
    }
}