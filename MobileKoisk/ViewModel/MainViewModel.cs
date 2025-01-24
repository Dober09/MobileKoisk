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
		private readonly ProductItemService _productItemService;

		private readonly SalesService _salesService;

		private readonly WishListServices _wishListServices;
		public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
		public ObservableCollection<SaleItem> SaleItems { get; } = new ObservableCollection<SaleItem>();
		public ObservableCollection<Product> FilteredProducts { get; set; } = new ObservableCollection<Product>();


		public ICommand AddToWishListCommand { get; }	
		public ICommand CategoryTappedCommand { get; }

		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set
			{
				_isLoading = value;
				// Notify UI of changes
				//OnPropertyChanged();
			}
		}

		public MainViewModel( ProductItemService productItemService, WishListServices wishListServices)
		{
			_productItemService = new ProductItemService();
			_wishListServices = wishListServices;
			LoadProductsAsync();
			LoadSalesAsync();	
			CategoryTappedCommand = new Command<string>(OnCategoryTapped);
			AddToWishListCommand = new Command<Product>(AddToWishList);
			

		}

		public async Task LoadProductsAsync()
		{
			if (IsLoading) return;

			IsLoading = true;

			try
			{
				var products = await _productItemService.LoadJsonDataAsync();

				Products.Clear(); // Clear existing item'
				
				foreach (var product in products)
				{
					Products.Add(product);
				}
			}
			catch (Exception ex)
			{
				// Handle exception (e.g., log it or show a message to the user)
				Console.WriteLine($"Error loading products: {ex.Message}");
			}
			finally
			{
				IsLoading = false;
			}
		}

		public async Task LoadSalesAsync()
		{
			if (IsLoading) return;

			IsLoading = true;

			try
			{
				var saleitems = await _salesService.LoadJsonDataAsync();

				Products.Clear(); // Clear existing item'

				foreach (var saleitem in saleitems)
				{
					SaleItems.Add(saleitem);
				}
			}
			catch (Exception ex)
			{
				// Handle exception (e.g., log it or show a message to the user)
				Console.WriteLine($"Error loading products: {ex.Message}");
			}
			finally
			{
				IsLoading = false;
			}
		}

		public void FilteredProductsByCategory(string category)
		{
			FilteredProducts.Clear();
			var filtered = Products.Where(p => p.category.Equals(category, StringComparison.OrdinalIgnoreCase));
			foreach(var product in filtered)
			{
				FilteredProducts.Add(product);
			}
		}

		public async void OnCategoryTapped(string category)
		{
			FilteredProducts.Clear();
			foreach(var product in Products)
			{
				if (product.category == category)
				{
					FilteredProducts.Add(product);
				}
			}

			NavigateToFilteredProductsPage(category);
		}

		private void NavigateToFilteredProductsPage(string category)
		{
			var productsPage = new ProductListPage(category);
			Application.Current.MainPage.Navigation.PushAsync(productsPage);
		}

		private void AddToWishList(Product product)
		{
			_wishListServices.AddToWishList(product);
		}

	}
}
