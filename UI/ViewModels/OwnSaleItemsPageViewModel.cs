using Core.BussinessLogic.Managers;
using Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Factories;
using UI.ViewModels.AbstractVMBase;
using UI.Views;
using Xamarin.Forms;

namespace UI.ViewModels
{
    public class OwnSaleItemsPageViewModel : ViewModelBase
    {
        private readonly ISaleItemViewModelFactory _saleItemViewModelFactory;
        private readonly ISaleItemsManager _saleItemsManager;

        public OwnSaleItemsPageViewModel(INavigationService navigationService, ISaleItemViewModelFactory saleItemViewModelFactory, ISaleItemsManager saleItemsManager, IEventAggregator eventAggregator) : base(navigationService)
        {
            _saleItemViewModelFactory = saleItemViewModelFactory;
            _saleItemsManager = saleItemsManager;
            //Subscribes ItemSoldEvent 
            eventAggregator.GetEvent<ItemSoldEvent>().Subscribe(UpdateView);
        }

        public IEnumerable<SaleItemViewModel> OwnSaleItemList { get; set; }

        public DelegateCommand AddSaleItemCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync(nameof(AddOrModifySaleItem));
        });

        /// <summary>
        /// Happens when redirected to this tab
        /// </summary>
        public DelegateCommand AppearingCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;

            try
            {
                var saleItems = await _saleItemsManager.GetOwnSaleItems();
                OwnSaleItemList = saleItems.Select(saleItem => _saleItemViewModelFactory.CreateSaleItemViewModel(saleItem)).ToList().OrderByDescending(saleItem => saleItem.TimeAdded);
                RaisePropertyChanged(nameof(OwnSaleItemList));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            IsBusy = false;
        });


        private async void UpdateView()
        {
            var saleItems = await _saleItemsManager.GetOwnSaleItems();
            OwnSaleItemList = saleItems.Select(saleItem => _saleItemViewModelFactory.CreateSaleItemViewModel(saleItem)).ToList().OrderByDescending(saleItem => saleItem.TimeAdded);
            await Application.Current.MainPage.DisplayAlert("Sold!", "You have sold and item successfully!", "OK");
            RaisePropertyChanged(nameof(OwnSaleItemList));
        }
    }
}
