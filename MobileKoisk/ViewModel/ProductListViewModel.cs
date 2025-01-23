using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MobileKoisk.Models;
using MobileKoisk.Services;

namespace MobileKoisk.ViewModel
{
	public class ProductListViewModel : BaseViewModel
	{
		private readonly ProductItemService _productItemService;
		private readonly WishListServices _wishListServices;

		// Declare FilteredProducts
		public ObservableCollection<Product> FilteredProducts { get; private set; } = new ObservableCollection<Product>();

		public ObservableCollection<Product> UnFilteredProducts { get; private set; } = new ObservableCollection<Product>();
		public ObservableCollection<Product> WishList { get; private set; } = new ObservableCollection<Product>();
		public string CategoryTitle { get; set; }
		public Command<Product> AddProductsToWishListCommand { get; }

		public ProductListViewModel(WishListServices wishListServices, string category)
		{
			_wishListServices = wishListServices;
			_productItemService = new ProductItemService();
			
			CategoryTitle = category;

			LoadDataAsync(category);

			AddProductsToWishListCommand = new Command<Product>(AddProductsToWishList);
		}

		private async Task LoadDataAsync(string category)
		{
			await LoadProductsAsync();
			LoadAndFilterProducts(category);
		}

		private void LoadAndFilterProducts(string category)
		{
			// Fetch products from the service
			var filtered = UnFilteredProducts.Where(p => p.category.Equals(category, StringComparison.OrdinalIgnoreCase));
			foreach (var product in filtered)
			{
				FilteredProducts.Add(product);
			}
		}

		public async Task LoadProductsAsync()
		{
			try
			{
				var products = await _productItemService.LoadJsonDataAsync();

				UnFilteredProducts.Clear(); // Clear existing items
				foreach (var product in products)
				{
					UnFilteredProducts.Add(product);
				}
			}
			catch (Exception ex)
			{
				// Handle exception (e.g., log it or show a message to the user)
				Console.WriteLine($"Error loading products: {ex.Message}");
			}
		}

		public void AddProductsToWishList(Product product)
		{
			_wishListServices.AddToWishList(product);
			BadgeCounterService.SetCount(BadgeCounterService.Count + 1);
			System.Diagnostics.Debug.WriteLine($"Product {product.item_description} added to wishlist");
		}
	}
}
