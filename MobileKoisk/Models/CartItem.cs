using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ScannedItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
