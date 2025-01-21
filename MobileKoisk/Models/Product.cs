
using System.Text.Json.Serialization;


namespace MobileKoisk.Models
{
	public class Product
	{

		public string from_date { get; set; }	
		public int section { get; set; }
		public object barcode { get; set; }
		public int item_num { get; set; }
		public string item_description { get; set; }
		public double selling_price { get; set; }
		public double quantity { get; set; }
		public string image_url { get; set; }
		public string category {  get; set; }

	}

	[JsonSerializable(typeof(List<Product>))]
	internal sealed partial class ProductItemContext : JsonSerializerContext
	{
	}
}
