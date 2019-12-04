using Core.BussinessLogic.Managers;
using Core.Events;
using Data.Database.Models;
using Data.Models.Constants;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Windows.Input;
using UI.ViewModels.AbstractVMBase;
using UI.Views;

namespace UI.ViewModels
{
    public class SaleItemViewModel : ViewModelBase
    {
        private readonly SaleItem _saleItem;
        private readonly IUserManager _userManager;
        private readonly ISaleItemsManager _saleItemsManager;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initial Viewmodel used for SaleItem collections
        /// to make it look better and its easier to call the methods from here.
        /// Viewmodel is being created by factory, so programmer can get as many as he needs
        /// </summary>
        public SaleItemViewModel(INavigationService navigationService, SaleItem saleItem, IUserManager userManager, ISaleItemsManager saleItemsManager, IEventAggregator eventAggregator) : base(navigationService)
        {
            _userManager = userManager;
            _saleItemsManager = saleItemsManager;
            _eventAggregator = eventAggregator;
            _saleItem = saleItem;
            Name = _saleItem.Name;
            Description = _saleItem.Description;
            var owner = _userManager.GetUserById(_saleItem.UserId).GetAwaiter().GetResult();
            OwnerName = $"{owner.FirstName} {owner.Surname}";
            OwnerPhoneNumber = $"{owner.PhoneNumber}";
            SolidPrice = _saleItem.SolidPrice;
            AuctionPrice = _saleItem.AuctionPrice;
            TimeAdded = _saleItem.TimeAdded;
            AuctionWinner = _saleItem.AuctionWinner;
        }

        public string Name
        {
            get;
        }

        public string Description
        {
            get;
        }

        public string OwnerName
        {
            get;
        }
        public string OwnerPhoneNumber
        {
            get;
        }
        public long SolidPrice
        {
            get;
        }
        public long AuctionPrice
        {
            get;
        }
        public DateTime TimeAdded
        {
            get;
        }
        public string AuctionWinner
        {
            get;
        }
        public ICommand NavigateCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync(nameof(SaleItemDetailPage), new NavigationParameters { { Universe.SaleItemParameter, _saleItem} });
        });
        public ICommand EditCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync(nameof(AddOrModifySaleItem), new NavigationParameters { { Universe.SaleItemParameter, _saleItem } });
        });
        public ICommand SellItemCommand => new DelegateCommand(async () =>
        {
            //Some buy logic, now just removes from databse
            await _saleItemsManager.RemoveSaleItem(_saleItem);
            _eventAggregator.GetEvent<ItemSoldEvent>().Publish();
        });
    }
}
