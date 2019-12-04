using Core.BussinessLogic.Managers;
using Prism.Commands;
using Prism.Navigation;
using UI.ViewModels.AbstractVMBase;
using UI.Views;
using Xamarin.Forms;

namespace UI.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;
        private string _username;
        private string _password;
        private string _phoneNumber;
        private string _firstName;
        private string _lastName;

        public RegisterPageViewModel(INavigationService navigationService, IUserManager userManager) : base(navigationService)
        {
            Title = "Register";
            _userManager = userManager;
        }

        public string Username {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                UsernameWrittenCheck = string.IsNullOrWhiteSpace(value);
                RaisePropertyChanged(nameof(UsernameWrittenCheck));
            }
        }
        public string Password {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                PasswordWrittenCheck = string.IsNullOrWhiteSpace(value);
                RaisePropertyChanged(nameof(PasswordWrittenCheck));
            }
        }
        public string PhoneNumber {
            get => _phoneNumber;
            set
            {
                SetProperty(ref _phoneNumber, value);
                TelNumberWrittenCheck = string.IsNullOrWhiteSpace(value);
                RaisePropertyChanged(nameof(TelNumberWrittenCheck));
            }
        }
        public string FirstName {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                FirstNameWrittenCheck = string.IsNullOrWhiteSpace(value);
                RaisePropertyChanged(nameof(FirstNameWrittenCheck));
            }
        }
        public string LastName {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                LastNameWrittenCheck = string.IsNullOrWhiteSpace(value);
                RaisePropertyChanged(nameof(LastNameWrittenCheck));
            }
        }

        public bool UsernameWrittenCheck { get; set; }
        public bool PasswordWrittenCheck { get; set; }
        public bool FirstNameWrittenCheck { get; set; }
        public bool LastNameWrittenCheck { get; set; }
        public bool TelNumberWrittenCheck { get; set; }

        public DelegateCommand RegisterCommand => new DelegateCommand(async () =>
        {
            try
            {
                //Data filled check
                if (!UsernameWrittenCheck && !PasswordWrittenCheck && !FirstNameWrittenCheck && !LastNameWrittenCheck && !TelNumberWrittenCheck)
                {
                    await _userManager.RegisterToDatabase(Username, Password, FirstName, LastName, PhoneNumber);
                    await Application.Current.MainPage.DisplayAlert("Registration", "User registered successfully!", "OK");
                    await NavigationService.NavigateAsync(nameof(LoginPage));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields", "OK");
                    FirstNameWrittenCheck = string.IsNullOrWhiteSpace(FirstName);
                    LastNameWrittenCheck = string.IsNullOrWhiteSpace(LastName);
                    UsernameWrittenCheck = string.IsNullOrWhiteSpace(Username);
                    PasswordWrittenCheck = string.IsNullOrWhiteSpace(Password);
                    TelNumberWrittenCheck = string.IsNullOrWhiteSpace(PhoneNumber);
                    RaisePropertyChanged(nameof(UsernameWrittenCheck));
                    RaisePropertyChanged(nameof(FirstNameWrittenCheck));
                    RaisePropertyChanged(nameof(LastNameWrittenCheck));
                    RaisePropertyChanged(nameof(PasswordWrittenCheck));
                    RaisePropertyChanged(nameof(TelNumberWrittenCheck));
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User with this username already exists!", "Understand");
            }
        });
        /// <summary>
        /// Happens when redirected to this page
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            UsernameWrittenCheck = true;
            PasswordWrittenCheck = true;
            FirstNameWrittenCheck = true;
            LastNameWrittenCheck = true;
            TelNumberWrittenCheck = true;
            IsBusy = false;
        }
    }
}
