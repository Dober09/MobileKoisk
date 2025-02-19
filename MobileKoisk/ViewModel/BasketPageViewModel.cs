using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using MobileKoisk.Helper;
using MobileKoisk.Models;
using MobileKoisk.Services;

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
		public ObservableCollection<BasketItem> BasketItems => BasketService.BasketItems;

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

			BasketService.BasketItemsChanged += OnBasketItemsChanged;
			BasketService.OnBasketItemAdded += OnBasketItemAdded;

			UpdateTotalPrice();
			// Initialize the basket items
			//BasketItems = new ObservableCollection<BasketItem>();

			//BasketItems.Add(

			//	new BasketItem { 
			//		ProductName = "dfdfdf"
			//	});

			//         // Register for messages
			//         WeakReferenceMessenger.Default.Register<AddToBasketMessage>(this, (r, m) => {
			//             System.Diagnostics.Debug.WriteLine($"Message received in ViewModel for product: {m.ProductItem?.item_description}");
			//             HandleAddToBasketMessage(m);

			//             System.Diagnostics.Debug.WriteLine($"Initial basket count: {BasketItems.Count}");
			//         });

			//         // Attach property change notifications for basket items
			//         foreach (var item in BasketItems)
			//{
			//	item.PropertyChanged += OnBasketItemChanged;
			//}

			// Update the total price
			UpdateTotalPrice();
		
		}


        private void OnBasketItemAdded(BasketItem item)
        {
            item.PropertyChanged += OnBasketItemChanged;
            UpdateTotalPrice();
        }

        private void OnBasketItemsChanged()
        {
            UpdateTotalPrice();
            OnPropertyChanged(nameof(BasketItems));
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
            TotalPrice = BasketService.GetTotalPrice();
        }
  //      private void HandleAddToBasketMessage( AddToBasketMessage message)
		//{


  //          try
  //          {
  //              if (message?.ProductItem == null)
  //              {
  //                  System.Diagnostics.Debug.WriteLine("Received null message or product");
  //                  return;
  //              }

  //              System.Diagnostics.Debug.WriteLine($"Processing product: {message.ProductItem.item_description}");

  //              //check if item already exists
  //              var existingItem = BasketItems.FirstOrDefault(item => item.ProductName == message.ProductItem.item_description);
  //              if (existingItem != null)
  //              {
  //                  existingItem.Quantity++;
  //                  System.Diagnostics.Debug.WriteLine($"Updated quantity for {existingItem.ProductName} to {existingItem.Quantity}");
  //              }
  //              else
  //              {
  //                  var newItem = new BasketItem
  //                  {
  //                      ProductName = message.ProductItem.item_description,
  //                      Price = (decimal)message.ProductItem.selling_price,
  //                      Quantity = 1,
  //                      ImageSource = message.ProductItem.image_url, //  set the image
                       
  //                  };

  //                  // Make sure to add property changed handler before adding to collection
  //                  newItem.PropertyChanged += OnBasketItemChanged;

  //                  MainThread.BeginInvokeOnMainThread(() => {
  //                      BasketItems.Add(newItem);
  //                      System.Diagnostics.Debug.WriteLine($"Current basket count: {BasketItems.Count}");
		//				System.Diagnostics.Debug.WriteLine($"Added new item: {newItem.ProductName}");
  //                      OnPropertyChanged(nameof(BasketItems));
  //                      UpdateTotalPrice();
  //                  });
  //              }
  //          }
  //          catch (Exception ex)
  //          {
  //              System.Diagnostics.Debug.WriteLine($"Error adding to basket: {ex.Message}");
  //          }


  //      }

        //private void AddProductToBasket(ProductItem productItem)
        //{
        //	MainThread.BeginInvokeOnMainThread(() =>
        //	{

        //		var existingItem = BasketItems.FirstOrDefault((item) => productItem.barcode == item.Barcode);
        //		if (existingItem != null)
        //		{
        //			// If item exist, increment quantity
        //			existingItem.Quantity++;
        //		}
        //		else
        //		{
        //			//If item doesn't exist, add new BasketItem
        //			var newItem = BasketItem.FromProductItem(productItem);
        //			BasketItems.Add(newItem);
        //			newItem.PropertyChanged += OnBasketItemChanged;
        //		}

        //		//Update totals
        //		UpdateTotalPrice();
        //	});
        //}

  //      private void OnBasketItemChanged(object sender, PropertyChangedEventArgs e)
		//{
		//	if (e.PropertyName == nameof(BasketItem.Quantity))
		//	{
		//		UpdateTotalPrice();
		//	}
		//}

		//private void UpdateTotalPrice()
		//{
		//	// Calculate the total price by summing up the price of each item multiplied by its quantity
		//	TotalPrice = BasketItems.Sum(item => item.Price * item.Quantity);

		//	//calculate vat
		//	VatAmount = TotalPrice * _vatRate /(1 + _vatRate);

		//	////Calculate Total Amount including vat
		//	TotalAmount = TotalPrice;

		//	// Notify that VAT-related properties have changed
		//	OnPropertyChanged(nameof(VatAmount));
		//	OnPropertyChanged(nameof(TotalAmount));
		//}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}

	
	}
