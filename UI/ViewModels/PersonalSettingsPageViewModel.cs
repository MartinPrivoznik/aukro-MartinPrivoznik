using Core.BussinessLogic.Managers;
using Data.Database.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using UI.ViewModels.AbstractVMBase;
using UI.Views;

namespace UI.ViewModels
{
    public class PersonalSettingsPageViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;

        public PersonalSettingsPageViewModel(INavigationService navigationService, IUserManager userManager) : base(navigationService)
        {
            _userManager = userManager;
        }
        public BasicUser User { get; set; }
        public DelegateCommand LogoutCommand => new DelegateCommand(() =>
        {
            _userManager.LoggedUser = null;
            NavigationService.NavigateAsync(nameof(LoginPage));
        });

        /// <summary>
        /// Happens when redirected to this tab
        /// </summary>
        public DelegateCommand AppearingCommand => new DelegateCommand(() =>
        {
            IsBusy = true;

            try
            {
                User = _userManager.LoggedUser;
                RaisePropertyChanged(nameof(User));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            IsBusy = false;
        });
    }
}
