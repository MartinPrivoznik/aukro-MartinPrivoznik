using Data.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataProviders.Repositories
{
    public interface ISaleItemsRepository
    {
        Task AddSaleItem(SaleItem item);
        Task UpdateItemAuctionPrice(SaleItem item, long newValue, string newWinner);
        Task<List<SaleItem>> GetAllSaleItems();
        Task<List<SaleItem>> GetItemsByUser(int userId);
        Task RemoveSaleItem(SaleItem item);
        Task<List<SaleItem>> GetNotOwnItems(int actualUserId);
    }
}
