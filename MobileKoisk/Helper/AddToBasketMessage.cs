using MobileKoisk.Models;

namespace MobileKoisk.Helper
{
    public class AddToBasketMessage
    {
        public Product ProductItem { get; }

        public AddToBasketMessage(Product productItem)
        {
            ProductItem = product ??  throw new ArgumentNullException(nameof(product));
        }
    }
}
