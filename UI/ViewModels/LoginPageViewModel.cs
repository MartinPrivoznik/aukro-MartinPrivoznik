using Core.BussinessLogic.Managers;
using Prism.Commands;
using Prism.Navigation;
using UI.ViewModels.AbstractVMBase;
using UI.Views;
using Xamarin.Forms;

namespace UI.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        //private fields
        private readonly IUserManager _userManager;

        public LoginPageViewModel(INavigationService navigationService, IUserManager userManager)
            : base(navigationService)
        {
            Title = "Login Page";
            _userManager = userManager;
            _userManager.FetchAllUsers();
        }

        //public props
        public string Username { get; set; }
        public string Password { get; set; }

        //commands
        public DelegateCommand LoginCommand => new DelegateCommand(async () =>
        {
            var user = await _userManager.AuthenticateLogin(Username, Password);
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Nickname or password is incorrect", "Understand");
            }
            else
            {
                //Save logged user to RAM and navigate to tabbed page 
                _userManager.LoggedUser = user;
                await NavigationService.NavigateAsync($"{nameof(MainTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(MainPage)}");
            }
        });
        public DelegateCommand RegisterCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync(nameof(RegisterPage));
        });
        
    }
}
