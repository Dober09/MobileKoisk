using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileKoisk.Models;
using System.Windows.Input;


namespace MobileKoisk.ViewModel
{
    class BasketPageViewModel
    {
		public ICommand NavigateToCheckoutCommand { get; }
		public ObservableCollection<BasketItem> BasketItems { get; set; }

        public BasketPageViewModel()
        {
            BasketItems = new ObservableCollection<BasketItem>
            {
               new BasketItem {ImageSource = "coffee.png", ProductName = "Coffee", Quantity=1, Price = 19.99M },
               new BasketItem { ImageSource = "selatisugar.png", ProductName = "Sugar", Quantity = 1, Price = 9.99M },
			   new BasketItem { ImageSource = "cremora.png", ProductName = "Milk", Quantity = 2, Price = 29.99M }
			};

		}
	}
}
