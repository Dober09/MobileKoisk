using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Models
{
    // Receipt Model
    public class Receipt
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public List<CartItem> PurchasedItems { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string MerchantName { get; set; }
        public string ReceiptNumber { get; set; }
    }
}
