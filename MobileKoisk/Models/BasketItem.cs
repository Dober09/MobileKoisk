using System.ComponentModel;
using System.Windows.Input;
using MobileKoisk.Services;

namespace MobileKoisk.Models
{
	public class BasketItem : INotifyPropertyChanged
	{
		// Properties
		public string Barcode { get; set; }
		public string? ImageSource { get; set; }
		public string? ProductName { get; set; }
		public decimal Price { get; set; }
		public string ProductSize { get; set; }

		private int _quantity;
		public int Quantity
		{
			get => _quantity;
			set
			{
				if (_quantity != value)
				{
					_quantity = value;
					OnPropertyChanged(nameof(Quantity));
				}
			}
		}

		// Commands
		public ICommand IncreaseQuantityCommand { get; }
		public ICommand DecreaseQuantityCommand { get; }

		// Constructor
		public BasketItem()
		{
			IncreaseQuantityCommand = new Command(() => IncreaseQuantity());
			DecreaseQuantityCommand = new Command(() => DecreaseQuantity());
		}

		public static BasketItem FromProductItem(Product product)
		{
			return new BasketItem
			{
				ProductName = product.item_description,
				Price = (decimal)product.selling_price,
				Quantity = 1,
				ImageSource = "meat.png",
				ProductSize = product.from_date,
				Barcode = (string)product.barcode,
				
			};
		}

		// Methods
		private void IncreaseQuantity()
		{
			Quantity++;
		}
        //
        public ICommand RemoveFromBasketCommand => new Command(() =>
        {
            // Since 'this' here refers to the BasketItem instance,
            // we can pass it directly to the BasketService
            BasketService.RemoveFromBasket(this);
            BadgeCounterService.DecrementCount();
        });

        private void DecreaseQuantity()
		{
			if (Quantity > 0)
			{
				Quantity--;
			}
		}

		// PropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
