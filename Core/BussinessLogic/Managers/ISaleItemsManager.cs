using Data.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.BussinessLogic.Managers
{
    public interface ISaleItemsManager
    {
        Task<List<SaleItem>> GetNotOwnSaleItems();
        Task<List<SaleItem>> GetAllSaleItems();
        Task<List<SaleItem>> GetOwnSaleItems();
        Task AddSaleItem(string name, string description, long auctionPrice, long solidPrice);
        Task RemoveSaleItem(SaleItem saleItem);
        Task UpdateAuctionPrice(SaleItem item, long newPrice);
    }
}
