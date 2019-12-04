using Core.BussinessLogic.Managers;
using Data.Database.Models;
using Data.Models.Constants;
using Prism.Commands;
using Prism.Navigation;
using System;
using UI.ViewModels.AbstractVMBase;
using UI.Views;
using Xamarin.Forms;

namespace UI.ViewModels
{
    public class IncreaseAuctionPricePageViewModel : ViewModelBase
    {
        private readonly ISaleItemsManager _saleItemsManager;
        private readonly IUserManager _userManager;
        private SaleItem _saleItem;

        public IncreaseAuctionPricePageViewModel(INavigationService navigationService, ISaleItemsManager saleItemsManager, IUserManager userManager): base(navigationService)
        {
            _saleItemsManager = saleItemsManager;
            _userManager = userManager;
        }

        public long Min { get; set; }
        public long Max { get; set; }
        public long SelectedValue { get; set; }
        public bool CheckEntryValue { get; set; }
        public DelegateCommand PlaceAPriceCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (SelectedValue >= Min && SelectedValue <= Max)
                {
                    await _saleItemsManager.UpdateAuctionPrice(_saleItem, SelectedValue);
                    await Application.Current.MainPage.DisplayAlert("Placed a price", "Price was updated succesfully!", "OK");
                    await NavigationService.NavigateAsync($"{nameof(MainTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(MainPage)}");
                }
                else
                    CheckEntryValue = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            RaisePropertyChanged(nameof(CheckEntryValue));
        });

        /// <summary>
        /// Happens when redirected to this page
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            //Try to get passed parameters
            if (parameters.TryGetValue(Universe.SaleItemParameter, out SaleItem saleItem))
            {
                try
                {
                    _saleItem = saleItem;
                    Min = _saleItem.AuctionPrice + 1;
                    Max = _saleItem.AuctionPrice + 99;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            CheckEntryValue = false;

            RaisePropertyChanged(nameof(CheckEntryValue));
            RaisePropertyChanged(nameof(Min));
            RaisePropertyChanged(nameof(Max));
            RaisePropertyChanged(nameof(SelectedValue));
            IsBusy = false;
        }
    }
}
