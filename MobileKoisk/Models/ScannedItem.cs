using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Models
{
    // Scanning-related models
    public class ScannedItem
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string ProductCategory { get; set; }
        public string ManufacturerDetails { get; set; }
        public DateTime ScannedAt { get; set; }
    }
}
