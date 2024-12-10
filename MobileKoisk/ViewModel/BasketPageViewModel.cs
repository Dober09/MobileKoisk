using System.Collections.ObjectModel;
using System.Windows.Input;

using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.View;




namespace MobileKoisk.ViewModel
{
	public class BasketPageViewModel
	{
	

		public ICommand ProceedToPaymentCommand { get; }

		public ObservableCollection<BasketItem> BasketItems { get; set; }

		public BasketPageViewModel()
		{
			

			BasketItems = new ObservableCollection<BasketItem>
			{
			   new BasketItem { ImageSource = "coffee.png", ProductName = "Coffee", Quantity = 1, Price = 19.99M,  ProductSize = "750 ml" },
			   new BasketItem { ImageSource = "selatisugar.png", ProductName = "Sugar", Quantity = 1, Price = 9.99M, ProductSize = "1 kg" },
			   new BasketItem { ImageSource = "cremora.png", ProductName = "Milk", Quantity = 2, Price = 29.99M, ProductSize = "500 ml" }

			};
		}

		
	}
}
