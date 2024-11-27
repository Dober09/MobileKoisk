using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.ViewModel
{
	public class ReceiptPageViewModel
	{
		// Properties for receipt data
		public string TransactionTime { get; set; }
		public string TransactionId { get; set; }
		public string CashierName { get; set; }
		public ObservableCollection<Item> Items { get; set; }
		public string TotalAmount { get; set; }
		public string VatAmount { get; set; }
		public string PaymentMethod { get; set; }

		// Constructor
		public ReceiptPageViewModel()
		{
			// Sample Data for Demonstration
			TransactionTime = "27/11/2024 15:30:45";
			TransactionId = "TX123456789";
			CashierName = "John Doe";
			PaymentMethod = "Credit Card";

			// Sample items with their individual totals
			Items = new ObservableCollection<Item>
			{
				new Item { Quantity = "1", Description = "Milk 1L", UnitPrice = "R20.00", TotalPrice = "R20.00" },
				new Item { Quantity = "2", Description = "Bread Loaf", UnitPrice = "R15.00", TotalPrice = "R30.00" },
				new Item { Quantity = "3", Description = "Eggs (Dozen)", UnitPrice = "R50.00", TotalPrice = "R150.00" }
			};

			// Calculate totals dynamically
			CalculateTotals();
		}

		private void CalculateTotals()
		{
			// Calculate total amount
			decimal total = 0;
			foreach (var item in Items)
			{
				if (decimal.TryParse(item.TotalPrice.Trim('R'), out var itemTotal))
				{
					total += itemTotal;
				}
			}

			TotalAmount = $"R{total:F2}";

			// Calculate VAT (15%)
			decimal vat = total * 0.15m;
			VatAmount = $"R{vat:F2}";
		}
	}

	// Class for each item in the receipt
	public class Item
	{
		public string Quantity { get; set; }
		public string Description { get; set; }
		public string UnitPrice { get; set; }
		public string TotalPrice { get; set; }
	}
}


