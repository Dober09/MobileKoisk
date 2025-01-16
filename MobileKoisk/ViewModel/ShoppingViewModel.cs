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
		public ObservableCollection<ShoppingItem> ShoppingItems { get; set; } = new ObservableCollection<ShoppingItem>();

		public ShoppingViewModel(DatabaseService dbService)
		{
			_dbService = dbService;
			LoadShoppingItems();
		}

		private async void LoadShoppingItems()
		{
			try
			{
				var items = await _dbService.GetShoppingItemsAsync();
				foreach (var item in items)
				{
					ShoppingItems.Add(item);
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
