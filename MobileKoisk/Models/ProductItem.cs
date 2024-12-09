
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


namespace MobileKoisk.Models;
    public class ProductItem
    {
        public string from_date { get; set; }
        public int department { get; set; }
        public int section { get; set; }
        public object barcode { get; set; }
        public int item_num { get; set; }
        public string item_description { get; set; }
        public double purshase_price { get; set; }
        public double selling_price { get; set; }
        public double quantity { get; set; }
    }

[JsonSerializable(typeof(List<ProductItem>))]
internal sealed partial class ProductItemContext : JsonSerializerContext
{
}



