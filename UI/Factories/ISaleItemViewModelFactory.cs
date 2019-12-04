using Data.Database.Models;
using UI.ViewModels;

namespace UI.Factories
{
    public interface ISaleItemViewModelFactory
    {
        SaleItemViewModel CreateSaleItemViewModel(SaleItem item);
    }
}
