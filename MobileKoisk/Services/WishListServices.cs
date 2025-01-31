using MobileKoisk.Models;
using System.Collections.ObjectModel;

namespace MobileKoisk.Services
{
	public class WishListServices
	{
		private readonly ObservableCollection<Product> _wishlist = new ObservableCollection<Product>();	

		public ObservableCollection<Product> WishList => _wishlist;

		public void  AddToWishList(Product product)
		{
			if(!_wishlist.Contains(product))
			{
				_wishlist.Add(product);
			}
		}
	}
}
