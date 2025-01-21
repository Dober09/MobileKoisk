using MobileKoisk.Models;

namespace MobileKoisk.Services
{
    public interface IProductItem
    {
        Task<List<Product>> LoadJsonDataAsync();
    }
}
