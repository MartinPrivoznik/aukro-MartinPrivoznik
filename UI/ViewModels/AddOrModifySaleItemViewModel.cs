using Core.BussinessLogic.Managers;
using Data.Database.Models;
using Data.Models.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.ViewModels.AbstractVMBase;
using UI.Views;

namespace UI.ViewModels
{
    public class AddOrModifySaleItemViewModel : ViewModelBase
    {
        private readonly ISaleItemsManager _saleItemsManager;

        //private props
        private bool _edit;
        private SaleItem _saleItem;

        public AddOrModifySaleItemViewModel(INavigationService navigationService, ISaleItemsManager saleItemsManager) : base(navigationService)
        {
            _saleItemsManager = saleItemsManager;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuctionPrice { get; set; }
        public long SolidPrice { get; set; }
        public bool NameWrittenCheck { get; set; }
        public bool PriceWrittenCheck { get; set; }
        public string ButtonText { get; set; }

        public DelegateCommand AddSaleItemCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;
            try
            {
                if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(SolidPrice.ToString()))
                {
                    if(string.IsNullOrWhiteSpace(Name))
                    {
                        NameWrittenCheck = true;
                    }
                    if(string.IsNullOrWhiteSpace(SolidPrice.ToString()))
                    {
                        PriceWrittenCheck = true;
                    }
                    RaisePropertyChanged(nameof(NameWrittenCheck));
                    RaisePropertyChanged(nameof(PriceWrittenCheck));
                }
                else
                {
                    if (_edit)
                    {
                        await _saleItemsManager.RemoveSaleItem(_saleItem);
                    }
                    await _saleItemsManager.AddSaleItem(Name, Description, AuctionPrice, SolidPrice);
                    await NavigationService.NavigateAsync($"{nameof(MainTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(OwnSaleItemsPage)}");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            IsBusy = false;
        });

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters.TryGetValue(Universe.SaleItemParameter, out SaleItem saleItem))
            {
                try
                {
                    _saleItem = saleItem;

                    Name = _saleItem.Name;
                    Description = _saleItem.Description;
                    AuctionPrice = _saleItem.AuctionPrice;
                    SolidPrice = _saleItem.SolidPrice;
                    ButtonText = "Save changes";
                    
                    _edit = true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(Description));
                RaisePropertyChanged(nameof(AuctionPrice));
                RaisePropertyChanged(nameof(SolidPrice));
            }
            else
            {
                _edit = false;
                ButtonText = "Add item to sale";
            }

            NameWrittenCheck = false;
            PriceWrittenCheck = false;

            RaisePropertyChanged(nameof(ButtonText));
            RaisePropertyChanged(nameof(NameWrittenCheck));
            RaisePropertyChanged(nameof(PriceWrittenCheck));

            IsBusy = false;
        }
    }
}
