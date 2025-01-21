using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using MobileKoisk.Helper;
using MobileKoisk.Models;

namespace MobileKoisk.ViewModel
{
	public class BasketPageViewModel : INotifyPropertyChanged
	{
		public decimal _vatRate = 0.15m;
		private decimal _vatAmount;
		private decimal _totalAmount;

		public decimal VatAmount
		{
			get => _vatAmount;
			set
			{
				if (_vatAmount != value)
				{
					_vatAmount = value;
					OnPropertyChanged(nameof(VatAmount));	
				}
			}
		}

		private decimal _totalPrice;
		public decimal TotalAmount
		{
			get => _totalAmount;
			set
			{
				if (_totalAmount != value)
				{
					_totalAmount = value;
					OnPropertyChanged(nameof(TotalAmount));
				}
			}
		}


		//basket items list
		public ObservableCollection<BasketItem> BasketItems { get; set; }

		//sales list
		private ObservableCollection<SaleItem> _salesItem;

		public ObservableCollection<SaleItem> SalesItems
		{
			get => _salesItem;
			set
			{
				if (_salesItem != value)
				{
					_salesItem = value;
					OnPropertyChanged(nameof(SalesItems));
				}
			}
		}
			
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

		// Indicates whether the credit card option is currently selected
		private bool _isCreditCardSelected;

		// Indicates whether the credit card button is toggled on
		private bool _isCreditCardButtonSelected;

		// Public property to access and modify the credit card selection state
		public bool IsCreditCardSelected
		{
			get => _isCreditCardSelected;
			set
			{
				// Update the property value and notify listeners if it changes
				if (_isCreditCardSelected != value)
				{
					_isCreditCardSelected = value;
					OnPropertyChanged(nameof(IsCreditCardSelected));
				}
			}
		}

		// Public property to access and modify the credit card button selection state
		public bool IsCreditCardButtonSelected
		{
			get => _isCreditCardButtonSelected;
			set
			{
				// Update the property value and notify listeners if it changes
				if (_isCreditCardButtonSelected != value)
				{
					_isCreditCardButtonSelected = value;
					OnPropertyChanged(nameof(IsCreditCardButtonSelected));
				}
			}
		}

		// Command to toggle the selection state of the credit card button and visibility
		public ICommand SelectCreditCardCommand => new Command(() =>
		{
			// Toggle the button's selected state
			IsCreditCardButtonSelected = !IsCreditCardButtonSelected;
			// Toggle the visibility of the credit card option
			IsCreditCardSelected = !IsCreditCardSelected;
		});

		public BasketPageViewModel()
		{

			// Initialize the basket items
			BasketItems = new ObservableCollection<BasketItem>();

			WeakReferenceMessenger.Default.Register<AddToBasketMessage>(this, (r, m) => { 
				
			});

			// Attach property change notifications for basket items
			foreach (var item in BasketItems)
			{
				item.PropertyChanged += OnBasketItemChanged;
			}

			// Update the total price
			UpdateTotalPrice();

		

			//basket items list
			SalesItems = new ObservableCollection<SaleItem>
			{
				new SaleItem { saleName = "Red Friday Sales", saleDescription = "Save Up to 50%", saleDate = "Fri 09 - Sun 11 November",Color = "#938059" },
				new SaleItem { saleName = "Fresh Wednesdays", saleDescription = "R10 Wednesday", saleDate = "Wednesdays", Color = "#618943" },
				new SaleItem { saleName = "Rush Hour Monday", saleDescription = "Exclusive Deals", saleDate = "Monday 17h00 to 18h00", Color = "#C5D86D" }
			};
		
		}


		//

		private void AddProductToBasket(ProductItem productItem)
		{
			MainThread.BeginInvokeOnMainThread(() =>
			{

				var existingItem = BasketItems.FirstOrDefault((item) => productItem.barcode == item.Barcode);
				if (existingItem != null)
				{
					// If item exist, increment quantity
					existingItem.Quantity++;
				}
				else
				{
					//If item doesn't exist, add new BasketItem
					var newItem = BasketItem.FromProductItem(productItem);
					BasketItems.Add(newItem);
					newItem.PropertyChanged += OnBasketItemChanged;
				}

				//Update totals
				UpdateTotalPrice();
			});
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

			//calculate vat
			VatAmount = TotalPrice * _vatRate /(1 + _vatRate);

			////Calculate Total Amount including vat
			TotalAmount = TotalPrice;

			// Notify that VAT-related properties have changed
			OnPropertyChanged(nameof(VatAmount));
			OnPropertyChanged(nameof(TotalAmount));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}

	
	}
