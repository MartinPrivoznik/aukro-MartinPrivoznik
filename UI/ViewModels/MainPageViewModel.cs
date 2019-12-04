using Core.BussinessLogic.Managers;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Factories;
using UI.ViewModels.AbstractVMBase;
using UI.Views;

namespace UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ISaleItemViewModelFactory _saleItemViewModelFactory;
        private readonly ISaleItemsManager _saleItemsManager;

        public MainPageViewModel(INavigationService navigationService, ISaleItemViewModelFactory saleItemViewModelFactory, ISaleItemsManager saleItemsManager) : base(navigationService)
        {
            _saleItemViewModelFactory = saleItemViewModelFactory;
            _saleItemsManager = saleItemsManager;
        }
        
        public IEnumerable<SaleItemViewModel> SaleItemList { get; set; }

        public DelegateCommand AddSaleItemCommand => new DelegateCommand(() =>
        {
            NavigationService.NavigateAsync(nameof(AddOrModifySaleItem));
        });

        /// <summary>
        /// Happens when redirected to this tab
        /// </summary>
        public DelegateCommand AppearingCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;

            try
            {
                var saleItems = await _saleItemsManager.GetNotOwnSaleItems();
                SaleItemList = saleItems.Select(saleItem => _saleItemViewModelFactory.CreateSaleItemViewModel(saleItem)).ToList().OrderByDescending(saleItem => saleItem.TimeAdded);
                RaisePropertyChanged(nameof(SaleItemList));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            IsBusy = false;
        });
    }
}
