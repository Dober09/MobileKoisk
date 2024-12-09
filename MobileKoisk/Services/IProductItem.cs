using MobileKoisk.Models;

namespace MobileKoisk.Services
{
    public interface IProductItem
    {
        Task<List<ProductItem>> LoadJsonDataAsync();
    }
}
