using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileKoisk.Api.Models
{
    public class BasketItem
    {
        // Properties
        public string Barcode { get; set; }
        public string? ImageSource { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
       

        public int Quantity { get; set; }
     
      
    }
}
