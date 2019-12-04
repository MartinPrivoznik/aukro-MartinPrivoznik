using Core.BussinessLogic.Managers;
using Core.DataProviders.Repositories;
using Data.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLogic.Managers
{
    public class SaleItemsManager : ISaleItemsManager
    {
        private readonly ISaleItemsRepository _saleItemsRepository;
        private readonly IUserManager _userManager;

        public SaleItemsManager(ISaleItemsRepository saleItemsRepository, IUserManager userManager)
        {
            _saleItemsRepository = saleItemsRepository;
            _userManager = userManager;
        }

        public Task AddSaleItem(string name, string description, long auctionPrice, long solidPrice)
        {
            return _saleItemsRepository.AddSaleItem(new SaleItem { Name = name, AuctionPrice = auctionPrice, SolidPrice = solidPrice, Description = description, UserId = _userManager.LoggedUser.id, TimeAdded = DateTime.Now});
        }

        public Task<List<SaleItem>> GetAllSaleItems()
        {
            return _saleItemsRepository.GetAllSaleItems();
        }

        public Task<List<SaleItem>> GetNotOwnSaleItems()
        {
            return _saleItemsRepository.GetNotOwnItems(_userManager.LoggedUser.id);
        }

        public Task<List<SaleItem>> GetOwnSaleItems()
        {
            return _saleItemsRepository.GetItemsByUser(_userManager.LoggedUser.id);
        }

        public Task RemoveSaleItem(SaleItem saleItem)
        {
            return _saleItemsRepository.RemoveSaleItem(saleItem);
        }

        public Task UpdateAuctionPrice(SaleItem item, long newPrice)
        {
            return _saleItemsRepository.UpdateItemAuctionPrice(item, newPrice, $"{_userManager.LoggedUser.FirstName} {_userManager.LoggedUser.Surname}");
        }
    }
}
