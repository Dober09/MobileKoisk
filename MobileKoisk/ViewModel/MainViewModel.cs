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

		public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
		public ObservableCollection<Product> FilteredProducts { get; set; } = new ObservableCollection<Product>();
		public ICommand CategoryTappedCommand { get; }

		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set
			{
				_isLoading = value;
				OnPropertyChanged(); // Notify UI of changes
			}
		}

		public MainViewModel()
		{
			_productItemService = new ProductItemService();
			LoadProductsAsync();
			CategoryTappedCommand = new Command<string>(OnCategoryTapped);
			

		}

		public async Task LoadProductsAsync()
		{
			if (IsLoading) return;

			IsLoading = true;

			try
			{
				var products = await _productItemService.LoadJsonDataAsync();

				Products.Clear(); // Clear existing items
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

	}
}
