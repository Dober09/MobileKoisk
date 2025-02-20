using CommunityToolkit.Mvvm.Input;
using MobileKoisk.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MobileKoisk.Services
{
	public class WishListServices
	{
		private readonly ObservableCollection<Product> _wishlist = new ObservableCollection<Product>();	

		public ObservableCollection<Product> WishList => _wishlist;
        public ICommand RemoveCommand { get; }

        public WishListServices()
        {
            RemoveCommand = new RelayCommand<Product>(RemoveFromWishList);
        }
        public void  AddToWishList(Product product)
		{
			if(!_wishlist.Contains(product))
			{
				_wishlist.Add(product);
			}
		}

        public void RemoveFromWishList(Product product)
        {
            if (product != null && _wishlist.Contains(product))
            {
                _wishlist.Remove(product);
                WishlistCounterServirce.DecrementCount();
              
            }
        }
    }
}
