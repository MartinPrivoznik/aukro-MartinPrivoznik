using Prism.Navigation;
using UI.ViewModels.AbstractVMBase;

namespace UI.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        public MainTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
