using MobileKoisk.Models;

namespace MobileKoisk.Helper
{
    public class AddToBasketMessage
    {
        public Product ProductItem { get; }


        public AddToBasketMessage(Product product)

        {
            ProductItem = product ??  throw new ArgumentNullException(nameof(product));
            System.Diagnostics.Debug.WriteLine($"AddToBasketMessage created for: {product.item_description}");
        }
    }
}
