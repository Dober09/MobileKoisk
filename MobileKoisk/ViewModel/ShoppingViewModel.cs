using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileKoisk.Models;
using MobileKoisk.Services;
using System.ComponentModel;

namespace MobileKoisk.ViewModel
{
	public class ShoppingViewModel : INotifyPropertyChanged
	{
		private readonly DatabaseService _dbService;

		//for binding shopping items to the ui
		public ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();

		public ShoppingViewModel(DatabaseService dbService)
		{
			_dbService = dbService;
			LoadProducts();
		}

		private async void LoadProducts()
		{
			try
			{
				var products = await _dbService.GetShoppingItemsAsync();
				foreach (var product in products)
				{
					products.Add(product);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading shopping items:{ex.Message}");
			}
			
		}
		public event PropertyChangedEventHandler? PropertyChanged;
	}


}
