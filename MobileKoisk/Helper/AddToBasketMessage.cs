using MobileKoisk.Models;

namespace MobileKoisk.Helper
{
    public class AddToBasketMessage
    {
        public ProductItem ProductItem { get; }

        public AddToBasketMessage(ProductItem productItem)
        {
            ProductItem = productItem;
        }
    }
}
