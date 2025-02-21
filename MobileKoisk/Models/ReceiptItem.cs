


namespace MobileKoisk.Models
{
    public class ReceiptItem
    {
       
        public int ReceiptItemId { get; set; }
        
        public int ReceiptId { get; set; }
       
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
    }

}
