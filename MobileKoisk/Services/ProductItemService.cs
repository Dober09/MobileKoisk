using MobileKoisk.Models;

using System.Text.Json;

namespace MobileKoisk.Services
{
    public class ProductItemService : IProductItem 
    {
        HttpClient httpClient;

        List<ProductItem> products = new List<ProductItem>();

        public ProductItemService()
        {
            httpClient = new HttpClient();
        }
        public async Task<List<ProductItem>> LoadJsonDataAsync()
        {
            if (products?.Count > 0) { 
                return products;
            }

            // offline reading of json
            using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            products = JsonSerializer.Deserialize<List<ProductItem>>(content);


            // online api from  our database designed by use
            var url = "";// the url from the api we created
                         //connect it to the net
            //var response = await httpClient.GetAsync(url);

            //if (response.IsSuccessStatusCode)
            //{
                //products = await response.Content.ReadAsStringAsync(ProductItemContext.Default.ListProductItem);
            //}
             

            return products;
        
        }
    }
}
