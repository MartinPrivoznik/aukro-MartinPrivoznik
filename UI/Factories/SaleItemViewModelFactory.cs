using Core.BussinessLogic.Managers;
using Data.Database.Models;
using Prism.Events;
using Prism.Navigation;
using System;
using UI.ViewModels;

namespace UI.Factories
{
    public class SaleItemViewModelFactory : ISaleItemViewModelFactory
    {
        /// <summary>
        /// Front end factory creating Lists of viewmodels
        /// </summary>

        private readonly INavigationService _navigationService;
        private readonly IUserManager _userManager;
        private readonly ISaleItemsManager _saleItemsManager;
        private readonly IEventAggregator _eventAggregator;

        public SaleItemViewModelFactory(INavigationService navigationService, IUserManager userManager, ISaleItemsManager saleItemsManager, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _userManager = userManager;
            _saleItemsManager = saleItemsManager;
            _eventAggregator = eventAggregator;
        }
        public SaleItemViewModel CreateSaleItemViewModel(SaleItem item)
        {
            return new SaleItemViewModel(_navigationService, item, _userManager, _saleItemsManager, _eventAggregator);
        }
    }
}
