namespace MobileKoisk.Api.Models
{
    public class ReceiptItem
    {
        public int ReceiptItemId { get; set; }

        public int ReceiptId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double PriceAtPurchase { get; set; }
    }
}
