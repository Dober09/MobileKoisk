using MobileKoisk.Models;

using System.Text.Json;

namespace MobileKoisk.Services
{
    public class ProductItemService : IProductItem 
    {
        HttpClient httpClient;

        List<Product> products = new List<Product>();

        public ProductItemService()
        {
            httpClient = new HttpClient();
        }
        public async Task<List<Product>> LoadJsonDataAsync()
        {
            if (products?.Count > 0) { 
                return products;
            }

            // offline reading of json
            using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            products = JsonSerializer.Deserialize<List<Product>>(content);


       
             

            return products;
        
        }
    }
}
