namespace MobileKoisk.Api.Models
{
    public class Product
    {
        public string from_date { get; set; }
        public int section { get; set; }
        public string barcode { get; set; }
        public int item_num { get; set; }
        public string item_description { get; set; }
        public double selling_price { get; set; }
        public double quantity { get; set; }
        public string image_url { get; set; }
        public string category { get; set; }
    }
}
