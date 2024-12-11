using System.ComponentModel;
using System.Windows.Input;

namespace MobileKoisk.Models
{
	public class BasketItem : INotifyPropertyChanged
	{
		// Properties
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

		// Methods
		private void IncreaseQuantity()
		{
			Quantity++;
		}

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
