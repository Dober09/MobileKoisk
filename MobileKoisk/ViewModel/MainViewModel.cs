using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.View;

namespace MobileKoisk.ViewModel
{
	public class MainViewModel : BaseViewModel
	{
		private readonly SalesService _salesService;
	 
		private readonly ProductItemService _productItemService;

		public ObservableCollection <Product> Products { get; set; }

		public ObservableCollection<SaleItem> SalesItems { get; set; }

		public MainViewModel(ProductItemService productItemService, SalesService  salesService) { 
		
			_productItemService = productItemService;
			_salesService = salesService;
			Products = new ObservableCollection<Product>();
			SalesItems = new ObservableCollection<SaleItem>(); 



			LoadProductItems();
			//LoadSalesItem();
		}


		private async void LoadProductItems()
		{
			try
			{
				var products = await _productItemService.LoadJsonDataAsync();
				Products.Clear();

				foreach (var product in products)
				{
					Products.Add(product);
				}
				System.Diagnostics.Debug.WriteLine($"Total list of products --->  {Products.Count}");

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error loading products --->  {ex.Message}");
			}
		}

		private async void LoadSalesItem()
		{
            try
            {
                var saleItems = await _salesService.LoadJsonDataAsync();
				SalesItems.Clear();

                foreach (var sale in saleItems)
                {
                    SalesItems.Add(sale);
                }
                System.Diagnostics.Debug.WriteLine($"Total list of products --->  {Products.Count}");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products --->  {ex.Message}");
            }

        }

	}
}
