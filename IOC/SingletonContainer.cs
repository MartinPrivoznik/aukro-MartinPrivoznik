using BussinessLogic.Managers;
using Core.BussinessLogic.Managers;
using Core.DataProviders.Repositories;
using Data.Database;
using DataProviders.Repositories;
using Prism.Ioc;
using UI.Factories;
using UI.ViewModels;
using UI.Views;

namespace IOC
{
    public static class SingletonContainer
    {
        /// <summary>
        /// IOC container Dryloc
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {

            //Data repositories
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<ISaleItemsRepository, SaleItemsRepository>();

            //Managers
            containerRegistry.RegisterSingleton<IUserManager, UserManager>();
            containerRegistry.RegisterSingleton<ISaleItemsManager, SaleItemsManager>();

            //Pages
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<OwnSaleItemsPage, OwnSaleItemsPageViewModel>();
            containerRegistry.RegisterForNavigation<PersonalSettingsPage, PersonalSettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddOrModifySaleItem, AddOrModifySaleItemViewModel>();
            containerRegistry.RegisterForNavigation<SaleItemDetailPage, SaleItemDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<IncreaseAuctionPricePage, IncreaseAuctionPricePageViewModel>();

            //Factories
            containerRegistry.Register<ISaleItemViewModelFactory, SaleItemViewModelFactory>();
        }
    }
}
