using MobileKoisk.Models;
using System.Collections.ObjectModel;

namespace MobileKoisk.Services
{
	public class WishListServices
	{
		private static WishListServices _instance;

		public static WishListServices Instance	=> _instance ?? new WishListServices();
		public ObservableCollection<ProductItemService> WishList { get; } = new ObservableCollection<ProductItemService>();
	
		private WishListServices() { }	
	}
}
