using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using MobileKoisk.Models;

namespace MobileKoisk.ViewModel
{
	public class BasketPageViewModel : INotifyPropertyChanged
	{
		private decimal _totalPrice;

		public ICommand ProceedToPaymentCommand { get; }

		public ObservableCollection<BasketItem> BasketItems { get; set; }

		public decimal TotalPrice
		{
			get => _totalPrice;
			set
			{
				if (_totalPrice != value)
				{
					_totalPrice = value;
					OnPropertyChanged(nameof(TotalPrice));
				}
			}
		}

		public BasketPageViewModel()
		{
			// Initialize the basket items
			BasketItems = new ObservableCollection<BasketItem>
			{
				new BasketItem { ImageSource = "coffee.png", ProductName = "Coffee", Quantity = 1, Price = 19.99M, ProductSize = "750 ml" },
				new BasketItem { ImageSource = "selatisugar.png", ProductName = "Sugar", Quantity = 1, Price = 9.99M, ProductSize = "1 kg" },
				new BasketItem { ImageSource = "cremora.png", ProductName = "Milk", Quantity = 2, Price = 29.99M, ProductSize = "500 ml" }
			};

			// Attach property change notifications for basket items
			foreach (var item in BasketItems)
			{
				item.PropertyChanged += OnBasketItemChanged;
			}

			// Update the total price
			UpdateTotalPrice();
		}

		private void OnBasketItemChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(BasketItem.Quantity))
			{
				UpdateTotalPrice();
			}
		}

		private void UpdateTotalPrice()
		{
			// Calculate the total price by summing up the price of each item multiplied by its quantity
			TotalPrice = BasketItems.Sum(item => item.Price * item.Quantity);
		}

		// Correctly implement the INotifyPropertyChanged interface
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
