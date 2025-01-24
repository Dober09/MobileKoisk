using MobileKoisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobileKoisk.Services
{

    class SalesService
    {
		List<SaleItem> saleItems = new List<SaleItem>();
		public async Task<List<SaleItem>> LoadJsonDataAsync()
		{
			if (saleItems?.Count > 0)
			{
				return saleItems;
			}

			// offline reading of json
			using var stream = await FileSystem.OpenAppPackageFileAsync("sale.json");
			using var reader = new StreamReader(stream);
			var content = await reader.ReadToEndAsync();
			saleItems = JsonSerializer.Deserialize<List<SaleItem>>(content);


			// online api from  our database designed by use
			// var url = "";// the url from the api we created
			//connect it to the net
			//var response = await httpClient.GetAsync(url);

			//if (response.IsSuccessStatusCode)
			//{
			//products = await response.Content.ReadAsStringAsync(ProductItemContext.Default.ListProductItem);
			//}


			return saleItems;

		}




	}
}
