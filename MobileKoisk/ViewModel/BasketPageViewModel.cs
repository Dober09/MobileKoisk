using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.View;

namespace MobileKoisk.ViewModel
{
	public class BasketPageViewModel
	{
		private readonly INavigationService _navigationService;

		public ICommand ProceedToPaymentCommand { get; }

		public ObservableCollection<BasketItem> BasketItems { get; set; }

		public BasketPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService; // Use the injected INavigationService
			ProceedToPaymentCommand = new Command(async () => await NavigateToPayment());

			BasketItems = new ObservableCollection<BasketItem>
			{
			   new BasketItem { ImageSource = "coffee.png", ProductName = "Coffee", Quantity = 1, Price = 19.99M },
			   new BasketItem { ImageSource = "selatisugar.png", ProductName = "Sugar", Quantity = 1, Price = 9.99M },
			   new BasketItem { ImageSource = "cremora.png", ProductName = "Milk", Quantity = 2, Price = 29.99M }
			};
		}

		private async Task NavigateToPayment()
		{
			await _navigationService.NavigateToAsync<PaymentPage>();
		}
	}
}
