using Core.BussinessLogic.Managers;
using Data.Database.Models;
using Data.Models.Constants;
using Prism.Commands;
using Prism.Navigation;
using System;
using UI.Factories;
using UI.ViewModels.AbstractVMBase;
using UI.Views;

namespace UI.ViewModels
{
    public class SaleItemDetailPageViewModel : ViewModelBase
    {
        private readonly ISaleItemsManager _saleItemsManager;
        private readonly ISaleItemViewModelFactory _saleItemViewModelFactory;
        private SaleItem _saleItem;

        public SaleItemDetailPageViewModel(INavigationService navigationService, ISaleItemsManager saleItemsManager, ISaleItemViewModelFactory saleItemViewModelFactory) : base(navigationService)
        {
            _saleItemsManager = saleItemsManager;
            _saleItemViewModelFactory = saleItemViewModelFactory;
        }

        public SaleItemViewModel SaleItem { get; set; }
        public bool CheckAnyAuctionWinner { get; set; }
        public DateTime AuctionEnding { get; set; }

        public DelegateCommand BuyNowCommand => new DelegateCommand(async () =>
        {
            //Some buy logic, now just removes from databse
            await _saleItemsManager.RemoveSaleItem(_saleItem);
            await NavigationService.NavigateAsync($"{nameof(MainTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(MainPage)}");
        });

        public DelegateCommand PlaceAuctionPriceCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync(nameof(IncreaseAuctionPricePage), new NavigationParameters { { Universe.SaleItemParameter, _saleItem } });
        });

        /// <summary>
        /// Happens when redirected to this page
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            // Get passed parameters
            if (parameters.TryGetValue(Universe.SaleItemParameter, out SaleItem saleItem))
            {
                try
                {
                    _saleItem = saleItem;
                    AuctionEnding = _saleItem.TimeAdded.AddDays(3);

                    //Null check
                    if (!string.IsNullOrEmpty(_saleItem.AuctionWinner))
                    {
                        CheckAnyAuctionWinner = true;
                        RaisePropertyChanged(nameof(CheckAnyAuctionWinner));
                    }

                    SaleItem = _saleItemViewModelFactory.CreateSaleItemViewModel(_saleItem);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            RaisePropertyChanged(nameof(AuctionEnding));
            RaisePropertyChanged(nameof(SaleItem));
            IsBusy = false;
        }
    }
}
