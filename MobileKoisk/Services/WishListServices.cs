using CommunityToolkit.Mvvm.Input;
using MobileKoisk.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileKoisk.Services
{
	public class WishListServices : INotifyPropertyChanged
    {
		private readonly ObservableCollection<Product> _wishlist = new ObservableCollection<Product>();
        private double _totalPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Product> WishList => _wishlist;
        public ICommand RemoveCommand { get; }

        public double TotalPrice
        {
            get => _totalPrice;
            private set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice)));
                }
            }
        }

        public WishListServices()
        {
            RemoveCommand = new RelayCommand<Product>(RemoveFromWishList);
            _wishlist.CollectionChanged += (s, e) => CalculateTotalPrice();
        }
        public void  AddToWishList(Product product)
		{
			if(!_wishlist.Contains(product))
			{
				_wishlist.Add(product);
			}
		}

        private void CalculateTotalPrice()
        {
            TotalPrice = _wishlist.Sum(p => p.selling_price);
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
