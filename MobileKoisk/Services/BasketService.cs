
using MobileKoisk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Services
{
    public static class BasketService
    {
        private static ObservableCollection<BasketItem> _basketItems = new ObservableCollection<BasketItem>();
        public static ObservableCollection<BasketItem> BasketItems => _basketItems;

        // Event that will be raised when basket items change
        public static event Action<BasketItem> OnBasketItemAdded;
        public static event Action BasketItemsChanged;
        public static decimal GetTotalPrice()
        {
            return _basketItems.Sum(item => item.Price * item.Quantity);
        }

        public static void ClearBasket()
        {
            _basketItems.Clear();
            BasketItemsChanged?.Invoke();
        }

        public static void RemoveFromBasket(BasketItem item)
        {
            if (BasketItems.Contains(item))
            {
                BasketItems.Remove(item);
                BasketItemsChanged?.Invoke();
            }
        }

        public static void AddItem(Product product)
        {
            var existingItem = _basketItems.FirstOrDefault(item => item.ProductName == product.item_description);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new BasketItem
                {
                    ProductName = product.item_description,
                    Price = (decimal)product.selling_price,
                    Quantity = 1,
                    ImageSource = product.image_url
                };

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _basketItems.Add(newItem);
                    OnBasketItemAdded?.Invoke(newItem);
                    BasketItemsChanged?.Invoke();
                });
            }
        }

    }
}
